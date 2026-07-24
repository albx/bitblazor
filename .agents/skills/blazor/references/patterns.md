# BitBlazor Component Patterns

## General-Purpose Component

A general-purpose component (alert, badge, card, button, modal, …) inherits `BitComponentBase`. Keep markup in the `.razor` file and all logic (parameters, computed properties) in the `.razor.cs` partial class.

```razor
@* BitAlert.razor *@
@namespace BitBlazor.Components
@inherits BitComponentBase

<div class="@ComputeCssClasses()" role="alert" @attributes="AdditionalAttributes">
    @ChildContent
</div>
```

```csharp
// BitAlert.razor.cs
namespace BitBlazor.Components;

/// <summary>
/// Represents an alert component using Bootstrap Italia styles.
/// </summary>
public partial class BitAlert : BitComponentBase
{
    /// <summary>Gets or sets the content of the alert.</summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>Gets or sets the color variant of the alert.</summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("alert")
            .Add($"alert-{Color.ToCssClass()}");
        AddCustomCssClass(builder);
        return builder.Build();
    }
}
```

## Form Component

Form components inherit `BitFormComponentBase<T>`. They must:

- Override `FieldIdPrefix` to generate a unique `id` for the underlying input
- Override `SupportedTypes` to guard against unsupported `T` at construction time
- Wire `aria-invalid` and `aria-describedby` from `IsInvalid` (provided by the base class)

```razor
@* BitCheckbox.razor *@
@namespace BitBlazor.Form
@inherits BitFormComponentBase<bool>

<div class="form-check">
    <input class="form-check-input"
           type="checkbox"
           id="@FieldId"
           checked="@Value"
           disabled="@Disabled"
           aria-invalid="@(IsInvalid ? "true" : null)"
           aria-describedby="@(IsInvalid ? $"{FieldId}-error" : null)"
           @onchange="OnValueChangedAsync"
           @attributes="AdditionalAttributes" />
    <label class="form-check-label" for="@FieldId">
        @Label
        @if (Required) { <span aria-hidden="true"> *</span> }
    </label>
    @if (IsInvalid)
    {
        <span id="@FieldId-error" role="alert" class="invalid-feedback d-block">
            <ValidationMessage For="@For" />
        </span>
    }
</div>
```

```csharp
// BitCheckbox.razor.cs
namespace BitBlazor.Form;

/// <summary>
/// Represents a checkbox form component using Bootstrap Italia styles.
/// </summary>
public partial class BitCheckbox : BitFormComponentBase<bool>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix => "checkbox";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes => [typeof(bool)];

    private async Task OnValueChangedAsync(ChangeEventArgs e)
    {
        var newValue = e.Value is bool b && b;
        await ValueChanged.InvokeAsync(newValue);
    }
}
```

## CSS Class Composition

```csharp
// Good — CssClassBuilder with conditional classes
private string ComputeCssClasses()
{
    var builder = new CssClassBuilder("btn")
        .Add($"btn-{Color.ToCssClass()}")
        .Add("btn-lg", Size == Size.Large)
        .Add("btn-sm", Size == Size.Small)
        .Add("disabled", Disabled);
    AddCustomCssClass(builder);  // always before Build()
    return builder.Build();
}

// Bad — manual concatenation
string css = "btn btn-" + color + (disabled ? " disabled" : "");
```

`CssClassBuilder` deduplicates classes automatically. Use Bootstrap Italia CSS class names exclusively — do not invent custom class names.

## Performance Best Practices

**Use `@key` for list diffing:**

```razor
@foreach (var item in items)
{
    <ItemComponent @key="item.Id" Item="@item" />
}
```

**Override `ShouldRender` for pure display components:**

```csharp
protected override bool ShouldRender() => _isDirty;
```

Note: Virtualization (`<Virtualize>`) is a consuming-app concern — document it in the component's usage examples rather than enforcing it in the library itself.

## JS Interop

Use `IJSRuntime` async APIs only. Inject via DI when a component genuinely requires JavaScript (e.g., focus management, clipboard, resize observers).

```csharp
// BitXxx.razor.cs
[Inject] private IJSRuntime JS { get; set; } = default!;

private ElementReference _elementRef;

private async Task FocusAsync()
    => await JS.InvokeVoidAsync("BitBlazor.focusElement", _elementRef);
```

```javascript
// wwwroot/js/bitblazor.js
window.BitBlazor = {
    focusElement: (el) => el?.focus()
};
```

Always call JS interop from `OnAfterRenderAsync` (never `OnInitializedAsync`) so the DOM element exists before the call is made.
