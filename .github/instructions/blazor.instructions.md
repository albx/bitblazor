---
description: 'Blazor UI kit library component authoring patterns and conventions for BitBlazor'
applyTo:
  - '**/*.razor'
  - '**/*.razor.cs'
  - '**/*.razor.css'
---

## Project Context

BitBlazor is a UI kit library providing accessible, reusable Blazor components styled with **Bootstrap Italia**. All conventions in this file apply to library component authoring, not application development.

## Blazor Code Style and Structure

- Write idiomatic and efficient Blazor and C# code.
- Follow .NET and Blazor conventions.
- Use Razor Components appropriately for component-based UI development.
- Prefer inline `@code` blocks only for trivial rendering helpers; place all non-trivial logic in a `.razor.cs` code-behind partial class.
- Async/await should be used where applicable to ensure non-blocking UI operations.

## Naming Conventions

- Follow PascalCase for component names, method names, and public members.
- Use camelCase for private fields and local variables.
- Prefix interface names with "I" (e.g., `IUserService`).
- **All library components must be prefixed with `Bit`** (e.g., `BitButton`, `BitModal`, `BitTextField`).
- Component file names must match the component class name exactly (e.g., `BitButton.razor` + `BitButton.razor.cs`).

## Component Architecture and Base Classes

Every library component must inherit from the appropriate base class:

- **`BitComponentBase`** — base for all non-form components. Provides `CssClass`, `Id`, and `AdditionalAttributes`.
- **`BitFormComponentBase<T>`** — base for form components that bind a value. Provides `Label`, `Value`, `ValueChanged`, `ValueExpression`, and `EditContext` integration.
- **`BitInputFieldBase<T>`** — base for text-like input fields. Extends `BitFormComponentBase<T>` with `Readonly`, `Plaintext`, `Placeholder`, and `Size`.

Never inherit directly from `ComponentBase` for library components.

### Partial Class Pattern

Split every non-trivial component into two files:

- `BitXxx.razor` — markup and `@code` rendering helpers only.
- `BitXxx.razor.cs` — `partial class` with all `[Parameter]` declarations, private state, and computed properties.

### Attribute Splatting

Always forward unknown attributes to the root HTML element via `AdditionalAttributes` (already declared on `BitComponentBase`):

```razor
<button @attributes="AdditionalAttributes">...</button>
```

### Required Parameters

Mark parameters that have no sensible default as `[EditorRequired]`:

```csharp
[Parameter]
[EditorRequired]
public RenderFragment ChildContent { get; set; }
```

## CSS Class Building

Use `CssClassBuilder` (from `BitBlazor.Core`) to compose CSS class strings. Never concatenate class strings manually:

```csharp
// Good
var builder = new CssClassBuilder("btn")
    .Add($"btn-{Color.ToCssClass()}")
    .Add("btn-sm", Size == Size.Small);
AddCustomCssClass(builder);
return builder.Build();

// Bad
string cssClass = "btn btn-" + color + (small ? " btn-sm" : "");
```

Always call `AddCustomCssClass(builder)` before `Build()` to allow consumers to inject custom classes.

## Design Tokens and Enums

Use the provided enum types as parameter types instead of raw strings or primitives:

| Enum | Purpose |
|------|---------|
| `Color` | Color variants (Primary, Secondary, Success, …) |
| `Size` | Size variants (Default, Small, Large) |
| `Variant` | Visual style (Solid, Outline, Ghost, …) |
| `Ratio` | Aspect ratio |
| `Typography` | Typography scale |

```csharp
// Good
[Parameter]
public Color Color { get; set; }

// Bad
[Parameter]
public string Color { get; set; } = "primary";
```

## Bootstrap Italia Styling

Components are styled exclusively with **Bootstrap Italia** CSS classes. Do not introduce custom CSS that duplicates or overrides Bootstrap Italia utilities. Keep component-specific styles in `.razor.css` scoped files.

## CSS Isolation

Use `.razor.css` scoped stylesheets for component-specific rules that are not expressible via Bootstrap Italia classes:

- File must be named `BitXxx.razor.css` alongside `BitXxx.razor`.
- Only put styles here that are truly component-scoped; prefer Bootstrap Italia utility classes wherever possible.

## XML Documentation Comments

All public types, properties, and methods in `.razor.cs` files must have `<summary>` XML documentation. The project has `<GenerateDocumentationFile>true</GenerateDocumentationFile>` and documentation is published:

```csharp
/// <summary>
/// Gets or sets the color style of the button.
/// </summary>
[Parameter]
public Color Color { get; set; }
```

## Blazor and .NET Specific Guidelines

- Utilize Blazor's built-in component lifecycle methods (`OnInitializedAsync`, `OnParametersSetAsync`).
- Use data binding effectively with `@bind`.
- Leverage Dependency Injection for any services needed by components.
- Structure components following Separation of Concerns (markup in `.razor`, logic in `.razor.cs`).
- Target modern C# features (records, pattern matching, global usings) as the baseline — the library targets `net9.0`.

## Error Handling and Validation

- Capture component-level rendering errors with `ErrorBoundary` where appropriate.
- Implement form validation support via DataAnnotations and `EditContext` integration in `BitFormComponentBase<T>`.

## Performance Optimization

- Optimize Razor components by reducing unnecessary renders; use `ShouldRender()` where render frequency must be controlled.
- Minimize the component render tree depth.
- Use `EventCallback` (not `Action` or `Func`) for user interaction callbacks to ensure correct rendering behaviour.

## Accessibility

Library components have first-class accessibility responsibilities. Follow the companion `a11y.instructions.md` for full WCAG 2.2 AA rules, and enforce these within every component:

- Render semantic HTML elements (`<button>`, `<input>`, `<label>`, `<nav>`, etc.) — never `<div>` or `<span>` for interactive roles.
- Always associate `<label>` with its input via `for`/`id` or wrap the input inside the label.
- Icon-only interactive elements must expose an `aria-label`.
- Forward ARIA attributes through `AdditionalAttributes` so consumers can override them.
- Dynamic content changes must use live regions (`role="status"` / `role="alert"`) where appropriate.
- Ensure focus is visible and keyboard-operable for every interactive component.

## Testing

- Test components using **bUnit** alongside xUnit.
- If mocking dependencies is necessary, add and use a mocking library (for example Moq or NSubstitute) consistently.
- Cover parameter combinations, CSS class output, event callbacks, and accessibility attributes in tests.
