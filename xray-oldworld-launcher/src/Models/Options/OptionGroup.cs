namespace XrayOldworldLauncher.Models.Options;

public class OptionGroup
{
    /// Group identifier matching the defaults LTX section (e.g., "hud", "general")
    public string Id { get; set; } = "";

    /// Display label for the group header
    public string Label { get; set; } = "";

    /// Options within this group
    public List<OptionDefinition> Options { get; set; } = new();
}
