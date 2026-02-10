# BitSelectField

The `BitSelectField` component represents a dropdown select input field using Bootstrap Italia styles for forms.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitSelectField<T>` component is designed to handle selection from a list of options and provides built-in support for form integration and validation. It is a generic component that can work with any data type supported by Blazor's value converters (or with an appropriate custom converter) and supports option grouping, disabled options, and accessibility attributes.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `T?` | ✗ | `null` | The current value of the select field |
| `ValueChanged` | `EventCallback<T>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<T?>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the select field |
| `Disabled` | `bool` | ✗ | `false` | Whether the select field is disabled |
| `ChildContent` | `RenderFragment` | ✓ | - | Content defining the selectable options (BitSelectItem or BitSelectItemGroup) |
| `AdditionalText` | `RenderFragment?` | ✗ | `null` | Additional descriptive text displayed below the select |
| `AdditionalTextId` | `string?` | ✗ | `null` | ID for the additional text (used for aria-describedby) |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Child Components

### BitSelectItem<TValue>

Represents an individual selectable option within a BitSelectField component.

**Namespace**: `BitBlazor.Form`

#### Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `TValue?` | ✗ | `null` | The value of the option |
| `ChildContent` | `RenderFragment?` | ✗ | `null` | Content to be rendered as the option text |
| `Disabled` | `bool` | ✗ | `false` | Whether the option is disabled |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

### BitSelectItemGroup

Represents a group of selectable items within a BitSelectField component.

**Namespace**: `BitBlazor.Form`

#### Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Label` | `string` | ✓ | - | The label text for the option group |
| `ChildContent` | `RenderFragment` | ✓ | - | Content defining the options in the group |
| `Disabled` | `bool` | ✗ | `false` | Whether the entire option group is disabled |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Usage Examples

### Basic select field

```razor
<BitSelectField Label="Country" 
                @bind-Value="model.Country">
    <BitSelectItem Value="@string.Empty">Select a country...</BitSelectItem>
    <BitSelectItem Value="@("IT")">Italy</BitSelectItem>
    <BitSelectItem Value="@("US")">United States</BitSelectItem>
    <BitSelectItem Value="@("UK")">United Kingdom</BitSelectItem>
    <BitSelectItem Value="@("FR")">France</BitSelectItem>
</BitSelectField>

@code {
    private FormModel model = new();
    
    private class FormModel
    {
        public string Country { get; set; } = string.Empty;
    }
}
```

