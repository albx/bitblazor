# Form Components - BitBlazor

This section covers all form-related components in BitBlazor, designed to work seamlessly with ASP.NET Core Blazor's form system and Bootstrap Italia styling.

## Overview

BitBlazor provides a comprehensive set of form components that integrate with:

- **ASP.NET Core Blazor Forms**: Full support for `EditForm`, model binding, and validation
- **Bootstrap Italia**: Consistent styling following the Italian Design System
- **Data Annotations**: Automatic validation using `DataAnnotationsValidator`
- **Accessibility**: WCAG-compliant form controls with proper ARIA attributes

## Available Form Components

### Input Components

| Component | Purpose | Key Features |
|-----------|---------|--------------|
| [`BitTextField`](text-field.md) | Single-line text input | Email/Tel/URL types, input groups, sizes |
| [`BitPasswordField`](password-field.md) | Password input with toggle | Show/hide functionality, secure input |
| [`BitTextAreaField`](text-area-field.md) | Multi-line text input | Configurable rows, auto-sizing |

### Base Classes

| Class | Purpose | Description |
|-------|---------|-------------|
| `BitFormComponentBase<T>` | Form integration | Base class for form-bound components |
| `BitInputFieldBase<T>` | Input field features | Base class for input components with labels, validation, and sizing |

## Common Features

All form components in BitBlazor share these features:

### Model Binding and Validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <!-- Any BitBlazor form component -->
    <BitTextField Label="Name" 
                  @bind-Value="model.Name"
                  For="@(() => model.Name)" />
    
    <ValidationSummary />
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">Submit</BitButton>
</EditForm>
```

### Sizing Options

All input components support three sizes:

```razor
<BitTextField Label="Small" Size="Size.Small" @bind-Value="value" />
<BitTextField Label="Default" @bind-Value="value" />
<BitTextField Label="Large" Size="Size.Large" @bind-Value="value" />
```

### States and Modes

```razor
<!-- Normal state -->
<BitTextField Label="Normal" @bind-Value="value" />

<!-- Disabled state -->
<BitTextField Label="Disabled" @bind-Value="value" Disabled="true" />

<!-- Readonly state -->
<BitTextField Label="Readonly" @bind-Value="value" Readonly="true" />

<!-- Plaintext mode -->
<BitTextField Label="Plaintext" @bind-Value="value" Plaintext="true" />
```

### Additional Text and Help

```razor
<BitTextField Label="Username" 
              @bind-Value="model.Username"
              AdditionalTextId="username-help">
    <AdditionalText>
        Choose a unique username between 3-20 characters.
    </AdditionalText>
</BitTextField>
```

### Accessibility Features

- Automatic label association using `for` and `id` attributes
- Support for `aria-describedby` with additional text
- Validation message announcements
- Keyboard navigation support
- Screen reader compatibility

## Form Integration Examples

### Complete Registration Form

```razor
<EditForm Model="registrationModel" OnValidSubmit="HandleRegistration">
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-md-6">
            <BitTextField Label="First Name" 
                          @bind-Value="registrationModel.FirstName"
                          For="@(() => registrationModel.FirstName)" />
        </div>
        <div class="col-md-6">
            <BitTextField Label="Last Name" 
                          @bind-Value="registrationModel.LastName"
                          For="@(() => registrationModel.LastName)" />
        </div>
        <div class="col-12">
            <BitTextField Label="Email Address" 
                          Type="TextFieldType.Email"
                          @bind-Value="registrationModel.Email"
                          For="@(() => registrationModel.Email)" />
        </div>
        <div class="col-12">
            <BitPasswordField Label="Password" 
                              @bind-Value="registrationModel.Password"
                              For="@(() => registrationModel.Password)" />
        </div>
        <div class="col-12">
            <BitPasswordField Label="Confirm Password" 
                              @bind-Value="registrationModel.ConfirmPassword"
                              For="@(() => registrationModel.ConfirmPassword)" />
        </div>
        <div class="col-12">
            <BitTextAreaField Label="Bio (Optional)" 
                              Rows="4"
                              @bind-Value="registrationModel.Bio"
                              Placeholder="Tell us about yourself..."
                              AdditionalTextId="bio-help">
                <AdditionalText>
                    Maximum 500 characters. This information will be visible on your profile.
                </AdditionalText>
            </BitTextAreaField>
        </div>
        <div class="col-12">
            <BitButton Type="ButtonType.Submit" 
                       Color="Color.Primary" 
                       Size="Size.Large"
                       CssClass="w-100">
                Create Account
            </BitButton>
        </div>
    </div>
    
    <ValidationSummary />
