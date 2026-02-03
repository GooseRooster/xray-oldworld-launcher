using System.Text.Json.Serialization;

namespace XrayOldworldLauncher.Models.Options;

public class LauncherConfig
{
    [JsonPropertyName("useAvx")]
    public bool UseAvx { get; set; }

    [JsonPropertyName("debugMode")]
    public bool DebugMode { get; set; }

    [JsonPropertyName("shadowMapSize")]
    public int ShadowMapSize { get; set; } = 2048;

    [JsonPropertyName("customArgs")]
    public string CustomArgs { get; set; } = "";

    [JsonPropertyName("gameRoot")]
    public string? GameRoot { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; } = "en";

    [JsonPropertyName("linuxCustomCommand")]
    public string? LinuxCustomCommand { get; set; }

    [JsonPropertyName("dynamicLightingPreset")]
    public string DynamicLightingPreset { get; set; } = "medium";

    [JsonPropertyName("staticLightingPreset")]
    public string StaticLightingPreset { get; set; } = "medium";
}
