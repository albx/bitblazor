# BitTextAreaField

The `BitTextAreaField` component represents a multi-line text input field using Bootstrap Italia styles for forms.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitTextAreaField` component is designed to handle user input for multi-line text fields. It supports binding to a nullable string value and provides additional functionality such as placeholder text, label activation based on the input state, and customizable row height.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `string?` | ✗ | `null` | The current value of the text area field |
| `ValueChanged` | `EventCallback<string?>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<string?>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the text area field |
| `Placeholder` | `string?` | ✗ | `null` | Placeholder text displayed when the field is empty |
| `Rows` | `int` | ✗ | `1` | The number of rows to display (determines height) |
| `Size` | `Size` | ✗ | `Size.Default` | The size of the text area field |
| `Disabled` | `bool` | ✗ | `false` | Whether the text area field is disabled |
| `Readonly` | `bool` | ✗ | `false` | Whether the text area field is readonly |
| `Plaintext` | `bool` | ✗ | `false` | Whether to render as plain text instead of textarea |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the textarea |
| `AdditionalTextId` | `string?` | ✗ | `null` | ID for the additional text (used for aria-describedby) |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Usage Examples

### Basic text area field

```razor
<BitTextAreaField Label="Description" 
                  Rows="4"
                  @bind-Value="model.Description" />
```

### Text area field with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitTextAreaField Label="Comments" 
                      Rows="6"
                      @bind-Value="model.Comments"
                      For="@(() => model.Comments)"
                      Placeholder="Please provide your feedback..." />
                      
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit Feedback
    </BitButton>
</EditForm>

