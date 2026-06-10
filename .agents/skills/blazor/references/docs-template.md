# Documentation Page Template

Every component must have a documentation page in `docs/`. The file location mirrors the component's namespace:

- `BitBlazor.Components` → `docs/components/bit-xxx.md`
- `BitBlazor.Form` → `docs/form/bit-xxx.md`

File names use kebab-case: `bit-text-field.md`, `bit-button-badge.md`.

## Required Structure

Use the following section order (see `docs/components/button.md` as a reference):

```markdown
# BitXxx

The `BitXxx` component represents a [brief description with Bootstrap Italia link](https://italia.github.io/bootstrap-italia/docs/...).

## Namespace

​```csharp
BitBlazor.Components   (or BitBlazor.Form)
​```

## Description

One or two sentences describing what the component does and when to use it.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | The content rendered inside the component |
| `Color` | `Color` | ✓ | - | The color variant |
| `Size` | `Size` | ✗ | `Size.Default` | The size of the component |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply to the root element |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the root element |

## Used Enumerations

Document every enum type referenced in the Parameters table.

### Color

| Value | Description |
|-------|-------------|
| `Primary` | Primary color |
| `Secondary` | Secondary color |
| `Success` | Success / positive color |
| `Warning` | Warning color |
| `Danger` | Error / negative color |

### Size

| Value | Description |
|-------|-------------|
| `Default` | Standard size |
| `Small` | Reduced size |
| `Large` | Enlarged size |

## Usage Examples

### Basic usage

​```razor
<BitXxx Color="Color.Primary">
    Content
</BitXxx>
​```

### [Variant name]

​```razor
<BitXxx Color="Color.Secondary" Variant="Variant.Outline">
    Content
</BitXxx>
​```

### With additional attributes

​```razor
<BitXxx Color="Color.Primary" data-testid="my-component">
    Content
</BitXxx>
​```
```

## Conventions

- **Link to Bootstrap Italia** in the opening sentence where the component maps to a Bootstrap Italia pattern
- **List all `[Parameter]` properties** including `CssClass` and `AdditionalAttributes` inherited from the base class
- **Mark `[EditorRequired]` parameters** with ✓ in the Required column
- **Enum tables** list every `Value` a consumer is expected to use (skip internal or future-only values)
- **Usage Examples**: at least one example per meaningful visual variant (color, size, disabled, with icon, etc.)
- **Do not** document internal computed properties or private fields — only `[Parameter]`-decorated members
