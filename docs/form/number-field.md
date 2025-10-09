# BitNumberField - BitBlazor

A numeric input field component that provides specialized controls for number input with increment/decrement buttons and type safety.

## Overview

`BitNumberField<T>` is a specialized form component designed for numeric input. It supports various numeric types and provides built-in increment/decrement functionality, along with optional symbols and adaptive sizing.

## Supported Types

The component supports the following numeric types:
- `int` (32-bit integer)
- `long` (64-bit integer) 
- `short` (16-bit integer)
- `float` (single-precision floating point)
- `double` (double-precision floating point)
- `decimal` (high precision decimal)

## Basic Usage

### Simple Number Input

```razor
<BitNumberField Label="Age" @bind-Value="model.Age" />

<BitNumberField Label="Price" @bind-Value="model.Price" />

<BitNumberField Label="Quantity" @bind-Value="model.Quantity" />

@code {
    private OrderModel model = new();
    
    private class OrderModel
    {
        public int Age { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
```

### With Min/Max Values

```razor
<BitNumberField Label="Rating" 
                @bind-Value="rating"
                Min="1"
                Max="5" />

<BitNumberField Label="Percentage" 
                @bind-Value="percentage"
                Min="0"
                Max="100" />

@code {
    private int rating = 3;
    private double percentage = 50.0;
}
```

### With Custom Step

```razor
<BitNumberField Label="Temperature" 
                @bind-Value="temperature"
                Step="0.5m"
                Min="-10m"
                Max="40m" />

<BitNumberField Label="Increment by 10" 
                @bind-Value="value"
                Step="10" />

@code {
    private decimal temperature = 20.0m;
    private int value = 100;
}
```

## Features

### Symbol Content

Add currency symbols, units, or other indicators:

```razor
<BitNumberField Label="Price" @bind-Value="price">
    <SymbolContent>€</SymbolContent>
</BitNumberField>

<BitNumberField Label="Weight" @bind-Value="weight">
    <SymbolContent>kg</SymbolContent>
</BitNumberField>

<BitNumberField Label="Distance" @bind-Value="distance">
    <SymbolContent><BitIcon IconName="@Icons.ItRuler" /></SymbolContent>
</BitNumberField>

@code {
    private decimal price = 99.99m;
    private double weight = 75.5;
    private float distance = 10.2f;
}
```

### Adaptive Sizing

Enable adaptive sizing to automatically adjust the input width based on the value:

```razor
<BitNumberField Label="Dynamic Width" 
                @bind-Value="adaptiveValue"
                Adaptive="true" />

@code {
    private int adaptiveValue = 123456;
}
```

### Custom Button Text

Customize the accessibility text for increment/decrement buttons:

```razor
<BitNumberField Label="Volume" 
                @bind-Value="volume"
                IncrementButtonText="Increase volume"
                DecrementButtonText="Decrease volume" />

@code {
    private int volume = 50;
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Value` | `T?` | `null` | The current numeric value |
| `ValueChanged` | `EventCallback<T?>` | - | Callback when value changes |
| `ValueExpression` | `Expression<Func<T?>>` | - | Expression for model binding |
| `Step` | `T?` | Type default | Increment/decrement step value |
| `Min` | `T?` | Type minimum | Minimum allowed value |
| `Max` | `T?` | Type maximum | Maximum allowed value |
| `Adaptive` | `bool` | `false` | Enable adaptive width sizing |
| `SymbolContent` | `RenderFragment?` | `null` | Custom symbol content |
| `IncrementButtonText` | `string` | "Increase value" | Accessibility text for + button |
| `DecrementButtonText` | `string` | "Decrease value" | Accessibility text for - button |

### Inherited Parameters

BitNumberField inherits all parameters from `BitInputFieldBase<T>`:

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Label` | `string?` | `null` | Field label text |
| `Placeholder` | `string?` | `null` | Placeholder text |
| `Size` | `Size` | `Size.Default` | Input size (Small, Default, Large) |
| `Disabled` | `bool` | `false` | Disable the input |
| `Readonly` | `bool` | `false` | Make input read-only |
| `Plaintext` | `bool` | `false` | Display as plain text |
| `AdditionalTextId` | `string?` | `null` | ID for additional help text |
| `AdditionalText` | `RenderFragment?` | `null` | Additional help text content |

## Examples

### Complete Product Form

```razor
<EditForm Model="product" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />
    
    <div class="row">
        <div class="col-md-8">
            <BitTextField Label="Product Name" 
                          @bind-Value="product.Name"
                          For="@(() => product.Name)" />
        </div>
        <div class="col-md-4">
            <BitNumberField Label="Stock Quantity" 
                            @bind-Value="product.StockQuantity"
                            For="@(() => product.StockQuantity)"
                            Min="0"
                            Step="1" />
        </div>
        <div class="col-md-6">
            <BitNumberField Label="Price" 
                            @bind-Value="product.Price"
                            For="@(() => product.Price)"
                            Min="0"
                            Step="0.01m">
                <SymbolContent>€</SymbolContent>
            </BitNumberField>
        </div>
        <div class="col-md-6">
            <BitNumberField Label="Weight" 
                            @bind-Value="product.Weight"
                            For="@(() => product.Weight)"
                            Min="0"
                            Step="0.1">
                <SymbolContent>kg</SymbolContent>
            </BitNumberField>
        </div>
        <div class="col-md-4">
            <BitNumberField Label="Rating" 
                            @bind-Value="product.Rating"
                            For="@(() => product.Rating)"
                            Min="1"
                            Max="5"
                            Step="1" />
        </div>
        <div class="col-md-4">
            <BitNumberField Label="Discount %" 
                            @bind-Value="product.DiscountPercentage"
                            For="@(() => product.DiscountPercentage)"
                            Min="0"
                            Max="100"
                            Step="5">
                <SymbolContent>%</SymbolContent>
            </BitNumberField>
        </div>
        <div class="col-md-4">
            <BitNumberField Label="Tax Rate" 
                            @bind-Value="product.TaxRate"
                            For="@(() => product.TaxRate)"
                            Min="0"
                            Max="1"
                            Step="0.001"
                            Adaptive="true">
                <SymbolContent>%</SymbolContent>
            </BitNumberField>
        </div>
    </div>
    
    <BitButton Type="ButtonType.Submit" Color="Color.Primary">
        Save Product
    </BitButton>
