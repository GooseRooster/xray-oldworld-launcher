using System.Text.Json.Serialization;

namespace XrayOldworldLauncher.Models.Options;

public class OptionChange
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("storageType")]
    public string StorageType { get; set; } = "axrOptions";

    /// Only set when StorageType is "userLtx"
    [JsonPropertyName("cmd")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Cmd { get; set; }

    public static OptionChange ForAxrOptions(string path, string value) => new()
    {
        Path = path,
        Value = value,
        StorageType = "axrOptions"
    };

    public static OptionChange ForUserLtx(string cmd, string value) => new()
    {
        Path = cmd,
        Value = value,
        StorageType = "userLtx",
        Cmd = cmd
    };
}
