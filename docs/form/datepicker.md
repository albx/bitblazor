# BitDatepicker

The `BitDatepicker` component represents a date input field using Bootstrap Italia styles for forms.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitDatepicker` component is designed to handle date selection and provides built-in support for form integration and validation. It is a generic component that supports both `DateTime` and `DateOnly` types, ensuring type safety while maintaining flexibility for different date handling scenarios.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `T` | ✗ | `default(T)` | The current value of the date picker |
| `ValueChanged` | `EventCallback<T>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<T>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the date picker field |
| `Disabled` | `bool` | ✗ | `false` | Whether the date picker is disabled |
| `Readonly` | `bool` | ✗ | `false` | Whether the date picker is readonly |
| `Plaintext` | `bool` | ✗ | `false` | Whether to render as plain text instead of input |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Supported Types

The `BitDatepicker<T>` component supports the following date types:

- `DateTime` - Full date and time information
- `DateOnly` - Date-only values (available in .NET 6+)

## Usage Examples

### Basic date picker with DateTime

```razor
<BitDatepicker Label="Birth Date" 
               @bind-Value="model.BirthDate" />

@code {
    private PersonModel model = new();
    
    private class PersonModel
    {
        public DateTime BirthDate { get; set; } = DateTime.Today;
    }
}
```

### Date picker with DateOnly

```razor
<BitDatepicker Label="Appointment Date" 
               @bind-Value="model.AppointmentDate" />

@code {
    private AppointmentModel model = new();
    
    private class AppointmentModel
    {
        public DateOnly AppointmentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }
}
```

