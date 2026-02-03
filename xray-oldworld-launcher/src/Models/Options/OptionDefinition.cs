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

public class OptionDefinition
{
    /// Unique identifier within the group (e.g., "show_crosshair")
    /// Also serves as the localization key: opt.{Id} for label, opt.{Id}.desc for description
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

    /// For List/Radio types: available choice values
    /// Display text is resolved via LocalizationService.ContentText(Id, value)
    public List<string>? Content { get; set; }

    /// If true, boolean values are written as "1"/"0" instead of "on"/"off"
    /// (relevant for console commands in user.ltx)
    public bool BoolToNum { get; set; }

    /// Decimal precision for display (Track type)
    public int Precision { get; set; } = 2;

    /// <summary>
    /// Optional visibility condition. When set, the option is only shown if this
    /// function returns true. Receives the current CmdValues dictionary.
    /// </summary>
    public Func<Dictionary<string, string>, bool>? VisibleWhen { get; set; }
}
