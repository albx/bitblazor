# BitCheckbox

The `BitCheckbox` component represents a checkbox form component that allows users to select or clear a boolean value within a form.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitCheckbox` component is designed to capture binary choices, such as yes/no or true/false, in form scenarios. It provides built-in support for form integration and validation using Bootstrap Italia styles.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `bool` | ✗ | `false` | The current value of the checkbox |
| `ValueChanged` | `EventCallback<bool>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<bool>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the checkbox |
| `Disabled` | `bool` | ✗ | `false` | Whether the checkbox is disabled |
| `Inline` | `bool` | ✗ | `false` | Whether the component's content should be rendered inline |
| `Grouped` | `bool` | ✗ | `false` | Whether the checkbox is grouped (aligns checkbox to the right of text) |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the checkbox |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Usage Examples

### Basic checkbox

```razor
<BitCheckbox Label="I agree to the terms and conditions" 
             @bind-Value="model.AcceptedTerms" />

@code {
    private FormModel model = new();
    
    private class FormModel
    {
        public bool AcceptedTerms { get; set; }
    }
}
```

### Checkbox with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitCheckbox Label="I agree to the privacy policy" 
                 @bind-Value="model.AcceptedPrivacy"
                 For="@(() => model.AcceptedPrivacy)" />
                 
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private ConsentModel model = new();
    
    private class ConsentModel
    {
        public bool AcceptedPrivacy { get; set; }
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Inline checkboxes

```razor
<div>
    <BitCheckbox Label="Option 1" 
                 Inline="true"
                 @bind-Value="option1" />
    
    <BitCheckbox Label="Option 2" 
                 Inline="true"
                 @bind-Value="option2" />
    
    <BitCheckbox Label="Option 3" 
                 Inline="true"
                 @bind-Value="option3" />
</div>

@code {
    private bool option1;
    private bool option2;
    private bool option3;
}
```

### Grouped checkbox

```razor
<BitCheckbox Label="Grouped checkbox (right aligned)" 
             Grouped="true"
             @bind-Value="isGrouped" />

@code {
    private bool isGrouped;
}
```

### Disabled checkbox

```razor
<BitCheckbox Label="This option is disabled" 
             Disabled="true"
             @bind-Value="disabledValue" />

@code {
    private bool disabledValue;
}
```

### Checkbox with additional text

```razor
<BitCheckbox Label="Subscribe to newsletter"
             @bind-Value="subscribeNewsletter"
             AdditionalTextId="helper-text">
    <AdditionalText>
        You will receive weekly updates about new features
    </AdditionalText>
</BitCheckbox>

@code {
    private bool subscribeNewsletter;
}
```

### Multiple checkboxes in a form

```razor
<EditForm Model="preferences" OnValidSubmit="SavePreferences">
    <DataAnnotationsValidator />
    
    <h4>Notification Preferences</h4>
    
    <BitCheckbox Label="Email notifications" 
                 @bind-Value="preferences.EmailNotifications" />
    
    <BitCheckbox Label="SMS notifications" 
                 @bind-Value="preferences.SmsNotifications" />
    
    <BitCheckbox Label="Push notifications" 
                 @bind-Value="preferences.PushNotifications" />
    
    <BitCheckbox Label="Weekly digest" 
                 @bind-Value="preferences.WeeklyDigest"
                 AdditionalTextId="helper-text">
        <AdditionalText>
            Receive a summary of activities once a week
        </AdditionalText>
    </BitCheckbox>
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Save Preferences
    </BitButton>
</EditForm>

@code {
    private NotificationPreferences preferences = new();
    
    private class NotificationPreferences
    {
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public bool PushNotifications { get; set; }
        public bool WeeklyDigest { get; set; }
    }
    
    private async Task SavePreferences()
    {
        // Save preferences logic
    }
}
```

## Styling

The `BitCheckbox` component uses Bootstrap Italia's checkbox styles. You can customize the appearance using:

- **Inline layout**: Set `Inline="true"` to display checkboxes side by side
- **Grouped layout**: Set `Grouped="true"` to align the checkbox to the right of the label
- **Custom classes**: Use `AdditionalAttributes` to add custom CSS classes

## Accessibility

The component follows accessibility best practices:
- Proper label association using `for` attribute
- Disabled state reflected in both visual styling and HTML attributes
- Support for `AdditionalAttributes` to add ARIA attributes as needed

## Notes

- The component only supports `bool` type for its value
- The checkbox automatically integrates with Blazor's `EditForm` and validation system
- Use `ValueExpression` (typically via `@bind-Value`) to enable validation
