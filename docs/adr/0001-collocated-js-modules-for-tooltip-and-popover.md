# ADR 0001 — Collocated JS modules for BitTooltip and BitPopover

- **Status**: Accepted
- **Date**: 2026-10-24
- **Scope**: `BitTooltip`, `BitPopover`, `wwwroot/js/bitblazor-interop.js`, Bootstrap Italia JS loading

## Context and problem space

`BitTooltip` and `BitPopover` are thin Blazor wrappers around the Bootstrap Italia
`Tooltip` and `Popover` JavaScript plugins. Both render a `display:contents` custom
element (`<bit-tooltip>` / `<bit-popover>`) and, on first render, attach a Bootstrap
instance to the wrapper's first child element through JS interop.

Today all the JavaScript lives in a single shared file, `wwwroot/js/bitblazor-interop.js`,
which:

1. Exports `initTooltipOnFirstChild` / `disposeTooltipOnFirstChild`,
   `initPopoverOnFirstChild` / `disposePopoverOnFirstChild`, plus the unused
   `initTooltip` / `disposeTooltip`.
2. Lazily loads `bootstrap-italia.bundle.min.js` through `ensureBootstrapItalia()`, by
   injecting a classic `<script>` element into `<head>`.

The shipped bundle is Bootstrap Italia **2.18.3** (Bootstrap core 5.2.3). It exposes its
version at runtime through `window.BOOTSTRAP_ITALIA_VERSION`.

### Problems with the current approach

| # | Problem | Impact |
|---|---------|--------|
| P1 | **Race in `ensureBootstrapItalia()`**: when a `script[data-bitblazor-bi]` element already exists (concurrent init from another module instance, hot reload), the promise resolves **immediately**, before the script has finished loading. | Init finds no `window.bootstrap` and silently does nothing. Intermittent missing tooltips/popovers. |
| P2 | **Hand-written loader**: deduplication relies on a module-level promise plus a DOM marker, re-implementing what the ES module map already provides. | Maintenance burden; root cause of P1. |
| P3 | **Low cohesion**: component-specific JS lives in a shared file far from the component; every new JS-backed component grows the same file. | Harder to discover, review and evolve per component. |
| P4 | **Dispose by DOM lookup**: dispose re-resolves `wrapper.firstElementChild`. If the child was re-rendered or replaced, the instance attached to the original element is never disposed. | Leaked Bootstrap instances / orphaned popups. |
| P5 | **Dispose during pending import**: if the component is disposed while `import()` or init is still awaiting, `_jsModule` is `null`, so dispose is skipped and init later attaches an instance to a detached element. | Leak. |
| P6 | **Silent failure**: when Bootstrap is unavailable, the interop returns without any diagnostic. | Hard to troubleshoot in consumer apps. |
| P7 | **Undefined version contract**: if a host page already provides `window.bootstrap`, the library silently uses it, whatever its version. No supported version is declared. | Version-dependent bugs with no diagnostic; unclear support boundary. |

### Decision drivers

- **Zero setup by default**: consumers only reference the CSS; the library loads its own,
  tested copy of the Bootstrap Italia JS on demand.
- **Bring your own copy, at your own risk**: consumers may load their own Bootstrap Italia
  JS (e.g. to use its features outside BitBlazor). Only the declared version is supported;
  any other version is the consumer's responsibility.
