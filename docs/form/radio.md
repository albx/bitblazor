# BitRadio

The `BitRadio` and `BitRadioGroup` components work together to represent a group of radio buttons that allow users to select a single value from a set of options within a form.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitRadioGroup` component provides a container for managing a group of radio buttons, handling data binding, validation, and form integration. Individual radio button items are represented by `BitRadio` components nested within the group. This combination enables single-choice selection scenarios with Bootstrap Italia styles.

## Parameters

### BitRadioGroup\<T\>

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `T?` | ✗ | `null` | The current value of the radio group |
| `ValueChanged` | `EventCallback<T>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<T>>` | ✗ | - | Expression for model binding and validation |
| `For` | `Expression<Func<T>>?` | ✗ | - | Expression that triggers validation for the field |
| `ChildContent` | `RenderFragment` | ✓ | - | The content containing the radio button items |
| `Inline` | `bool` | ✗ | `false` | Whether the radio items should be rendered inline |
| `Grouped` | `bool` | ✗ | `false` | Whether the radio items are grouped (aligns radio buttons to the right of text) |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

### BitRadio\<T\>

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Label` | `string` | ✓ | - | The label text for the radio button |
| `Value` | `T?` | ✗ | `null` | The value associated with this radio button item |
| `Disabled` | `bool` | ✗ | `false` | Whether the radio button is disabled |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the radio button |
| `AdditionalTextId` | `string?` | ✗ | `null` | The identifier for additional text, used for aria-describedby accessibility |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Usage Examples

### Basic radio group

```razor
<BitRadioGroup @bind-Value="model.SelectedOption">
    <BitRadio Label="Option 1" Value="1" />
    <BitRadio Label="Option 2" Value="2" />
    <BitRadio Label="Option 3" Value="3" />
</BitRadioGroup>

@code {
    private FormModel model = new();
    
    private class FormModel
    {
        public int SelectedOption { get; set; } = 1;
    }
}
```

### Radio group with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitRadioGroup @bind-Value="model.PreferredContact"
                   For="@(() => model.PreferredContact)">
        <BitRadio Label="Email" Value="ContactMethod.Email" />
        <BitRadio Label="Phone" Value="ContactMethod.Phone" />
        <BitRadio Label="SMS" Value="ContactMethod.SMS" />
    </BitRadioGroup>
                 
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private ContactModel model = new();
    
    private class ContactModel
    {
        [Required(ErrorMessage = "Please select a contact method")]
        public ContactMethod PreferredContact { get; set; }
    }
    
    private enum ContactMethod
    {
        Email,
        Phone,
        SMS
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Inline radio buttons

```razor
<BitRadioGroup @bind-Value="model.Size" Inline="true">
    <BitRadio Label="Small" Value="Size.Small" />
    <BitRadio Label="Medium" Value="Size.Medium" />
    <BitRadio Label="Large" Value="Size.Large" />
</BitRadioGroup>

@code {
    private SizeModel model = new();
    
    private class SizeModel
    {
        public Size Size { get; set; } = Size.Medium;
    }
}
```

### Grouped radio buttons

```razor
<BitRadioGroup @bind-Value="model.Agreement" Grouped="true">
    <BitRadio Label="I agree to the terms and conditions" Value="true" />
    <BitRadio Label="I do not agree" Value="false" />
</BitRadioGroup>

@code {
    private AgreementModel model = new();
    
    private class AgreementModel
    {
        public bool Agreement { get; set; }
    }
}
```

### Radio button with additional text

```razor
<BitRadioGroup @bind-Value="model.Plan">
    <BitRadio Label="Basic Plan" 
              Value="Plan.Basic"
              AdditionalTextId="basic-plan-desc">
        <AdditionalText>
            Free forever with basic features
        </AdditionalText>
    </BitRadio>
    
    <BitRadio Label="Pro Plan" 
              Value="Plan.Pro"
              AdditionalTextId="pro-plan-desc">
        <AdditionalText>
            $9.99/month with advanced features
        </AdditionalText>
    </BitRadio>
    
    <BitRadio Label="Enterprise Plan" 
              Value="Plan.Enterprise"
              AdditionalTextId="enterprise-plan-desc">
        <AdditionalText>
            Custom pricing with all features
        </AdditionalText>
    </BitRadio>
</BitRadioGroup>

@code {
    private PlanModel model = new();
    
    private class PlanModel
    {
        public Plan Plan { get; set; } = Plan.Basic;
    }
    
    private enum Plan
    {
        Basic,
        Pro,
        Enterprise
    }
}
```

### Radio buttons with string values

```razor
<BitRadioGroup @bind-Value="model.Country">
    <BitRadio Label="United States" Value="@("US")" />
    <BitRadio Label="Canada" Value="@("CA")" />
    <BitRadio Label="United Kingdom" Value="@("UK")" />
    <BitRadio Label="Other" Value="@("OTHER")" />
</BitRadioGroup>

@code {
    private CountryModel model = new();
    
    private class CountryModel
    {
        public string Country { get; set; } = "US";
    }
}
```

### Disabled radio button

```razor
<BitRadioGroup @bind-Value="model.Status">
    <BitRadio Label="Active" Value="Status.Active" />
    <BitRadio Label="Inactive" Value="Status.Inactive" />
    <BitRadio Label="Suspended" Value="Status.Suspended" Disabled="true" />
</BitRadioGroup>

@code {
    private StatusModel model = new();
    
    private class StatusModel
    {
        public Status Status { get; set; } = Status.Active;
    }
    
    private enum Status
    {
        Active,
        Inactive,
        Suspended
    }
}
```

## Accessibility

- Each radio button is properly associated with its label using the `for` and `id` attributes
- The `aria-describedby` attribute is automatically set when `AdditionalText` is provided with an `AdditionalTextId`
- Disabled radio buttons receive the appropriate `disabled` attribute
- Keyboard navigation works as expected with arrow keys for navigating between radio options

## Best Practices

1. **Group Related Options**: Always use `BitRadioGroup` to wrap related `BitRadio` items
2. **Provide Clear Labels**: Each radio option should have a descriptive label that clearly indicates what selecting it means
3. **Set Default Value**: Consider setting a default selection to avoid requiring users to make an obvious choice
4. **Use Appropriate Types**: Use enums for predefined sets of options and primitive types (int, string) for dynamic options
5. **Validation**: Use the `For` parameter with validation attributes when the selection is required
6. **Additional Context**: Use `AdditionalText` to provide extra information about each option when needed

## Notes

- The `BitRadio` component must be used within a `BitRadioGroup` component
- The type parameter `T` must match between the `BitRadioGroup` and `BitRadio` components
- Radio groups are single-selection only - for multiple selections, use `BitCheckbox` instead
- The `Grouped` parameter aligns radio buttons to the right, following Bootstrap Italia's grouped form check pattern
