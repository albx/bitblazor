# BitTimepicker

The `BitTimepicker` component represents a time input field using Bootstrap Italia styles for forms.

## Namespace

```csharp
BitBlazor.Form
```

## Description

The `BitTimepicker` component is designed to handle time selection and provides built-in support for form integration and validation. It is a generic component that supports the `TimeOnly` type, ensuring type safety and providing a consistent time input experience.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Value` | `T` | ✗ | `default(T)` | The current value of the time picker |
| `ValueChanged` | `EventCallback<T>` | ✗ | - | Callback fired when the value changes |
| `ValueExpression` | `Expression<Func<T>>` | ✗ | - | Expression for model binding and validation |
| `Label` | `string` | ✓ | - | The label text for the time picker field |
| `Disabled` | `bool` | ✗ | `false` | Whether the time picker is disabled |
| `Readonly` | `bool` | ✗ | `false` | Whether the time picker is readonly |
| `Plaintext` | `bool` | ✗ | `false` | Whether to render as plain text instead of input |
| `AdditionalAttributes` | `Dictionary<string, object>` | ✗ | `{}` | Additional HTML attributes |

## Supported Types

The `BitTimepicker<T>` component supports the following time type:

- `TimeOnly` - Time-only values (available in .NET 6+)

## Usage Examples

### Basic time picker

```razor
<BitTimepicker Label="Meeting Time" 
               @bind-Value="model.MeetingTime" />

@code {
    private MeetingModel model = new();
    
    private class MeetingModel
    {
        public TimeOnly MeetingTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    }
}
```

### Time picker with initial value

```razor
<BitTimepicker Label="Start Time" 
               @bind-Value="model.StartTime" />

@code {
    private ScheduleModel model = new()
    {
        StartTime = new TimeOnly(9, 0) // 09:00
    };
    
    private class ScheduleModel
    {
        public TimeOnly StartTime { get; set; }
    }
}
```

### Time picker with validation

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <BitTimepicker Label="Appointment Time" 
                   @bind-Value="model.AppointmentTime"
                   For="@(() => model.AppointmentTime)" />
                  
    <ValidationSummary />
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Submit
    </BitButton>
</EditForm>

@code {
    private AppointmentModel model = new();
    
    private class AppointmentModel
    {
        [Required(ErrorMessage = "Appointment time is required")]
        public TimeOnly AppointmentTime { get; set; }
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
    }
}
```

### Disabled time picker

```razor
<BitTimepicker Label="Default Time" 
               @bind-Value="model.DefaultTime"
               Disabled="true" />

@code {
    private SettingsModel model = new()
    {
        DefaultTime = new TimeOnly(12, 0) // 12:00
    };
    
    private class SettingsModel
    {
        public TimeOnly DefaultTime { get; set; }
    }
}
```

### Readonly time picker

```razor
<BitTimepicker Label="Record Time" 
               @bind-Value="model.RecordTime"
               Readonly="true" />

@code {
    private RecordModel model = new()
    {
        RecordTime = TimeOnly.FromDateTime(DateTime.Now)
    };
    
    private class RecordModel
    {
        public TimeOnly RecordTime { get; set; }
    }
}
```

### Plaintext time picker (for display only)

```razor
<BitTimepicker Label="Submission Time" 
               @bind-Value="model.SubmissionTime"
               Plaintext="true" />

@code {
    private SubmissionModel model = new()
    {
        SubmissionTime = new TimeOnly(14, 30) // 14:30
    };
    
    private class SubmissionModel
    {
        public TimeOnly SubmissionTime { get; set; }
    }
}
```

## Complete Example

Here's a complete example showing various configurations:

```razor
@page "/timepicker-example"

<h1>Time Picker Examples</h1>

<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <div class="row">
        <div class="col-md-6">
            <BitTimepicker Label="Start Time" 
                           @bind-Value="model.StartTime"
                           For="@(() => model.StartTime)" />
        </div>
        
        <div class="col-md-6">
            <BitTimepicker Label="End Time" 
                           @bind-Value="model.EndTime"
                           For="@(() => model.EndTime)" />
        </div>
    </div>
    
    <div class="row mt-3">
        <div class="col-md-6">
            <BitTimepicker Label="Break Time" 
                           @bind-Value="model.BreakTime" />
        </div>
        
        <div class="col-md-6">
            <BitTimepicker Label="Default Time (Readonly)" 
                           @bind-Value="model.DefaultTime"
                           Readonly="true" />
        </div>
    </div>
    
    <ValidationSummary />
    
    <div class="mt-4">
        <BitButton Type="ButtonType.Submit" Color="Color.Primary">
            Save Schedule
        </BitButton>
    </div>
</EditForm>

@if (submitted)
{
    <div class="alert alert-success mt-4">
        <h4>Schedule Saved!</h4>
        <ul>
            <li>Start Time: @model.StartTime.ToString("HH:mm")</li>
            <li>End Time: @model.EndTime.ToString("HH:mm")</li>
            <li>Break Time: @model.BreakTime?.ToString("HH:mm") ?? "Not set"</li>
        </ul>
    </div>
}

@code {
    private ScheduleFormModel model = new();
    private bool submitted = false;
    
    private class ScheduleFormModel
    {
        [Required(ErrorMessage = "Start time is required")]
        public TimeOnly StartTime { get; set; } = new TimeOnly(9, 0);
        
        [Required(ErrorMessage = "End time is required")]
        public TimeOnly EndTime { get; set; } = new TimeOnly(17, 0);
        
        public TimeOnly? BreakTime { get; set; }
        
        public TimeOnly DefaultTime { get; set; } = new TimeOnly(12, 0);
    }
    
    private async Task HandleSubmit()
    {
        submitted = true;
        // Process the schedule
        await Task.CompletedTask;
    }
}
```

## Integration with Forms

The `BitTimepicker` component seamlessly integrates with Blazor's `EditForm` and validation system:

- Use `@bind-Value` for two-way data binding
- Use `For` parameter to connect with validation
- Supports `DataAnnotationsValidator` and custom validators
- Displays validation messages automatically when used with validation attributes

## Accessibility

The component automatically handles:

- Proper label association with the input field
- Disabled state styling and functionality
- Readonly state for non-editable displays
- Keyboard navigation support through native HTML5 time input

## Notes

- The `BitTimepicker` component uses the HTML5 `<input type="time">` element internally
- Browser support for time input may vary; modern browsers provide a native time picker UI
- The component supports the `TimeOnly` type introduced in .NET 6
- Time values are displayed in 24-hour format (HH:mm) by default, but the browser's time picker may show 12-hour format based on user locale settings
