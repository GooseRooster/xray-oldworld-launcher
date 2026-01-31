namespace XrayOldworldLauncher.Models.Options;

public class OptionPage
{
    /// Page identifier matching the tab id (e.g., "video", "sound")
    /// Also serves as the localization key: page.{Id}
    public string Id { get; set; } = "";

    /// Groups of options within this page
    public List<OptionGroup> Groups { get; set; } = new();
}
