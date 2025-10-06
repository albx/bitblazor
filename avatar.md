# BitAvatar

The `BitAvatar` component and its related components represent an [avatar using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/avatar/).



// TODO


## Namespace

```csharp
BitBlazor.Components
```

## Description

The Card system provides a flexible and extensible way to display content in well-organized containers. The system includes the main `BitCard` component and various sub-components for structuring content.

## Main Component: BitCard

### Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | The content of the card |
| `Type` | `CardType` | ✗ | `CardType.Default` | The type of card to display |
| `Bordered` | `bool` | ✗ | `true` | Indicates if the card should have a border |
| `Shadow` | `CardShadow` | ✗ | `CardShadow.Small` | The shadow style of the card |
| `FullHeight` | `bool` | ✗ | `false` | Indicates if the card should occupy the full height of its container |
| `Inline` | `bool` | ✗ | `false` | Indicates if content should be displayed inline |
| `Reverse` | `bool` | ✗ | `false` | Reverses the inline card layout |
| `Mini` | `bool` | ✗ | `false` | Displays the inline card in mini version |
| `BorderTopColor` | `Color?` | ✗ | `null` | Color of the card's top border |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes |

### Enumerations

#### CardType
| Value | Description |
|-------|-------------|
| `Default` | Standard card |
| `Profile` | Card for user profiles |
| `Banner` | Card with banner layout |

#### CardShadow
| Value | Description |
|-------|-------------|
| `Small` | Small shadow |
| `Medium` | Medium shadow |
| `Large` | Large shadow |

## Sub-components

### CardBody
Represents the main body of the card.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the card body

### CardTitle
Represents the card title.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the title
- `Typography` (`Typography`): The heading level (default: H3)
- `HasIcon` (`bool`): Indicates if the title has an associated icon

### CardSubtitle
Represents the card subtitle.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the subtitle

### CardText
Represents the card text.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the text

### CardImage
Manages card images.

**Parameters:**
- `ImageSrc` (`string`): Image URL (required)
- `ImageAlt` (`string`): Alternative text (default: empty string)
- `Ratio` (`Ratio`): Aspect ratio of the image (default: `Ratio.Ratio1x1`)

### CardImageWrapper
Wrapper for card images.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the wrapper

### CardFooter
Represents the card footer.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the footer

### CardDate
Displays dates in the card.

**Parameters:**
- `Date` (`DateTime`): The date to display (required)
- `DatetimeFormat` (`string`): Format for the datetime attribute (required, default: "yyyy-MM-dd")
- `DisplayFormat` (`string`): Format for displaying the date (required)
- `TextColor` (`Color?`): Optional color for the date text

### CardSignature
Represents a signature in the card.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the signature

### CardProfileHeader
Header specific for profile cards.

**Parameters:**
- `ChildContent` (`RenderFragment`): The content of the header

### CardProfileIcon
Icon for profile cards.

**Parameters:**
- `IconName` (`string`): Icon name
- `IconSize` (`IconSize`): Icon size

### CardBannerIcon
Icon for banner cards.

**Parameters:**
- `IconName` (`string`): Icon name
- `IconColor` (`IconColor`): Icon color

### CardTitleIcon
Icon for the card title.

**Parameters:**
- `IconName` (`string`): Icon name

### InlineCardContent
Content specific for inline cards.

**Parameters:**
- `ChildContent` (`RenderFragment`): The inline content

## Usage Examples

### Simple card

```razor
<BitCard>
    <CardTitle>Card Title</CardTitle>
    <CardBody>
        <CardText>
            This is the card content. It can contain any type of content.
        </CardText>
    </CardBody>
</BitCard>
```

### Card with image

```razor
<BitCard>
    <CardTitle>Card with Image</CardTitle>
    <CardImageWrapper>
        <CardImage ImageSrc="/images/example.jpg" 
                   ImageAlt="Example" 
                   Ratio="Ratio.Ratio16x9" />
    </CardImageWrapper>
    <CardBody>
        <CardText>
            This card includes an image at the top.
        </CardText>
    </CardBody>
</BitCard>
```

### Profile card

