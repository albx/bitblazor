---
name: blazor
description: "Author, extend, and review BitBlazor library components — accessible, Bootstrap Italia-styled Blazor UI kit components for .NET 9+. USE FOR: creating a new Bit component, extending an existing component, adding a form field, reviewing component markup or parameters, writing bUnit tests for a component. DO NOT USE FOR: unrelated stacks or building Blazor applications with BitBlazor (not authoring the library itself). INVOKES: inspect the repository context, edit targeted files, and run relevant build and test commands when changes are made."
compatibility: "Requires .NET 9+. Library targets net9.0 and uses Bootstrap Italia for all styling."
---

# BitBlazor Component Authoring

## Trigger On

- creating a new `Bit` prefixed component (e.g., `BitAlert`, `BitTextField`)
- extending an existing BitBlazor component with new parameters or behaviour
- adding a form field component that integrates with `EditForm` and `EditContext`
- reviewing component markup, parameter declarations, or CSS class composition
- writing or reviewing bUnit tests for a library component
- integrating with JavaScript from a library component

## Documentation

- [Blazor Overview](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-10.0)
- [Blazor Component Lifecycle](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-10.0)
- [Blazor Performance Best Practices](https://learn.microsoft.com/en-us/aspnet/core/blazor/performance?view=aspnetcore-10.0)
- [Blazor JS Interop](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/?view=aspnetcore-10.0)
- [Bootstrap Italia](https://italia.github.io/bootstrap-italia/docs)
- [bUnit Testing](https://bunit.dev/docs/getting-started/)

### References

- [patterns.md](references/patterns.md) — General-purpose and form component code patterns, CSS class composition, performance, JS interop
- [anti-patterns.md](references/anti-patterns.md) — BitBlazor-specific mistakes and how to avoid them
- [testing.md](references/testing.md) — bUnit testing patterns and conventions
- [stories.md](references/stories.md) — BlazingStory file structure and conventions
- [docs-template.md](references/docs-template.md) — Documentation page template and structure

## Component Authoring Workflow

1. **Choose the right base class** (see Base Class Hierarchy below)
2. **Create the two component files** in `src/BitBlazor/Components/<Name>/` or `src/BitBlazor/Form/<Name>/`:
   - `BitXxx.razor` — markup only; inline `@code` for trivial render helpers
   - `BitXxx.razor.cs` — partial class with all `[Parameter]` declarations, private state, and computed properties
3. **Declare parameters** with XML `<summary>` docs; use enum types (`Color`, `Size`, `Variant`) instead of raw strings
4. **Compose the CSS class string** using `CssClassBuilder`; always call `AddCustomCssClass()` before `Build()`
5. **Write semantic markup** using native HTML elements; forward unknown attributes via `@attributes="AdditionalAttributes"`
6. **Wire ARIA attributes** for interactive and form components (see [Accessibility](#accessibility) section)
7. **Create a test file** in `tests/BitBlazor.Test/Components/<Name>/` or `tests/BitBlazor.Test/Form/<Name>/` — see [testing.md](references/testing.md)
8. **Create a story file** mirroring the component's namespace under `stories/BitBlazor.Stories/Components/Stories/` — see [stories.md](references/stories.md)
9. **Write the documentation page** in `docs/components/` or `docs/form/` — see [docs-template.md](references/docs-template.md)

## Base Class Hierarchy

Choose the base class that matches the component's role:

| Base Class | When to Use | Key Members Provided |
|------------|-------------|----------------------|
| `BitComponentBase` | General-purpose display and interactive components (alert, badge, card, button, …) | `CssClass`, `Id`, `AdditionalAttributes`, `AddCustomCssClass()` |
| `BitFormComponentBase<T>` | Form components that bind a value | Everything above + `Label`, `Value`, `ValueChanged`, `ValueExpression`, `Required`, `Disabled`, `AdditionalText`, `AdditionalTextId`, `CurrentEditContext` |
| `BitInputFieldBase<T>` | Text-like input fields | Everything above + `Readonly`, `Plaintext`, `Placeholder`, `Size` |

**Never inherit directly from `ComponentBase`.** See [patterns.md](references/patterns.md) for full component examples.

## CSS Class Composition

Use `CssClassBuilder` (from `BitBlazor.Core`) to compose CSS class strings — never concatenate manually. Always call `AddCustomCssClass()` before `Build()`:

```csharp
var builder = new CssClassBuilder("btn")
    .Add($"btn-{Color.ToCssClass()}")
    .Add("btn-sm", Size == Size.Small);
AddCustomCssClass(builder);
return builder.Build();
```

Use Bootstrap Italia CSS class names exclusively. See [patterns.md](references/patterns.md) for complete examples.


## Design Token Enums

Always use the project's enum types as parameter types — never raw strings:

| Enum | Values | Example Parameter |
|------|--------|-------------------|
| `Color` | `Primary`, `Secondary`, `Success`, `Warning`, `Danger`, … | `public Color Color { get; set; }` |
| `Size` | `Default`, `Small`, `Large` | `public Size Size { get; set; } = Size.Default;` |
| `Variant` | `Solid`, `Outline`, `Ghost`, … | `public Variant Variant { get; set; } = Variant.Solid;` |
| `Ratio` | Aspect ratio values | `public Ratio Ratio { get; set; }` |
| `Typography` | Typography scale values | `public Typography Typography { get; set; }` |

## Render Mode Compatibility

Library components must work in **all render modes** used by the consuming application. Follow these rules:

| Concern | Rule |
|---------|------|
| JS Interop | Always use `IJSRuntime` async APIs; never synchronous interop |
| State | Components are stateless by design — values flow in via parameters, changes flow out via `EventCallback` |
| No DB / HTTP calls | Library components never access services directly; they are pure UI |
| `EventCallback` | Use `EventCallback` (not `Action`/`Func`) for callbacks to ensure correct rendering |

## Accessibility

Accessibility is a **first-class requirement** for every component in this library. The authoritative ruleset is [`a11y.instructions.md`](../../../.github/instructions/a11y.instructions.md), which is automatically active on every `.razor`, `.razor.cs`, and `.razor.css` edit — read it before authoring any interactive component.

The checks most commonly missed when creating a new BitBlazor component are:

| Check | Rule |
|-------|------|
| **BL1** — `@onclick` on `<div>` or `<span>` | Always use `<button>`. If unavoidable, add `role="button"`, `tabindex="0"`, and handle Enter + Space via `@onkeydown` |
| **BL4** — Modal/overlay close without focus restoration | Store an `ElementReference` to the trigger; call `FocusAsync()` on close |
| **BL5** — `EditForm` inputs missing `aria-invalid` / `aria-describedby` | Wire both attributes from `IsInvalid` — see [patterns.md](references/patterns.md) |
| **BL6** — `@if` revealing interactive content without focus management | After showing content call `FocusAsync()` with `await Task.Yield()` to let Blazor flush the DOM first |

Additionally, for every component:
- Icon-only interactive elements **must** have `aria-label`
- Never remove `outline` in `.razor.css` without providing a `:focus-visible` replacement
- Colour must not be the **sole** indicator of state (error, success, disabled)

See [anti-patterns.md](references/anti-patterns.md) for a full list of BitBlazor-specific mistakes to avoid.

See [patterns.md](references/patterns.md) for performance best practices and JS interop patterns.

See [stories.md](references/stories.md) for the BlazingStory file template and conventions.

See [testing.md](references/testing.md) for bUnit test examples and conventions.

## Deliver

- `BitXxx.razor` + `BitXxx.razor.cs` inheriting the correct base class
- all parameters documented with XML `<summary>` comments
- CSS classes composed via `CssClassBuilder` with `AddCustomCssClass()` before `Build()`
- semantic HTML with `@attributes="AdditionalAttributes"` on the root element
- ARIA attributes wired correctly for interactive and form components
- a bUnit test file — see [testing.md](references/testing.md)
- a BlazingStory file — see [stories.md](references/stories.md)
- a documentation page — see [docs-template.md](references/docs-template.md)

## Validate

- component builds without warnings (`dotnet build`)
- all bUnit tests pass (`dotnet test`)
- component renders correctly with `CssClass` injected by a consumer
- form components announce validation errors via `aria-invalid` and `aria-describedby`
- icon-only interactive elements expose `aria-label`
- no `outline: none` without a `:focus-visible` replacement in `.razor.css`
- all BL1–BL6 checks from `a11y.instructions.md` pass for the new component
- documentation page exists in `docs/` with parameter table and at least one usage example
