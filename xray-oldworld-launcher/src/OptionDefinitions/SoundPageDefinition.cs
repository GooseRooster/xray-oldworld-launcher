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
                        ConsoleCommand = "snd_volume_eff", DefaultValue = "1", Min = 0, Max = 1, Step = 0.05,
                        Label = "Master Volume", Description = "Overall game sound volume" },
                    new() { Id = "music_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "snd_volume_music", DefaultValue = "0.8", Min = 0, Max = 1, Step = 0.05,
                        Label = "Music Volume", Description = "Background music volume" },
                    new() { Id = "eax", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "EAX / Environmental Audio",
                        Description = "Enable environmental audio effects (reverb, echo)" },
                    new() { Id = "caption", Type = OptionType.List, ValueType = OptionValueType.String,
                        DefaultValue = "none", Label = "Captions",
                        Description = "Show text captions for audio",
                        Content = new()
                        {
                            new() { Value = "none", DisplayText = "Off" },
                            new() { Value = "action", DisplayText = "Action Only" },
                            new() { Value = "all", DisplayText = "All" }
                        }},
                }
            },
            new OptionGroup
            {
                Id = "environment",
                Label = "Environment",
                Options = new()
                {
                    new() { Id = "ambient_volume", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        DefaultValue = "1", Min = 0, Max = 1, Step = 0.05,
                        Label = "Ambient Volume", Description = "Volume of ambient environmental sounds" },
                    new() { Id = "wind_sound", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "Wind Sound",
                        Description = "Enable dynamic wind audio" },
                    new() { Id = "helmet_rain_sound", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "false", Label = "Helmet Rain Sound",
                        Description = "Rain sound effect when wearing a helmet" },
                    new() { Id = "breathing_sound", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "false", Label = "Breathing Sound",
                        Description = "Player breathing audio" },
                }
            },
            new OptionGroup
            {
                Id = "radio",
                Label = "Zone FM Radio",
                Options = new()
                {
                    new() { Id = "zone", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "Zone FM Radio",
                        Description = "Enable the Zone FM radio system" },
                    new() { Id = "emission_intereferences", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "Emission Interference",
                        Description = "Radio static during emissions" },
                    new() { Id = "underground_intereferences", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "Underground Interference",
                        Description = "Radio static in underground areas" },
                    new() { Id = "display_tracks", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "false", Label = "Display Track Names",
                        Description = "Show the name of the currently playing track" },
                }
            }
        }
    };
}
