namespace XrayOldworldLauncher.Models.Options;

public enum OptionType
{
    Check,
    Track,
    List,
    RadioH,
    RadioV,
    Input,
    Line,
    Title
}

public enum OptionValueType
{
    String = 0,
    Bool = 1,
    Float = 2
}

public class ListItem
{
    public string Value { get; set; } = "";
    public string? DisplayText { get; set; }
}

public class OptionDefinition
{
    /// Unique identifier within the group (e.g., "show_crosshair")
    public string Id { get; set; } = "";

    /// The type of UI control to render
    public OptionType Type { get; set; }

    /// How the value is stored/parsed
    public OptionValueType ValueType { get; set; }

    /// If set, value is stored in user.ltx as this console command
    /// If null, value is stored in axr_options.ltx
    public string? ConsoleCommand { get; set; }

    /// Default value as a string
    public string DefaultValue { get; set; } = "";

    /// For Track type: minimum value
    public double? Min { get; set; }

    /// For Track type: maximum value
    public double? Max { get; set; }

    /// For Track type: step increment
    public double? Step { get; set; }

    /// For List/Radio types: available choices
    public List<ListItem>? Content { get; set; }

    /// Display label for the option
    public string Label { get; set; } = "";

    /// Tooltip/description text
    public string? Description { get; set; }

    /// If true, boolean values are written as "1"/"0" instead of "on"/"off"
    /// (relevant for console commands in user.ltx)
    public bool BoolToNum { get; set; }

    /// Decimal precision for display (Track type)
    public int Precision { get; set; } = 2;
}