### Date picker with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitDatepicker Label="Event Date" 
                   @bind-Value="model.EventDate"
                   For="@(() => model.EventDate)" />
                  
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private EventModel model = new();
    
    private class EventModel
    {
        [Required(ErrorMessage = "Event date is required")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; } = DateTime.Today;
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Disabled date picker

```razor
<BitDatepicker Label="Registration Date" 
               Value="@registrationDate"
               Disabled="true" />

@code {
    private DateTime registrationDate = new DateTime(2025, 1, 15);
}
```

### Readonly date picker

```razor
<BitDatepicker Label="Created Date" 
               Value="@createdDate"
               Readonly="true" />

@code {
    private DateTime createdDate = DateTime.Now;
}
```

### Plaintext mode

```razor
<BitDatepicker Label="Last Updated" 
               Value="@lastUpdated"
               Plaintext="true" />

@code {
    private DateTime lastUpdated = DateTime.Now;
}
```

### Complete form example with date range validation

```razor
<EditForm Model="bookingModel" OnValidSubmit="HandleBooking">
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-md-6">
            <BitDatepicker Label="Check-in Date" 
                          @bind-Value="bookingModel.CheckInDate"
                          For="@(() => bookingModel.CheckInDate)">
                <AdditionalText>
                    Select your arrival date
                </AdditionalText>
            </BitDatepicker>
        </div>
        <div class="col-md-6">
            <BitDatepicker Label="Check-out Date" 
                          @bind-Value="bookingModel.CheckOutDate"
                          For="@(() => bookingModel.CheckOutDate)">
                <AdditionalText>
                    Select your departure date
                </AdditionalText>
            </BitDatepicker>
        </div>
        <div class="col-12">
            <BitButton Type="ButtonType.Submit" 
                      Color="Color.Primary"
                      CssClass="w-100">
                Book Now
            </BitButton>
        </div>
    </div>
    
    <ValidationSummary />
</EditForm>

@code {
    private BookingModel bookingModel = new();
    
    private class BookingModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; } = DateTime.Today;
        
        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThan(nameof(CheckInDate), ErrorMessage = "Check-out date must be after check-in date")]
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
    }
    
    // Custom validation attribute
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            
            if (property == null)
                throw new ArgumentException("Property with this name not found");
                
            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);
            
            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage ?? "Date must be greater than comparison date");
                
            return ValidationResult.Success;
        }
    }
    
    private async Task HandleBooking()
    {
        // Handle booking submission
    }
}
```

### Using with DateOnly for better semantics

```razor
<EditForm Model="scheduleModel" OnValidSubmit="HandleSchedule">
    <DataAnnotationsValidator />
    
    <BitDatepicker Label="Start Date" 
                  @bind-Value="scheduleModel.StartDate"
                  For="@(() => scheduleModel.StartDate)" />
    
    <BitDatepicker Label="End Date" 
                  @bind-Value="scheduleModel.EndDate"
                  For="@(() => scheduleModel.EndDate)" />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Create Schedule
    </BitButton>
    
    <ValidationSummary />
</EditForm>

@code {
    private ScheduleModel scheduleModel = new();
    
    private class ScheduleModel
    {
        [Required]
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        
        [Required]
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(7));
    }
    
    private async Task HandleSchedule()
    {
        // Handle schedule creation
    }
}
```

## Generated HTML Structure

### Basic date picker

```html
<div class="form-group">
    <label for="date-12345" class="active">Birth Date</label>
    <input type="date" 
           class="form-control" 
           name="date-12345" 
           id="date-12345"
           value="2025-01-15" />
</div>
```

### Date picker with validation error

```html
<div class="form-group">
    <label for="date-12345" class="active">Event Date</label>
    <input type="date" 
           class="form-control" 
           name="date-12345" 
           id="date-12345"
           aria-describedby="date-12345-validation" />
    <div class="is-invalid" id="date-12345-validation">
        The Event Date field is required.
    </div>
</div>
```

### Disabled date picker

```html
<div class="form-group">
    <label for="date-12345" class="active">Registration Date</label>
    <input type="date" 
           class="form-control" 
           name="date-12345" 
           id="date-12345"
           value="2025-01-15"
           disabled />
</div>
```

### Readonly date picker

```html
<div class="form-group">
    <label for="date-12345" class="active">Created Date</label>
    <input type="date" 
           class="form-control" 
           name="date-12345" 
           id="date-12345"
           value="2025-11-04"
           readonly />
</div>
```

## Generated CSS Classes

### Input element classes

- `form-control` - Base input styling
- `form-control-plaintext` - Plain text styling (when Plaintext="true")

### Label classes

- `active` - Applied to indicate an active label state

### Container classes

- `form-group` - Container for the entire field

## Accessibility

- Automatically associates labels with inputs using the `for` attribute
- Supports `aria-describedby` when AdditionalText is provided
- Maintains focus management and keyboard navigation
- Compatible with screen readers
- Supports validation message announcements
- Native browser date picker provides accessible date selection

## Form Integration

- Full support for `EditForm` and model binding
- Compatible with `DataAnnotationsValidator`
- Integrates with `ValidationSummary` and `ValidationMessage<T>`
- Supports `For` expression for validation binding
- Automatic validation state styling
- Type-safe with generic type parameter

## Browser Behavior

The `BitDatepicker` component uses the native HTML5 `<input type="date">` element, which provides:

- **Native date picker UI**: Each browser provides its own date selection interface
- **Locale support**: Date format automatically adjusts to user's locale
- **Mobile optimization**: Touch-friendly date selection on mobile devices
- **Keyboard accessibility**: Full keyboard support for date navigation
- **Format standardization**: Internal value format is always `yyyy-MM-dd`

### Browser-specific UIs

- **Chrome/Edge**: Calendar popup with month/year navigation
- **Firefox**: Calendar popup with keyboard shortcuts
- **Safari**: Scrollable date picker interface
- **Mobile browsers**: Native mobile date picker controls

## Notes

- The component automatically generates unique IDs for form fields using the "date" prefix
- Labels are automatically styled as "active" 
- The component extends `BitInputFieldBase<T>` for consistent form behavior
- All Bootstrap Italia form styling is automatically applied
- The component supports all standard HTML input attributes through `AdditionalAttributes`
- The internal value format is always `yyyy-MM-dd` regardless of display format
- Use `DateTime` when you need time information along with the date
- Use `DateOnly` when you only need date information (cleaner semantics, available in .NET 6+)
- The component leverages Blazor's built-in `InputDate` component internally for robust date handling

## Validation Examples

### Common validation scenarios

```csharp
public class DateValidationModel
{
    // Required date
    [Required(ErrorMessage = "Date is required")]
    public DateTime RequiredDate { get; set; }
    
    // Date must be today or in the future
    [Required]
    [FutureDate(ErrorMessage = "Date must be in the future")]
    public DateTime FutureDate { get; set; }
    
    // Date must be in the past
    [Required]
    [PastDate(ErrorMessage = "Date must be in the past")]
    public DateTime PastDate { get; set; }
    
    // Date within specific range
    [Required]
    [Range(typeof(DateTime), "2025-01-01", "2025-12-31", 
        ErrorMessage = "Date must be in 2025")]
    public DateTime DateInRange { get; set; }
}

// Custom validation attributes
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime && dateTime.Date <= DateTime.Today)
        {
            return new ValidationResult(ErrorMessage ?? "Date must be in the future");
        }
        
        if (value is DateOnly dateOnly && dateOnly <= DateOnly.FromDateTime(DateTime.Today))
        {
            return new ValidationResult(ErrorMessage ?? "Date must be in the future");
        }
        
        return ValidationResult.Success;
    }
}

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime && dateTime.Date >= DateTime.Today)
        {
            return new ValidationResult(ErrorMessage ?? "Date must be in the past");
        }
        
        if (value is DateOnly dateOnly && dateOnly >= DateOnly.FromDateTime(DateTime.Today))
        {
            return new ValidationResult(ErrorMessage ?? "Date must be in the past");
        }
        
        return ValidationResult.Success;
    }
}
```

## Best Practices

### 1. Choose the Right Type

```razor
<!-- ✅ Good: Use DateOnly when you don't need time information -->
<BitDatepicker Label="Birth Date" @bind-Value="birthDate" />
@code {
    private DateOnly birthDate = DateOnly.FromDateTime(DateTime.Today);
}

<!-- ⚠️ Acceptable: Use DateTime when you might need time later -->
<BitDatepicker Label="Event Date" @bind-Value="eventDate" />
@code {
    private DateTime eventDate = DateTime.Today;
}
```

### 2. Always Provide Clear Labels

```razor
<!-- ✅ Good -->
<BitDatepicker Label="Appointment Date" @bind-Value="appointmentDate" />

<!-- ❌ Bad: Missing label -->
<BitDatepicker @bind-Value="appointmentDate" />
```

### 3. Use Additional Text for Context

```razor
<!-- ✅ Good -->
<BitDatepicker Label="Delivery Date" @bind-Value="deliveryDate">
    <AdditionalText>
        Delivery available Monday through Friday
    </AdditionalText>
</BitDatepicker>
```

### 4. Implement Proper Validation

```razor
<!-- ✅ Good: With validation -->
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <BitDatepicker Label="Start Date" 
                  @bind-Value="model.StartDate"
                  For="@(() => model.StartDate)" />
</EditForm>
```

### 5. Use Readonly for Display-Only Dates

```razor
<!-- ✅ Good: Readonly for historical data -->
<BitDatepicker Label="Created On" 
              Value="@createdDate"
              Readonly="true" />
```

## See Also

- [Form Components Overview](form-components.md) - Complete guide to form components
- [BitTextField](text-field.md) - Single-line text input
- [BitNumberField](number-field.md) - Numeric input field
- [Enumerations](../enumerations.md) - Available enumeration values
