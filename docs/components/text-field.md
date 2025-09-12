# BitTextField

The `BitTextField` component represents a text input field using Bootstrap Italia styles for forms.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitTextField` component is designed to handle string input values and provides built-in support for form integration and validation. It supports various input types (text, email, tel, url) and offers features like input groups with prepend/append content, different sizes, and accessibility attributes.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `string?` | ✗ | `null` | The current value of the text field |
| `ValueChanged` | `EventCallback<string?>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<string?>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the input field |
| `Type` | `TextFieldType` | ✗ | `TextFieldType.Text` | The type of the text field (Text, Email, Tel, Url) |
| `Placeholder` | `string?` | ✗ | `null` | Placeholder text displayed when the field is empty |
| `Size` | `Size` | ✗ | `Size.Default` | The size of the text field |
| `Disabled` | `bool` | ✗ | `false` | Whether the text field is disabled |
| `Readonly` | `bool` | ✗ | `false` | Whether the text field is readonly |
| `Plaintext` | `bool` | ✗ | `false` | Whether to render as plain text instead of input |
| `PrependContent` | `RenderFragment?` | ✗ | `null` | Content displayed before the input (creates input group) |
| `AppendContent` | `RenderFragment?` | ✗ | `null` | Content displayed after the input (creates input group) |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the input |
| `AdditionalTextId` | `string?` | ✗ | `null` | ID for the additional text (used for aria-describedby) |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Used Enumerations

### TextFieldType

**Namespace**: `BitBlazor.Form`

Specifies the type of input field for a text-based form element.

| Value | Description | HTML Input Type |
|-------|-------------|-----------------|
| `Text` | Plain text input | `text` |
| `Email` | Email address input | `email` |
| `Tel` | Telephone number input | `tel` |
| `Url` | URL input | `url` |

## Usage Examples

### Basic text field

```razor
<BitTextField Label="Full Name" 
              @bind-Value="model.FullName" />
```

### Email input field

```razor
<BitTextField Label="Email Address" 
              Type="TextFieldType.Email"
              @bind-Value="model.Email" 
              Placeholder="user@example.com" />
```

### Text field with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitTextField Label="Username" 
                  @bind-Value="model.Username"
                  For="@(() => model.Username)" />
                  
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private UserModel model = new();
    
    private class UserModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Text field with input group

```razor
<BitTextField Label="Website URL" 
              Type="TextFieldType.Url"
              @bind-Value="model.Website">
    <PrependContent>
        <span class="input-group-text">https://</span>
    </PrependContent>
    <AppendContent>
        <div class="input-group-append">
            <BitButton Color="Color.Primary" Size="Size.Small">
                <BitIcon IconName="@Icons.ItExternalLink" />
            </BitButton>
        </div>
    </AppendContent>
</BitTextField>
```

### Text field with additional text

```razor
<BitTextField Label="Password" 
              @bind-Value="model.Password"
              AdditionalTextId="pwd-help">
    <AdditionalText>
        Password must be at least 8 characters long and contain uppercase, lowercase, and special characters.
    </AdditionalText>
</BitTextField>
```

### Different sizes

```razor
<BitTextField Label="Small Field" 
              Size="Size.Small"
              @bind-Value="smallValue" />

<BitTextField Label="Default Field" 
              @bind-Value="defaultValue" />

<BitTextField Label="Large Field" 
              Size="Size.Large"
              @bind-Value="largeValue" />
```

### Readonly and plaintext modes

```razor
<BitTextField Label="Readonly Field" 
              Value="Read-only content"
              Readonly="true" />

<BitTextField Label="Plaintext Field" 
              Value="Plain text content"
              Plaintext="true" />
```

## Generated HTML Structure

### Basic text field

```html
<div class="form-group">
    <label for="text-12345" class="active">Full Name</label>
    <input type="text" 
           class="form-control" 
           name="text-12345" 
           value="John Doe" />
</div>
```

### Text field with input group

```html
<div class="form-group">
    <div class="input-group">
        <span class="input-group-text">https://</span>
        <label for="text-12345" class="active">Website URL</label>
        <input type="url" 
               class="form-control" 
               name="text-12345" 
               value="example.com" />
        <button type="button" class="btn btn-primary btn-sm">
            <svg class="icon">...</svg>
        </button>
    </div>
</div>
```

### Text field with validation error

```html
<div class="form-group">
    <label for="text-12345" class="active">Username</label>
    <input type="text" 
           class="form-control" 
           name="text-12345" 
           aria-describedby="text-12345-validation" />
    <div class="is-invalid" id="text-12345-validation">
        The Username field is required.
    </div>
</div>
```

## Generated CSS Classes

### Input element classes

- `form-control` - Base input styling
- `form-control-lg` - Large size styling (when Size="Large")
- `form-control-sm` - Small size styling (when Size="Small")
- `form-control-plaintext` - Plain text styling (when Plaintext="true")

### Label classes

- `active` - Applied when the field has content or placeholder

### Container classes

- `form-group` - Container for the entire field
- `input-group` - Applied when PrependContent or AppendContent is used

## Accessibility

- Automatically associates labels with inputs using the `for` attribute
- Supports `aria-describedby` when AdditionalText is provided
- Maintains focus management and keyboard navigation
- Compatible with screen readers
- Supports validation message announcements

## Form Integration

- Full support for `EditForm` and model binding
- Compatible with `DataAnnotationsValidator`
- Integrates with `ValidationSummary` and `ValidationMessage<T>`
- Supports `For` expression for validation binding
- Automatic validation state styling

## Notes

- The component automatically generates unique IDs for form fields using the "text" prefix
- Labels become "active" (styled differently) when the field has content or a placeholder
- Input groups are automatically created when PrependContent or AppendContent is provided
- The component extends `BitInputFieldBase<string?>` for consistent form behavior
- All Bootstrap Italia form styling is automatically applied
- The component supports all standard HTML input attributes through `AdditionalAttributes`