@code {
    private FeedbackModel model = new();
    
    private class FeedbackModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Comments { get; set; } = string.Empty;
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Text area field with character limit guidance

```razor
<BitTextAreaField Label="Bio" 
                  Rows="5"
                  @bind-Value="model.Bio"
                  AdditionalTextId="bio-help"
                  Placeholder="Tell us about yourself...">
    <AdditionalText>
        Maximum 500 characters. Currently: @(model.Bio?.Length ?? 0) characters.
    </AdditionalText>
</BitTextAreaField>
```

### Different sizes and configurations

```razor
<!-- Small text area -->
<BitTextAreaField Label="Short Note" 
                  Size="Size.Small"
                  Rows="2"
                  @bind-Value="shortNote" />

<!-- Default text area -->
<BitTextAreaField Label="Message" 
                  Rows="4"
                  @bind-Value="message" />

<!-- Large text area -->
<BitTextAreaField Label="Detailed Description" 
                  Size="Size.Large"
                  Rows="8"
                  @bind-Value="description" />

<!-- Readonly text area -->
<BitTextAreaField Label="Terms and Conditions" 
                  Rows="10"
                  Value="@termsAndConditions"
                  Readonly="true" />
```

### Contact form example

```razor
<EditForm Model="contactModel" OnValidSubmit="HandleContact">
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-md-6">
            <BitTextField Label="Full Name" 
                          @bind-Value="contactModel.FullName"
                          For="@(() => contactModel.FullName)" />
        </div>
        <div class="col-md-6">
            <BitTextField Label="Email Address" 
                          Type="TextFieldType.Email"
                          @bind-Value="contactModel.Email"
                          For="@(() => contactModel.Email)" />
        </div>
        <div class="col-12">
            <BitTextField Label="Subject" 
                          @bind-Value="contactModel.Subject"
                          For="@(() => contactModel.Subject)" />
        </div>
        <div class="col-12">
            <BitTextAreaField Label="Message" 
                              Rows="6"
                              @bind-Value="contactModel.Message"
                              For="@(() => contactModel.Message)"
                              AdditionalTextId="msg-help"
                              Placeholder="Please describe your inquiry...">
                <AdditionalText>
                    Please provide as much detail as possible to help us assist you better.
                </AdditionalText>
            </BitTextAreaField>
        </div>
        <div class="col-12">
            <BitButton Type="ButtonType.Submit" 
                       Color="Color.Primary"
                       Icon="@Icons.ItMail">
                Send Message
            </BitButton>
        </div>
    </div>
    
    <ValidationSummary />
</EditForm>

@code {
    private ContactModel contactModel = new();
    
    private class ContactModel
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000, MinimumLength = 20)]
        public string Message { get; set; } = string.Empty;
    }
    
    private async Task HandleContact()
    {
        // Handle contact form submission
    }
}
```

### Blog post editor example

```razor
<EditForm Model="postModel" OnValidSubmit="HandleSavePost">
    <DataAnnotationsValidator />
    
    <BitTextField Label="Post Title" 
                  @bind-Value="postModel.Title"
                  For="@(() => postModel.Title)" />
    
    <BitTextAreaField Label="Post Content" 
                      Rows="15"
                      @bind-Value="postModel.Content"
                      For="@(() => postModel.Content)"
                      AdditionalTextId="content-help"
                      Placeholder="Write your blog post content here...">
        <AdditionalText>
            <div class="d-flex justify-content-between">
                <span>Supports Markdown formatting</span>
                <span class="@(GetCharacterCountClass())">
                    @(postModel.Content?.Length ?? 0) / 10,000 characters
                </span>
            </div>
        </AdditionalText>
    </BitTextAreaField>
    
    <div class="d-flex gap-2">
        <BitButton Type="ButtonType.Submit" 
                   Color="Color.Primary"
                   Icon="@Icons.ItSave">
            Save Draft
        </BitButton>
        <BitButton Color="Color.Success" 
                   Icon="@Icons.ItCheck">
            Publish
        </BitButton>
    </div>
    
    <ValidationSummary />
</EditForm>

@code {
    private PostModel postModel = new();
    
    private class PostModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(10000, MinimumLength = 100)]
        public string Content { get; set; } = string.Empty;
    }
    
    private string GetCharacterCountClass()
    {
        var length = postModel.Content?.Length ?? 0;
        return length > 9000 ? "text-danger" : length > 8000 ? "text-warning" : "text-muted";
    }
    
    private async Task HandleSavePost()
    {
        // Handle save post
    }
}
```

## Generated HTML Structure

### Basic text area field

```html
<div class="form-group">
    <label for="textarea-12345" class="active">Description</label>
    <textarea class="form-control" 
              name="textarea-12345" 
              rows="4">Sample content</textarea>
</div>
```

### Text area field with validation error

```html
<div class="form-group">
    <label for="textarea-12345" class="active">Comments</label>
    <textarea class="form-control" 
              name="textarea-12345" 
              rows="6"
              aria-describedby="textarea-12345-validation">Short</textarea>
    <div class="is-invalid" id="textarea-12345-validation">
        The field Comments must be a string with a minimum length of 10 and a maximum length of 1000.
    </div>
</div>
```

### Text area field with additional text

```html
<div class="form-group">
    <label for="textarea-12345" class="active">Bio</label>
    <textarea class="form-control" 
              name="textarea-12345" 
              rows="5"
              aria-describedby="bio-help" 
              placeholder="Tell us about yourself...">User bio content</textarea>
    <small id="bio-help" class="form-text">
        Maximum 500 characters. Currently: 16 characters.
    </small>
</div>
```

## Generated CSS Classes

### Textarea element classes

- `form-control` - Base textarea styling
- `form-control-lg` - Large size styling (when Size="Large")
- `form-control-sm` - Small size styling (when Size="Small")
- `form-control-plaintext` - Plain text styling (when Plaintext="true")

### Label classes

- `active` - Applied when the field has content or placeholder

### Container classes

- `form-group` - Container for the entire field

### Additional text classes

- `form-text` - Bootstrap styling for helper text

## Accessibility

- Automatically associates labels with textarea using the `for` attribute
- Supports `aria-describedby` when AdditionalText is provided
- Maintains focus management and keyboard navigation
- Compatible with screen readers
- Supports validation message announcements
- Proper semantic HTML structure with `<textarea>` element

## Form Integration

- Full support for `EditForm` and model binding
- Compatible with `DataAnnotationsValidator`
- Integrates with `ValidationSummary` and `ValidationMessage<T>`
- Supports `For` expression for validation binding
- Automatic validation state styling

## Height Management

The component allows flexible height control through the `Rows` parameter.

## Notes

- The component automatically generates unique IDs for form fields using the "textarea" prefix
- Labels become "active" (styled differently) when the field has content or a placeholder
- The `rows` attribute determines the initial height of the textarea
- Users can resize the textarea if the browser allows it (default behavior)
- The component extends `BitInputFieldBase<string?>` for consistent form behavior
- All Bootstrap Italia form styling is automatically applied
- The component supports all standard HTML textarea attributes through `AdditionalAttributes`
- Vertical scrolling is automatically handled by the browser when content exceeds the height
- The component is optimized for both short and long-form text input scenarios
