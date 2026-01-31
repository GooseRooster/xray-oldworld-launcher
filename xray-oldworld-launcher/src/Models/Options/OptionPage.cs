namespace XrayOldworldLauncher.Models.Options;

public class OptionPage
{
    /// Page identifier matching the tab id (e.g., "video", "sound")
    public string Id { get; set; } = "";

    /// Display label for the page
    public string Label { get; set; } = "";

    /// Groups of options within this page
    public List<OptionGroup> Groups { get; set; } = new();
}
