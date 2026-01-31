using System.Text.Json.Serialization;

namespace XrayOldworldLauncher.Models.Options;

public class OptionsState
{
    [JsonPropertyName("axrOptions")]
    public Dictionary<string, string> AxrOptions { get; set; } = new();

    [JsonPropertyName("userLtx")]
    public Dictionary<string, string> UserLtx { get; set; } = new();
}
