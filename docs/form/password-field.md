# BitPasswordField

The `BitPasswordField` component represents a password input field using Bootstrap Italia styles with built-in show/hide password functionality.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitPasswordField` component is designed to handle password input scenarios, providing built-in support for binding and validation of string values. It includes a toggle button to show/hide the password text and is optimized for use in forms and other data entry contexts.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `string?` | ✗ | `null` | The current value of the password field |
| `ValueChanged` | `EventCallback<string?>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<string?>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the password field |
| `Placeholder` | `string?` | ✗ | `null` | Placeholder text displayed when the field is empty |
| `Size` | `Size` | ✗ | `Size.Default` | The size of the password field |
| `Disabled` | `bool` | ✗ | `false` | Whether the password field is disabled |
| `Readonly` | `bool` | ✗ | `false` | Whether the password field is readonly |
| `Plaintext` | `bool` | ✗ | `false` | Whether to render as plain text instead of input |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the input |
| `AdditionalTextId` | `string?` | ✗ | `null` | ID for the additional text (used for aria-describedby) |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Usage Examples

### Basic password field

```razor
<BitPasswordField Label="Password" 
                  @bind-Value="model.Password" />
```

### Password field with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitPasswordField Label="Password" 
                      @bind-Value="model.Password"
                      For="@(() => model.Password)" />
                      
    <BitPasswordField Label="Confirm Password" 
                      @bind-Value="model.ConfirmPassword"
                      For="@(() => model.ConfirmPassword)" />
                      
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Create Account
    </BitButton>
</EditForm>

@code {
    private RegisterModel model = new();
    
    private class RegisterModel
    {
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must contain uppercase, lowercase, number and special character")]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    
    private async Task HandleSubmit()
    {
        // Handle registration
    }
}
```

### Password field with additional guidance

```razor
<BitPasswordField Label="New Password" 
                  @bind-Value="model.NewPassword"
                  AdditionalTextId="pwd-help">
    <AdditionalText>
        <strong>Password Requirements:</strong>
        <ul class="mb-0 mt-2">
            <li>At least 8 characters long</li>
            <li>Contains uppercase and lowercase letters</li>
            <li>Contains at least one number</li>
            <li>Contains at least one special character</li>
        </ul>
    </AdditionalText>
</BitPasswordField>
```

### Different sizes

```razor
<BitPasswordField Label="Small Password" 
                  Size="Size.Small"
                  @bind-Value="smallPassword" />

<BitPasswordField Label="Default Password" 
                  @bind-Value="defaultPassword" />

<BitPasswordField Label="Large Password" 
                  Size="Size.Large"
                  @bind-Value="largePassword" />
```

### Login form example

```razor
<EditForm Model="loginModel" OnValidSubmit="HandleLogin">
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-12">
            <BitTextField Label="Email or Username" 
                          Type="TextFieldType.Email"
                          @bind-Value="loginModel.Email"
                          For="@(() => loginModel.Email)" />
        </div>
        <div class="col-12">
            <BitPasswordField Label="Password" 
                              @bind-Value="loginModel.Password"
                              For="@(() => loginModel.Password)" />
        </div>
        <div class="col-12">
            <BitButton Type="ButtonType.Submit" 
                       Color="Color.Primary" 
                       Size="Size.Large"
                       CssClass="w-100">
                Sign In
            </BitButton>
        </div>
    </div>
    
    <ValidationSummary />
</EditForm>

@code {
    private LoginModel loginModel = new();
    
    private class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    
    private async Task HandleLogin()
    {
        // Handle login
    }
}
```

## Generated HTML Structure

### Basic password field

```html
<div class="form-group">
    <label for="pwd-12345" class="active">Password</label>
    <input type="password" 
           class="form-control input-password" 
           id="pwd-12345"
           name="pwd-12345" />
    <button type="button" 
            class="password-icon btn" 
            role="switch" 
            aria-checked="false">
        <span class="visually-hidden">show/hide password</span>
        <svg class="password-icon-visible icon icon-sm" aria-hidden="true"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-password-visible"></use></svg>
                    <svg class="password-icon-invisible icon icon-sm d-none" aria-hidden="true"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-password-invisible"></use></svg>
    </button>
</div>
```

### Password field with additional text

```html
<div class="form-group">
    <label for="pwd-12345" class="active">New Password</label>
    <input type="password" 
           class="form-control input-password" 
           id="pwd-12345"
           name="pwd-12345"
           aria-describedby="pwd-help" />
    <button type="button" 
            class="password-icon btn" 
            role="switch" 
            aria-checked="false">
        <span class="visually-hidden">show/hide password</span>
        <svg class="password-icon-visible icon icon-sm" aria-hidden="true"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-password-visible"></use></svg>
                    <svg class="password-icon-invisible icon icon-sm d-none" aria-hidden="true"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-password-invisible"></use></svg>
    </button>
    <p id="pwd-help" class="form-text text-muted d-block small pb-0">
        Password must be at least 8 characters long...
    </p>
</div>
```

## Generated CSS Classes

### Input element classes

- `form-control` - Base input styling
- `input-password` - Specific styling for password fields
- `form-control-lg` - Large size styling (when Size="Large")
- `form-control-sm` - Small size styling (when Size="Small")
- `form-control-plaintext` - Plain text styling (when Plaintext="true")

### Toggle button classes

- `password-icon` - Base styling for the toggle button
- `btn` - Bootstrap button styling

### Icon classes

- `password-icon-visible` - Show password icon (visible when password is hidden)
- `password-icon-invisible` - Hide password icon (visible when password is shown)
- `d-none` - Bootstrap utility class to hide inactive icon

### Label classes

- `active` - Applied when the field has content or placeholder

### Container classes

- `form-group` - Container for the entire field

## Accessibility

- Toggle button includes `role="switch"` for screen readers
- Toggle button state is communicated via `aria-checked` attribute
- Visually hidden text explains the button purpose: "show/hide password"
- Automatically associates labels with inputs using the `for` and `id` attributes
- Supports `aria-describedby` when AdditionalText is provided
- Icons include `aria-hidden="true"` to prevent screen reader confusion
- Maintains focus management and keyboard navigation
- Compatible with screen readers
- Supports validation message announcements

## Form Integration

- Full support for `EditForm` and model binding
- Compatible with `DataAnnotationsValidator`
- Integrates with `ValidationSummary` and `ValidationMessage<T>`
- Supports `For` expression for validation binding
- Automatic validation state styling

## Interactive Features

### Password Visibility Toggle

The component includes a built-in toggle button that allows users to show/hide the password text:

- **Hidden state**: Input type is "password", characters are masked
- **Visible state**: Input type is "text", characters are visible
- **Visual indicators**: Different icons show the current state
- **Accessibility**: Toggle state is announced to screen readers

### Icons Used

- **Password Visible Icon**: `Icons.ItPasswordVisible` - shown when password is hidden (clicking will show password)
- **Password Invisible Icon**: `Icons.ItPasswordInvisible` - shown when password is visible (clicking will hide password)

## Notes

- The component automatically generates unique IDs for form fields using the "pwd" prefix
- Labels become "active" (styled differently) when the field has content or a placeholder
- The toggle button is always present and cannot be disabled
- Password visibility state is maintained per component instance
- The component extends `BitInputFieldBase<string?>` for consistent form behavior
- All Bootstrap Italia form styling is automatically applied
- The component supports all standard HTML input attributes through `AdditionalAttributes`
- Input type automatically switches between "password" and "text" based on visibility state
- Additional text is rendered with specific styling for password field guidance
