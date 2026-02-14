using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class SoundPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "sound",
        Groups = new()
        {
            new OptionGroup
            {
                Id = "general",
                Options = new()
                {
                    new() { Id = "master_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "snd_volume_eff", DefaultValue = "1.0", Min = 0, Max = 1, Step = 0.1 },
                    new() { Id = "music_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "snd_volume_music", DefaultValue = "0.8", Min = 0, Max = 1, Step = 0.1 },
                    new() { Id = "audio_effects_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                        ConsoleCommand = "snd_audio_effects_quality", DefaultValue = "st_opt_medium",
                        Content = new() { "st_opt_low", "st_opt_medium", "st_opt_high" } },
                    new() { Id = "dynamic_music", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_dynamic_music", DefaultValue = "false" },
                }
            },
        }
    };
}