</EditForm>

@code {
    private ProductModel product = new();
    
    private class ProductModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative")]
        public int StockQuantity { get; set; }
        
        [Required]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between €0.01 and €9999.99")]
        public decimal Price { get; set; }
        
        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000 kg")]
        public double? Weight { get; set; }
        
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }
        
        [Range(0, 100, ErrorMessage = "Discount must be between 0% and 100%")]
        public int? DiscountPercentage { get; set; }
        
        [Range(0, 1, ErrorMessage = "Tax rate must be between 0 and 1")]
        public double? TaxRate { get; set; }
    }
    
    private async Task HandleSubmit()
    {
        // Handle form submission
        Console.WriteLine($"Product: {product.Name}, Price: €{product.Price}");
    }
}
```

### Calculator Example

```razor
<div class="calculator">
    <h3>Simple Calculator</h3>
    
    <div class="row">
        <div class="col-md-6">
            <BitNumberField Label="First Number" 
                            @bind-Value="firstNumber"
                            Adaptive="true" />
        </div>
        <div class="col-md-6">
            <BitNumberField Label="Second Number" 
                            @bind-Value="secondNumber"
                            Adaptive="true" />
        </div>
        <div class="col-12 mt-3">
            <div class="btn-group w-100">
                <BitButton Color="Color.Primary" @onclick="Add">+</BitButton>
                <BitButton Color="Color.Primary" @onclick="Subtract">-</BitButton>
                <BitButton Color="Color.Primary" @onclick="Multiply">×</BitButton>
                <BitButton Color="Color.Primary" @onclick="Divide">÷</BitButton>
            </div>
        </div>
        <div class="col-12 mt-3">
            <BitNumberField Label="Result" 
                            @bind-Value="result"
                            Readonly="true"
                            Adaptive="true" />
        </div>
    </div>
</div>

@code {
    private double firstNumber = 0;
    private double secondNumber = 0;
    private double result = 0;
    
    private void Add() => result = firstNumber + secondNumber;
    private void Subtract() => result = firstNumber - secondNumber;
    private void Multiply() => result = firstNumber * secondNumber;
    private void Divide() => result = secondNumber != 0 ? firstNumber / secondNumber : 0;
}
```

## Accessibility

BitNumberField includes comprehensive accessibility features:

### Screen Reader Support
- Proper labeling with `for` and `id` attributes
- Hidden accessibility text for increment/decrement buttons
- ARIA attributes for validation states

### Keyboard Navigation
- Tab navigation between fields and buttons
- Enter/Space to activate increment/decrement buttons
- Arrow keys for value adjustment (browser default)

### Visual Indicators
- Clear visual feedback for validation states
- Consistent focus indicators
- High contrast support

## Styling

### CSS Classes Applied

```css
/* Container */
.form-group { }

/* Label */
.input-number-label { }      /* Without symbol */
.input-symbol-label { }      /* With symbol */

/* Input group */
.input-group.input-number { }
.input-number-adaptive { }   /* When Adaptive=true */

/* Input field */
.form-control { }

/* Symbol */
.input-group-text { }

/* Increment/Decrement buttons */
.input-group-text.align-buttons { }
.input-number-add { }
.input-number-sub { }
```

### Custom Styling

```css
/* Custom number field styling */
.my-number-field .input-number {
    border-radius: 8px;
}

.my-number-field .input-number-add,
.my-number-field .input-number-sub {
    background: var(--bs-primary);
    color: white;
    border: none;
}

.my-number-field .input-number-add:hover,
.my-number-field .input-number-sub:hover {
    background: var(--bs-primary-dark);
}
```

## Browser Compatibility

- **Modern Browsers**: Full support for all features
- **Mobile**: Touch-friendly increment/decrement buttons
- **Accessibility**: Screen reader and keyboard navigation support
- **Progressive Enhancement**: Falls back to standard number input

## Related Components

- [`BitTextField`](text-field.md) - For text input fields
- [`BitPasswordField`](password-field.md) - For password input
- [`BitTextAreaField`](text-area-field.md) - For multi-line text input
- [Form Components Overview](form-components.md) - Complete form system guide