- Bundle loaded **once**, and only when a tooltip/popover is actually used.
- Correct lifecycle: every created instance is disposed exactly once.
- Follow the standard Blazor convention for component JS
  ([JS collocation](https://learn.microsoft.com/aspnet/core/blazor/js-interop/javascript-location)).
- Work in every interactive render mode (Server, WebAssembly, Auto); no-op in static SSR.

## Options considered

### A. Keep the shared `bitblazor-interop.js` and fix the loader

Fix P1 by awaiting the existing script's `load` event.

- ✅ Smallest change, no breaking change.
- ❌ Keeps the hand-written loader (P2) and low cohesion (P3); P4–P7 need separate fixes.

### B. Collocated modules and a mandatory manual `<script>` added by the consumer

Each component has a `.razor.js`; the consumer must add
`<script src="_content/BitBlazor/bootstrap-italia/js/bootstrap-italia.bundle.min.js">`
in `App.razor` / `index.html`.

- ✅ Most CSP-friendly (the tag can carry a nonce); no loader code at all.
- ✅ Consistent with how the CSS is referenced.
- ❌ **Breaking change**: existing apps silently lose tooltips until they add the tag.
- ❌ Load-order pitfalls (`defer`/`async`, placement after `blazor.*.js`, placement in
  a page instead of `App.razor` under enhanced navigation).
- ❌ The ~1 MB bundle and its document-level handlers become active on **every** page.

### C. Collocated modules with a static import of the bundle

Each `.razor.js` starts with
`import '../../bootstrap-italia/js/bootstrap-italia.bundle.min.js';`.

- ✅ Simplest code; the module map guarantees a single evaluation among library modules.
- ✅ The library always runs its own, tested version.
- ❌ **Incompatible with bring-your-own**: a static import cannot be conditional, so a host
  copy loaded via a classic `<script>` (not part of the module map) is evaluated a second
  time: `window.bootstrap` overwritten, instances registered with the host copy no longer
  found, document-level `data-bs-toggle` handlers registered twice (collapses/dropdowns
  that open and immediately close), font `<style>` blocks injected twice.
- ❌ A bundle load failure makes the whole component module fail to load; the browser
  error usually names `*.razor.js`, not the bundle.

### D. Collocated modules with a guarded dynamic import and a version check (**chosen**)

Each `.razor.js` imports the bundle via `import()` only when `window.bootstrap` is not
already defined, checks the Bootstrap Italia version, and exports a single module-pattern
object with a `create` function returning a disposable handle.

### E. Like D, with the loader in a shared module

The guard, import and version check are centralised in `wwwroot/js/bootstrap-loader.js`.

- ✅ Single place for the bundle path, supported version and check.
- ❌ Extra file and indirection; the module map already deduplicates the bundle by URL.

## Decision

Adopt **option D**, with the version contract below.

### Version contract

| Scenario | Behaviour | Support |
|----------|-----------|---------|
| No Bootstrap JS on the page (default) | The library imports its own bundled copy on first use. | ✅ Supported |
| Host provides Bootstrap Italia at the **supported version** | The library reuses `window.bootstrap`; no second copy is loaded. | ✅ Supported |
| Host provides any **other version** (or plain Bootstrap) | The library reuses it and logs a `console.warn` with the detected and supported versions. | ⚠️ Consumer's own risk |

Rules for the bring-your-own scenario, published in the documentation:

- The **supported version** is the one shipped with the package (currently `2.18.x`;
  differences in patch version do not trigger the warning).
- The JS and CSS must be the **same Bootstrap Italia version**: the popup markup
  (`.tooltip-inner`, `.popover-header`, `.bs-*-auto`, …) is generated by the JS and
  styled by the CSS.
- The host `<script>` must be **classic and synchronous** (no `defer`/`async`/`type="module"`)
  and placed **before** `blazor.web.js` / `blazor.webassembly.js`, in `App.razor` or
  `index.html` (not in a page or layout, which enhanced navigation does not re-run).
  Otherwise the guard may run before the host copy is evaluated and the bundle is
  evaluated twice.

The CSS is out of scope: it remains referenced by the consumer as today.

### File layout

```
src/BitBlazor/Components/Tooltip/BitTooltip.razor
src/BitBlazor/Components/Tooltip/BitTooltip.razor.cs
src/BitBlazor/Components/Tooltip/BitTooltip.razor.js      ← new
src/BitBlazor/Components/Popover/BitPopover.razor
src/BitBlazor/Components/Popover/BitPopover.razor.cs
src/BitBlazor/Components/Popover/BitPopover.razor.js      ← new
```

The SDK publishes collocated files at
`_content/BitBlazor/Components/<Name>/<Component>.razor.js`.
`wwwroot/js/bitblazor-interop.js` is removed.

### Loading Bootstrap Italia as a module

`bootstrap-italia.bundle.min.js` is a UMD bundle. When evaluated as an ES module,
`this` is `undefined` and neither `exports` nor `define` exist, so the wrapper falls back
to `globalThis.bootstrap = factory()`. The factory body is already `"use strict"`, so
module semantics do not change runtime behaviour. The file parses as a module
(`node --check` on a `.mjs` copy).

The import is **side-effect only** (no ES exports); the plugins are read from
`window.bootstrap`. Because the module map evaluates a URL once, concurrent imports from
`BitTooltip.razor.js` and `BitPopover.razor.js` share a single fetch and evaluation.

```js
// BitTooltip.razor.js
const BUNDLE_URL = '../../bootstrap-italia/js/bootstrap-italia.bundle.min.js';
const SUPPORTED_VERSION = '2.18'; // major.minor of the shipped bundle

let versionChecked = false;

async function ensureBootstrap() {
    if (!window.bootstrap) {
        // Not provided by the host page: load the library's own copy.
        await import(BUNDLE_URL);
    } else if (!versionChecked) {
        // Provided by the host page: reuse it, but flag unsupported versions.
        const detected = window.BOOTSTRAP_ITALIA_VERSION;
        if (!detected?.startsWith(`${SUPPORTED_VERSION}.`)) {
            console.warn(
                `[BitBlazor] Bootstrap Italia ${detected ?? '(unknown)'} detected; ` +
                `supported version is ${SUPPORTED_VERSION}.x. Use at your own risk.`);
        }
    }
    versionChecked = true;

    if (!window.bootstrap?.Tooltip) {
        throw new Error('[BitTooltip] Bootstrap Italia Tooltip plugin is not available.');
    }
}

function clean(options) {
    return Object.fromEntries(
        Object.entries(options ?? {}).filter(([, v]) => v !== null && v !== undefined));
}

export const BitTooltip = {
    /**
     * Creates a Bootstrap Italia Tooltip on the first child of the wrapper.
     * @param {HTMLElement} wrapper - The <bit-tooltip> wrapper element.
     * @param {string} title - The tooltip text.
     * @param {object} options - Tooltip options (Bootstrap JS API).
     * @returns {Promise<{ dispose: () => void } | null>} A handle bound to the created instance.
     */
    async create(wrapper, title, options) {
        const target = wrapper?.firstElementChild;
        if (!target) {
            console.warn('[BitTooltip] No child element found for tooltip.');
            return null;
        }

        await ensureBootstrap();

        target.setAttribute('title', title);
        const instance = bootstrap.Tooltip.getOrCreateInstance(target, clean(options));

        return {
            dispose: () => instance.dispose()
        };
    }
};
```

`BitPopover.razor.js` follows the same shape:
`BitPopover.create(wrapper, title, content, options)`.

### Instance handle instead of DOM lookup on dispose

`create` returns a small handle bound to the **created instance**. .NET keeps it as an
`IJSObjectReference` and disposes it directly, removing the `firstElementChild` lookup at
dispose time (fixes P4). Returning a handle rather than the raw Bootstrap instance keeps
the interop surface minimal and leaves room for a future `update(...)` method.

### .NET side

```csharp
private const string ModulePath = "./_content/BitBlazor/Components/Tooltip/BitTooltip.razor.js";

private IJSObjectReference? _module;
private IJSObjectReference? _instance;
private ElementReference _wrapperRef;   // renamed from _spanRef
private bool _disposed;

protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (!firstRender) return;

    _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
    var instance = await _module.InvokeAsync<IJSObjectReference?>(
        "BitTooltip.create", _wrapperRef, Text, BuildOptions());

    if (_disposed)
    {
        // Disposed while awaiting: release immediately (fixes P5).
        await ReleaseAsync(instance);
        return;
    }

    _instance = instance;
}

public async ValueTask DisposeAsync()
{
    _disposed = true;
    try
    {
        await ReleaseAsync(_instance);
        if (_module is not null) await _module.DisposeAsync();
    }
    catch (JSDisconnectedException) { }
    catch (TaskCanceledException) { }
}

private static async Task ReleaseAsync(IJSObjectReference? instance)
{
    if (instance is null) return;
    await instance.InvokeVoidAsync("dispose");
    await instance.DisposeAsync();
}
```

Blazor resolves dotted identifiers (`"BitTooltip.create"`) by walking the module object.

### Upgrade policy

Upgrading the shipped Bootstrap Italia bundle is done in a single change that updates,
together: the bundle and CSS under `wwwroot/bootstrap-italia/`, `SUPPORTED_VERSION` in
every collocated module, and the supported version in the documentation. A Bootstrap
Italia **major** upgrade is a BitBlazor breaking change.

## Consequences

### Positive

- **Zero setup by default**: not a breaking change for consumers who never added the bundle.
- **Load-once guaranteed by the platform**: the ES module map removes the hand-written
  loader and its race (fixes P1, P2).
- **Lazy**: the ~1 MB bundle is fetched only when the first tooltip/popover renders.
- **Cohesion**: each component owns its JS, following the Blazor collocation convention
  (fixes P3).
- **Correct lifecycle**: dispose targets the exact instance created; disposal during a
  pending import is handled (fixes P4, P5).
- **Loud failure**: a missing plugin produces a clear error instead of a silent no-op (fixes P6).
- **Explicit support boundary**: declared supported version, runtime warning for other
  versions, single tested version to maintain (fixes P7). Bug reports can be triaged
  from the console warning.

### Negative / risks

| Risk | Mitigation |
|------|------------|
| **Double evaluation** if the host `<script>` is deferred, async, or placed after `blazor.*.js`: the guard runs before the host copy exists, imports the library copy, then the host copy evaluates too. | Document the placement rule; out-of-contract setups are the consumer's risk. |
| **Unsupported host version** may misbehave (renamed options are silently ignored by Bootstrap; markup may not match the CSS). | Runtime `console.warn`; documented as consumer's own risk. |
| **Version check depends on `window.BOOTSTRAP_ITALIA_VERSION`**: plain Bootstrap or future releases dropping the global report "unknown". | Warning still emitted (as unsupported); revisit on each upgrade. |
| **Path fragility**: the .NET `ModulePath` and the JS `BUNDLE_URL` depend on the folder layout. Moving a folder causes a runtime 404 that neither the build nor bUnit catches. | Keep paths in constants; smoke-test in the stories app; note it in the component authoring skill. |
| **CSP**: `import()` is governed by `script-src` like an injected `<script>`; nonce-only policies (without `'strict-dynamic'`) block it. | Document the requirement (`script-src 'self'` or `'strict-dynamic'`); bring-your-own with a nonced `<script>` is an alternative. |
| **AMD pages**: if RequireJS is present, the UMD wrapper registers with `define` and never sets `window.bootstrap`. | Rare and unchanged from today; the loud error makes it diagnosable. |
| **Bundle-wide side effects**: once loaded, the bundle registers document-level handlers that affect the whole page. | Unchanged from today; smoke-test `BitDropdown` and `BitModal` alongside a tooltip. |
| **Removing `bitblazor-interop.js`** removes a publicly reachable static asset (`_content/BitBlazor/js/bitblazor-interop.js`). | Call out in release notes. |
| **Duplication**: `ensureBootstrap`, `SUPPORTED_VERSION` and `clean` are repeated in both modules; the version may drift between them. | Accepted for two components; the upgrade policy updates both. Move to option E if a third Bootstrap-backed component is added. |
| **Browser verification limited to a standalone page**: module evaluation, single bundle load, dispose and the version warning were verified in headless Edge outside Blazor. | Smoke-test Server/WebAssembly and enhanced navigation in the sample/stories app. |

### Documentation impact

- README: add a "JavaScript" section next to the CSS setup, stating that no setup is
  required, the supported Bootstrap Italia version, the bring-your-own rules, and the CSP
  requirement.
- `docs/components/tooltip.md` and `docs/components/popover.md`: short note linking to
  that section.

### Testing impact

- `BitTooltipTest.Behaviors.cs` / `BitPopoverTest.Behaviors.cs` must target the new module
  paths and identifiers (`BitTooltip.create`, `BitPopover.create`); the captured options
  argument index is unchanged.
- `create` returns an `IJSObjectReference`; set it up in bUnit as a module-returning
  invocation so the handle's `dispose` call can be asserted.
- Add a test asserting that the handle is disposed when the component is disposed.

## Validation before acceptance

1. In the stories app, render a tooltip and a popover on the same page; verify the bundle
   is requested **once** (Network tab) and both work.
2. Repeat with Server and WebAssembly interactivity.
3. Navigate away and back (enhanced navigation); verify no popup is left orphaned.
4. Add the bundle manually (synchronous, before `blazor.*.js`); verify it is not evaluated
   a second time and no warning is logged.
5. Fake an unsupported version (`window.BOOTSTRAP_ITALIA_VERSION = '3.0.0'` before Blazor
   starts); verify the warning is logged once per module and components still work.
6. Smoke-test `BitDropdown` and `BitModal` alongside a tooltip.

## Open questions

- Should the components re-initialise when parameters (`Text`, `Placement`, …) change
  after first render? Out of scope here; the handle shape allows adding `update(...)` later.
- Should the tooltip default trigger become `hover focus` for keyboard accessibility?
  To be tracked separately.
- Should the supported range be widened beyond the shipped minor once more versions are
  tested?
