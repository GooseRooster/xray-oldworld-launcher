using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class SoundPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "sound",
        Label = "Sound",
        Groups = new()
        {
            new OptionGroup
            {
                Id = "general",
                Label = "General",
                Options = new()
                {
                    new() { Id = "master_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "snd_volume_eff", DefaultValue = "1.0", Min = 0, Max = 1, Step = 0.1,
                        Label = "Master Volume", Description = "Overall game sound volume" },
                    new() { Id = "music_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "snd_volume_music", DefaultValue = "0.8", Min = 0, Max = 1, Step = 0.1,
                        Label = "Music Volume", Description = "Background music volume" },
                    new() { Id = "eax", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "snd_efx", DefaultValue = "true",
                        Label = "EAX / Environmental Audio",
                        Description = "Enable environmental audio effects (reverb, echo)" },
                    new() { Id = "dynamic_music", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_dynamic_music", DefaultValue = "false",
                        Label = "Dynamic Music",
                        Description = "Music reacts to gameplay events" },
                }
            },
        }
    };
}