</EditForm>

@code {
    private RegistrationModel registrationModel = new();
    
    private class RegistrationModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Bio { get; set; }
    }
    
    private async Task HandleRegistration()
    {
        // Handle registration logic
    }
}
```

### Advanced Input Groups

```razor
<!-- URL input with protocol prefix -->
<BitTextField Label="Website" 
              Type="TextFieldType.Url"
              @bind-Value="model.Website">
    <PrependContent>
        <span class="input-group-text">https://</span>
    </PrependContent>
    <AppendContent>
        <BitButton Color="Color.Secondary" Size="Size.Small">
            <BitIcon IconName="@Icons.ItExternalLink" />
        </BitButton>
    </AppendContent>
</BitTextField>

<!-- Price input with currency -->
<BitTextField Label="Price" 
              @bind-Value="model.Price">
    <PrependContent>
        <span class="input-group-text">€</span>
    </PrependContent>
    <AppendContent>
        <span class="input-group-text">.00</span>
    </AppendContent>
</BitTextField>
```

## Validation Integration

### Data Annotations Support

```csharp
public class ContactModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;
    
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string? Phone { get; set; }
    
    [Required]
    [StringLength(1000, MinimumLength = 10, 
        ErrorMessage = "Message must be between 10 and 1000 characters")]
    public string Message { get; set; } = string.Empty;
}
```

### Custom Validation Attributes

```csharp
public class StrongPasswordAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string password)
            return false;
            
        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}

public class UserModel
{
    [Required]
    [StrongPassword(ErrorMessage = "Password must contain uppercase, lowercase, digit, and special character")]
    public string Password { get; set; } = string.Empty;
}
```

## Styling and Customization

### Bootstrap Italia Integration

All form components automatically apply Bootstrap Italia classes:

```css
/* Automatically applied classes */
.form-group { /* Container */ }
.form-control { /* Input styling */ }
.form-control-lg { /* Large size */ }
.form-control-sm { /* Small size */ }
.is-invalid { /* Validation error state */ }
.form-text { /* Helper text */ }
```

### Custom CSS Classes

```razor
<BitTextField Label="Custom Styled" 
              @bind-Value="value"
              CssClass="my-custom-field"
              AdditionalAttributes="@(new Dictionary<string, object> 
              {
                  ["style"] = "border-radius: 10px;"
              })" />
```

## Best Practices

### 1. Always Use Labels

```razor
<!-- ✅ Good -->
<BitTextField Label="Email Address" @bind-Value="model.Email" />

<!-- ❌ Bad -->
<BitTextField Placeholder="Email Address" @bind-Value="model.Email" />
```

### 2. Provide Helpful Additional Text

```razor
<BitPasswordField Label="Password" 
                  @bind-Value="model.Password">
    <AdditionalText>
        Must be at least 8 characters with uppercase, lowercase, number, and special character.
    </AdditionalText>
</BitPasswordField>
```

### 3. Use Appropriate Input Types

```razor
<!-- ✅ Good -->
<BitTextField Label="Email" Type="TextFieldType.Email" @bind-Value="model.Email" />
<BitTextField Label="Phone" Type="TextFieldType.Tel" @bind-Value="model.Phone" />

<!-- ❌ Bad -->
<BitTextField Label="Email" @bind-Value="model.Email" />
<BitTextField Label="Phone" @bind-Value="model.Phone" />
```

### 4. Group Related Fields

```razor
<div class="row">
    <div class="col-md-6">
        <BitTextField Label="First Name" @bind-Value="model.FirstName" />
    </div>
    <div class="col-md-6">
        <BitTextField Label="Last Name" @bind-Value="model.LastName" />
    </div>
</div>
```

### 5. Use Proper Validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <!-- Form fields with For expressions -->
    <BitTextField Label="Name" 
                  @bind-Value="model.Name"
                  For="@(() => model.Name)" />
    
    <ValidationSummary />
</EditForm>
```

## Performance Considerations

- Form components are optimized for minimal re-renders
- Use `@bind-Value` for two-way data binding
- Avoid unnecessary `StateHasChanged()` calls
- Consider using `@bind-Value:event="oninput"` for real-time validation

## Browser Compatibility

All form components are compatible with:

- Modern browsers (Chrome, Firefox, Safari, Edge)
- Internet Explorer 11+ (with polyfills)
- Mobile browsers (iOS Safari, Chrome Mobile)
- Screen readers and assistive technologies

## Next Steps

- Explore individual component documentation for detailed usage
- Check out the [Quick Reference](../quick-reference.md) for common patterns
- Review [Enumeration](../enumerations.md) documentation for available options
