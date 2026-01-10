# BitToggle

The `BitToggle` component represents a toggle switch form component that allows users to select between two states, typically on and off.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitToggle` component is designed to capture boolean input from users in forms or interactive UI scenarios. It provides a visual indicator reflecting its current state and can be bound to a boolean value in a data model, with built-in support for form integration and validation using Bootstrap Italia styles.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `bool` | ✗ | `false` | The current value of the toggle |
| `ValueChanged` | `EventCallback<bool>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<bool>>` | ✗ | - | Expression for model binding and validation |
| `For` | `Expression<Func<bool>>?` | ✗ | - | Expression that triggers validation for the field |
| `Label` | `string` | ✓ | - | The label text for the toggle |
| `Disabled` | `bool` | ✗ | `false` | Whether the toggle is disabled |
| `ViewMode` | `ToggleViewMode` | ✗ | `ToggleViewMode.Inline` | The display mode used to render the toggle component |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the toggle |
| `AdditionalTextId` | `string?` | ✗ | `null` | The identifier for additional text, used for aria-describedby accessibility |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## ToggleViewMode Enumeration

| Value | Description |
|-------|-------------|
| `Inline` | The toggle content is rendered inline |
| `Grouped` | The toggle is grouped (aligns toggle to the right of text) |

## Usage Examples

### Basic toggle

```razor
<BitToggle Label="Enable notifications" 
           @bind-Value="model.NotificationsEnabled" />

@code {
    private FormModel model = new();
    
    private class FormModel
    {
        public bool NotificationsEnabled { get; set; }
    }
}
```

### Toggle with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitToggle Label="I agree to the terms of service" 
               @bind-Value="model.AcceptedTerms"
               For="@(() => model.AcceptedTerms)" />
                 
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private ConsentModel model = new();
    
    private class ConsentModel
    {
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms of service")]
        public bool AcceptedTerms { get; set; }
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Grouped toggle

```razor
<BitToggle Label="Grouped toggle (right aligned)" 
           ViewMode="ToggleViewMode.Grouped"
           @bind-Value="isGrouped" />

@code {
    private bool isGrouped;
}
```

### Disabled toggle

```razor
<BitToggle Label="This option is disabled" 
           Disabled="true"
           @bind-Value="disabledValue" />

@code {
    private bool disabledValue = true;
}
```

### Toggle with additional text

```razor
<BitToggle Label="Enable dark mode"
           @bind-Value="darkModeEnabled"
           AdditionalTextId="helper-text">
    <AdditionalText>
        Switch to a dark color scheme for better viewing in low light
    </AdditionalText>
</BitToggle>

@code {
    private bool darkModeEnabled;
}
```

### Multiple toggles in a form

```razor
<EditForm Model="settings" OnValidSubmit="SaveSettings">
    <DataAnnotationsValidator />
    
    <h4>Application Settings</h4>
    
    <BitToggle Label="Enable notifications" 
               @bind-Value="settings.NotificationsEnabled" />
    
    <BitToggle Label="Auto-save changes" 
               @bind-Value="settings.AutoSaveEnabled" />
    
    <BitToggle Label="Show tips on startup" 
               @bind-Value="settings.ShowTips" />
    
    <BitToggle Label="Enable analytics" 
               @bind-Value="settings.AnalyticsEnabled"
               AdditionalTextId="helper-text">
        <AdditionalText>
            Help us improve the application by sharing anonymous usage data
        </AdditionalText>
    </BitToggle>
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Save Settings
    </BitButton>
</EditForm>

@code {
    private AppSettings settings = new();
    
    private class AppSettings
    {
        public bool NotificationsEnabled { get; set; } = true;
        public bool AutoSaveEnabled { get; set; } = true;
        public bool ShowTips { get; set; } = true;
        public bool AnalyticsEnabled { get; set; }
    }
    
    private async Task SaveSettings()
    {
        // Save settings logic
    }
}
```

### Toggle with dynamic state

```razor
<BitToggle Label="@GetToggleLabel()" 
           @bind-Value="isActive"
           ViewMode="ToggleViewMode.Inline" />

<p>Status: @(isActive ? "Active" : "Inactive")</p>

@code {
    private bool isActive;
    
    private string GetToggleLabel()
    {
        return isActive ? "Deactivate feature" : "Activate feature";
    }
}
```

## Styling

The `BitToggle` component uses Bootstrap Italia's toggle switch styles. You can customize the appearance using:

- **Inline layout**: Set `ViewMode="ToggleViewMode.Inline"` (default) to display toggles inline
- **Grouped layout**: Set `ViewMode="ToggleViewMode.Grouped"` to align the toggle to the right of the label
- **Custom classes**: Use `AdditionalAttributes` to add custom CSS classes

## Accessibility

The component follows accessibility best practices:
- Proper label association using `for` attribute
- Disabled state reflected in both visual styling and HTML attributes
- Support for `AdditionalAttributes` to add ARIA attributes as needed
- Visual indicator (lever) provides clear feedback on the toggle state

## Notes

- The component only supports `bool` type for its value
- The toggle automatically integrates with Blazor's `EditForm` and validation system
- Use `ValueExpression` (typically via `@bind-Value`) to enable validation
- The toggle provides a more visual and modern alternative to checkboxes for boolean inputs
- Unlike checkboxes, toggles are typically used for settings that take effect immediately

## See Also

- [BitCheckbox](checkbox.md) - For traditional checkbox inputs
- [Form Components](form-components.md) - Overview of all form components
