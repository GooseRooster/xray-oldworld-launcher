namespace XrayOldworldLauncher.Models.Options;

public class OptionGroup
{
    /// Group identifier matching the defaults LTX section (e.g., "hud", "general")
    /// Also serves as the localization key: group.{pageId}.{Id}
    public string Id { get; set; } = "";

    /// Options within this group
    public List<OptionDefinition> Options { get; set; } = new();
}
