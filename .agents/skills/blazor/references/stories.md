# Stories with BlazingStory

Every component must have a corresponding story file so it appears in the live Storybook. Story files use the [BlazingStory](https://github.com/jsakamoto/BlazingStory) library.

## File Location

```
stories/BitBlazor.Stories/Components/Stories/<Namespace>/BitXxx.stories.razor
```

The subfolder under `Stories/` must mirror the component's namespace segment — the same path used for the component's source and test files. For example, a component in `BitBlazor.Components` goes under `Stories/Components/`, and one in `BitBlazor.Form` goes under `Stories/Form/`.

## Structure

```razor
@attribute [Stories("Components/BitXxx")]

<Stories TComponent="BitXxx">

    @* Declare controls for each enum/value parameter *@
    <ArgType For="_ => _.Color" Control="ControlType.Select" />
    <ArgType For="_ => _.Size" Control="ControlType.Select" DefaultValue="Size.Default" />

    @* Default story — shows the component in its most common state *@
    <Story Name="Default">
        <Arguments>
            <Arg For="_ => _.Color" Value="Color.Primary" />
        </Arguments>
        <Template>
            <div class="py-2 px-2">
                <BitXxx @attributes="context.Args">Content</BitXxx>
            </div>
        </Template>
    </Story>

    @* Variant stories — one per meaningful visual state *@
    <Story Name="Colors">
        <Template>
            <div class="py-2 px-2">
                <BitXxx Color="Color.Primary">Primary</BitXxx>
                <BitXxx Color="Color.Secondary">Secondary</BitXxx>
                <BitXxx Color="Color.Success">Success</BitXxx>
                <BitXxx Color="Color.Danger">Danger</BitXxx>
            </div>
        </Template>
    </Story>

    <Story Name="Sizes">
        <Template>
            <div class="py-2 px-2">
                <BitXxx Color="Color.Primary" Size="Size.Small">Small</BitXxx>
                <BitXxx Color="Color.Primary" Size="Size.Default">Default</BitXxx>
                <BitXxx Color="Color.Primary" Size="Size.Large">Large</BitXxx>
            </div>
        </Template>
    </Story>

    <Story Name="Disabled">
        <Template>
            <div class="py-2 px-2">
                <BitXxx Color="Color.Primary" Disabled="true">Disabled</BitXxx>
            </div>
        </Template>
    </Story>

</Stories>
```

## Conventions

- The `[Stories(...)]` path must match the component's namespace segment (`"Components/BitXxx"` or `"Form/BitXxx"`) to appear in the correct Storybook section
- Always include a **Default** story that uses `@attributes="context.Args"` so the Storybook controls panel works
- Declare `<ArgType>` for every enum or selectable parameter; use `DefaultValue` to pre-select the most common value
- Add one story per meaningful visual variant: colors, sizes, disabled state, with icon, outline variant, etc.
- Wrap each story template in `<div class="py-2 px-2">` for consistent padding in the preview
- Form component stories must supply a minimal `EditForm` + model wrapper so the component is interactive in the preview
