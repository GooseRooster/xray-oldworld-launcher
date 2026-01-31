using System.Text.Json.Serialization;

namespace XrayOldworldLauncher.Models.Options;

public class GamePaths
{
    [JsonPropertyName("gameRoot")]
    public string GameRoot { get; set; } = "";

    [JsonPropertyName("appdata")]
    public string Appdata { get; set; } = "";

    [JsonPropertyName("gamedata")]
    public string Gamedata { get; set; } = "";

    [JsonPropertyName("bin")]
    public string Bin { get; set; } = "";

    [JsonPropertyName("launcherDir")]
    public string LauncherDir { get; set; } = "";
}