### Select field with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitSelectField Label="Role" 
                    @bind-Value="model.Role"
                    For="@(() => model.Role)">
        <BitSelectItem Value="@string.Empty">Select a role...</BitSelectItem>
        <BitSelectItem Value="@("admin")">Administrator</BitSelectItem>
        <BitSelectItem Value="@("user")">User</BitSelectItem>
        <BitSelectItem Value="@("guest")">Guest</BitSelectItem>
    </BitSelectField>
                  
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private UserModel model = new();
    
    private class UserModel
    {
        [Required(ErrorMessage = "Please select a role")]
        public string Role { get; set; } = string.Empty;
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Select field with grouped options

```razor
<BitSelectField Label="Browser" 
                @bind-Value="model.Browser">
    <BitSelectItem Value="@string.Empty">Choose a browser...</BitSelectItem>
    
    <BitSelectItemGroup Label="Modern Browsers">
        <BitSelectItem Value="@("chrome")">Google Chrome</BitSelectItem>
        <BitSelectItem Value="@("firefox")">Mozilla Firefox</BitSelectItem>
        <BitSelectItem Value="@("edge")">Microsoft Edge</BitSelectItem>
        <BitSelectItem Value="@("safari")">Apple Safari</BitSelectItem>
    </BitSelectItemGroup>
    
    <BitSelectItemGroup Label="Legacy Browsers">
        <BitSelectItem Value="@("ie11")">Internet Explorer 11</BitSelectItem>
        <BitSelectItem Value="@("opera")">Opera</BitSelectItem>
    </BitSelectItemGroup>
</BitSelectField>

@code {
    private BrowserModel model = new();
    
    private class BrowserModel
    {
        public string Browser { get; set; } = string.Empty;
    }
}
```

### Select field with disabled options

```razor
<BitSelectField Label="Subscription Plan" 
                @bind-Value="model.Plan">
    <BitSelectItem Value="@string.Empty">Select a plan...</BitSelectItem>
    <BitSelectItem Value="@("free")">Free - Basic features</BitSelectItem>
    <BitSelectItem Value="@("pro")">Pro - Advanced features</BitSelectItem>
    <BitSelectItem Value="@("enterprise")" Disabled="true">
        Enterprise - Contact sales
    </BitSelectItem>
</BitSelectField>

@code {
    private SubscriptionModel model = new();
    
    private class SubscriptionModel
    {
        public string Plan { get; set; } = string.Empty;
    }
}
```

### Select field with numeric values

```razor
<BitSelectField Label="Priority" 
                @bind-Value="model.Priority">
    <BitSelectItem Value="@(0)">Select priority...</BitSelectItem>
    <BitSelectItem Value="@(1)">Low</BitSelectItem>
    <BitSelectItem Value="@(2)">Medium</BitSelectItem>
    <BitSelectItem Value="@(3)">High</BitSelectItem>
    <BitSelectItem Value="@(4)">Critical</BitSelectItem>
</BitSelectField>

@code {
    private TaskModel model = new();
    
    private class TaskModel
    {
        public int Priority { get; set; }
    }
}
```

### Select field with enum values

```razor
<BitSelectField Label="Status" 
                @bind-Value="model.Status">
    <BitSelectItem Value="@OrderStatus.Pending">Pending</BitSelectItem>
    <BitSelectItem Value="@OrderStatus.Processing">Processing</BitSelectItem>
    <BitSelectItem Value="@OrderStatus.Shipped">Shipped</BitSelectItem>
    <BitSelectItem Value="@OrderStatus.Delivered">Delivered</BitSelectItem>
    <BitSelectItem Value="@OrderStatus.Cancelled">Cancelled</BitSelectItem>
</BitSelectField>

@code {
    private OrderModel model = new() { Status = OrderStatus.Pending };
    
    private class OrderModel
    {
        public OrderStatus Status { get; set; }
    }
    
    private enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
```

### Select field with additional text

```razor
<BitSelectField Label="Language" 
                @bind-Value="model.Language"
                AdditionalTextId="lang-help">
    <AdditionalText>
        Select your preferred language for the application interface.
    </AdditionalText>
    <BitSelectItem Value="@string.Empty">Select a language...</BitSelectItem>
    <BitSelectItem Value="@("en")">English</BitSelectItem>
    <BitSelectItem Value="@("it")">Italiano</BitSelectItem>
    <BitSelectItem Value="@("es")">Español</BitSelectItem>
    <BitSelectItem Value="@("fr")">Français</BitSelectItem>
</BitSelectField>

@code {
    private SettingsModel model = new();
    
    private class SettingsModel
    {
        public string Language { get; set; } = "en";
    }
}
```

### Disabled select field

```razor
<BitSelectField Label="Payment Method" 
                @bind-Value="model.PaymentMethod"
                Disabled="true">
    <BitSelectItem Value="@string.Empty">Select payment method...</BitSelectItem>
    <BitSelectItem Value="@("card")">Credit Card</BitSelectItem>
    <BitSelectItem Value="@("paypal")">PayPal</BitSelectItem>
    <BitSelectItem Value="@("bank")">Bank Transfer</BitSelectItem>
</BitSelectField>

@code {
    private PaymentModel model = new();
    
    private class PaymentModel
    {
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
```

## Generated HTML Structure

### Basic select field

```html
<div class="select-wrapper">
    <label for="select-12345">Country</label>
    <select id="select-12345" name="select-12345">
        <option value="">Select a country...</option>
        <option value="IT">Italy</option>
        <option value="US">United States</option>
        <option value="UK">United Kingdom</option>
        <option value="FR">France</option>
    </select>
</div>
```

### Select field with grouped options

```html
<div class="select-wrapper">
    <label for="select-12345">Browser</label>
    <select id="select-12345" name="select-12345">
        <option value="">Choose a browser...</option>
        <optgroup label="Modern Browsers">
            <option value="chrome">Google Chrome</option>
            <option value="firefox">Mozilla Firefox</option>
            <option value="edge">Microsoft Edge</option>
            <option value="safari">Apple Safari</option>
        </optgroup>
        <optgroup label="Legacy Browsers">
            <option value="ie11">Internet Explorer 11</option>
            <option value="opera">Opera</option>
        </optgroup>
    </select>
</div>
```

### Select field with validation error

```html
<div class="select-wrapper">
    <label for="select-12345">Role</label>
    <select name="select-12345"
            id="select-12345" 
            aria-describedby="select-12345-validation">
        <option value="">Select a role...</option>
        <option value="admin">Administrator</option>
        <option value="user">User</option>
        <option value="guest">Guest</option>
    </select>
</div>
```

### Select field with disabled option

```html
<div class="select-wrapper">
    <label for="select-12345">Subscription Plan</label>
    <select id="select-12345" name="select-12345">
        <option value="">Select a plan...</option>
        <option value="free">Free - Basic features</option>
        <option value="pro">Pro - Advanced features</option>
        <option value="enterprise" disabled>Enterprise - Contact sales</option>
    </select>
</div>
```

## Generated CSS Classes

### Container classes

- `select-wrapper` - Container wrapper for the entire field

## Accessibility

- Automatically associates labels with select elements using the `for` attribute
- Supports `aria-describedby` when AdditionalText is provided
- Maintains focus management and keyboard navigation
- Compatible with screen readers
- Supports validation message announcements
- Option groups enhance screen reader navigation and organization
- Disabled options are properly announced to assistive technologies

## Form Integration

- Full support for `EditForm` and model binding
- Compatible with `DataAnnotationsValidator`
- Integrates with `ValidationSummary` and `ValidationMessage<T>`
- Supports `For` expression for validation binding
- Automatic validation state styling
- Generic type support allows binding to any value type (string, int, enum, etc.)

## Notes

- The component automatically generates unique IDs for form fields using the "select" prefix
- BitSelectField is a generic component - you can use it with any type: `BitSelectField<string>`, `BitSelectField<int>`, `BitSelectField<MyEnum>`, etc.
- Use BitSelectItem components as direct children to define individual options
- Use BitSelectItemGroup to organize related options under a common label (rendered as optgroup)
- The first option typically serves as a placeholder (with empty or default value)
- Options and groups can be individually disabled
- The component extends `BitFormComponentBase<T>` for consistent form behavior
- All Bootstrap Italia form styling is automatically applied
- The component supports all standard HTML select attributes through `AdditionalAttributes`
- When working with enums, ensure you pass enum values directly to BitSelectItem without converting to string
