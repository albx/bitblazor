# BitBlazor Anti-Patterns

| Anti-Pattern | Why It's Bad | Better Approach |
|--------------|--------------|-----------------|
| Inheriting `ComponentBase` directly | Loses `CssClass`, `Id`, `AdditionalAttributes`, `AddCustomCssClass()` | Inherit `BitComponentBase`, `BitFormComponentBase<T>`, or `BitInputFieldBase<T>` |
| Manual CSS string concatenation | Fragile, duplicates classes, no deduplication | Use `CssClassBuilder` |
| Raw `string` for color/size parameters | No type safety, breaks design tokens | Use `Color`, `Size`, `Variant` enums |
| `<div>` or `<span>` for interactive roles | Not keyboard operable, no built-in semantics | Use native `<button>`, `<input>`, `<a>` |
| Forgetting `AddCustomCssClass()` before `Build()` | Consumers cannot inject custom classes via the `CssClass` parameter | Always call it as the last step before `Build()` |
| Omitting XML `<summary>` on `[Parameter]` members | No IntelliSense for library consumers | Document every `[Parameter]` with `<summary>` |
| Missing `aria-invalid` / `aria-describedby` on form inputs | Screen readers cannot announce validation errors | Wire from `IsInvalid` — see patterns.md Form Component section |
| Missing `[EditorRequired]` on required parameters | Silent null-reference failures at runtime | Mark required parameters with `[EditorRequired]` |
| Omitting `@attributes="AdditionalAttributes"` on the root element | Consumers cannot pass arbitrary HTML attributes | Forward `AdditionalAttributes` on the root element |
| Ignoring `ShouldRender` on pure display components | Unnecessary re-renders when used in large lists | Override `ShouldRender()` when render frequency should be controlled |
| Synchronous JS interop | Blocks the SignalR circuit in Server mode or WASM thread | Use `IJSRuntime` async APIs exclusively |
| Calling JS interop in `OnInitializedAsync` | No DOM element exists during server prerender | Call JS only from `OnAfterRenderAsync(firstRender: true)` |
| Mixing logic into `.razor` markup file | Hard to test and review; obscures the component's API surface | Put all `[Parameter]` declarations, state, and computed properties in `.razor.cs` |