```razor
<BitCard Type="CardType.Profile" Shadow="CardShadow.Medium">
    <CardProfileHeader>
        <ProfileName>John Doe</ProfileName>
        <ProfileType>Developer</ProfileType>
        <ProfileImage>
            <CardProfileIcon IconName="@Icons.ItUser"
                             IconColor="IconColor.Primary"
                             AriaHidden="true"/>
        </ProfileImage>
    </CardProfileHeader>
    <CardBody>
        <CardText>
            Senior developer with experience in .NET and Blazor
        </CardText>
    </CardBody>
    <CardFooter>
        <BitButton Color="Color.Primary" Size="Size.Small">
            Contact
        </BitButton>
    </CardFooter>
</BitCard>
```

### Banner card

```razor
<BitCard Type="CardType.Banner">
    <CardTitle>
        <a href="#">Content title</a>
    </CardTitle>
    
    <CardBannerIcon Icon="@Icons.ItChartLine"
                    IconColor="IconColor.Secondary"
                    AriaHidden="true" />
    
    <CardBody>
        <CardSubtitle>Subtitle</CardSubtitle>
    </CardBody>
</BitCard>
```

### Inline card

```razor
<BitCard Inline="true">
    <InlineCardContent>
        <CardTitle>Recent Article</CardTitle>
        <CardText>Brief description of the article...</CardText>
    </InlineCardContent>
    <CardImageWrapper>
        <CardImage ImageSrc="/images/example.jpg" ImageAlt="Example" />
    </CardImageWrapper>
</BitCard>
```

### Card with colored border

```razor
<BitCard BorderTopColor="Color.Primary">
    <CardTitle HasIcon="true">
        Important Information
        <CardTitleIcon IconName="@Icons.ItInfo" />
    </CardTitle>
    <CardBody>
        <CardText>
            This card has a colored top border to attract attention.
        </CardText>
    </CardBody>
</BitCard>
```

### Card with date and signature

```razor
<BitCard>
    <CardTitle>Official Document</CardTitle>
    <CardBody>
        <CardSignature>
            by John Doe
        </CardSignature>
        <CardText>
            Document content that requires a signature and date.
        </CardText>
    </CardBody>
    <CardFooter>
        <CardDate Date="DateTime.Now" 
                  DatetimeFormat="yyyy-MM-dd" 
                  DisplayFormat="dd MMM yyyy" 
                  TextColor="Color.Secondary" />
    </CardFooter>
</BitCard>
```

### Card with custom shadow

```razor
<BitCard Shadow="CardShadow.Large" FullHeight="true">
    <CardTitle>Prominent Card</CardTitle>
    <CardBody>
        <CardText>
            This card has a large shadow and occupies the full available height.
        </CardText>
    </CardBody>
</BitCard>
```

## Generated HTML Structure

The main `BitCard` component generates the following HTML structure:

```html
<article class="it-card rounded border shadow-sm">
    <!-- Card content -->
</article>
```

### Sub-component HTML structures

**CardTitle:**
```html
<h3 class="it-card-title">
    Title content
</h3>
<!-- OR with icon -->
<h3 class="it-card-title it-card-title-icon">
    Title content
</h3>
```

**CardDate:**
```html
<time datetime="2024-01-01" class="it-card-date text-secondary">
    01 Jan 2024
</time>
```

**CardBody:**
```html
<div class="it-card-body">
    Body content
</div>
```

## Generated CSS Classes

### Base classes
- `it-card`: Base card class
- `rounded`: Rounded corners

### Type classes
- `it-card-profile`: For profile cards
- `it-card-banner`: For banner cards

### Inline classes
- `it-card-inline`: Inline layout
- `it-card-inline-mini`: Mini inline version
- `it-card-inline-reverse`: Reversed inline layout

### Style classes
- `border`: Visible border
- `shadow-sm`: Small shadow
- `shadow`: Medium shadow
- `shadow-lg`: Large shadow
- `it-card-height-full`: Full height
- `it-card-image`: Card with image

### Top border classes
- `it-card-border-top`: Top border enabled
- `it-card-border-top-primary`: Primary border
- `it-card-border-top-secondary`: Secondary border
- `it-card-border-top-success`: Success border
- `it-card-border-top-warning`: Warning border
- `it-card-border-top-danger`: Danger border

## Notes

- The system uses `CascadingValue` to share context between the main component and sub-components
- Cards support different layouts (standard, profile, banner, inline)
- Images in cards automatically notify the parent component of their presence
- The `Mini` and `Reverse` parameters only work when `Inline` is `true`
- The component supports full accessibility using the semantic `<article>` tag
