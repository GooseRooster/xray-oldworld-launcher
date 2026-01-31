using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class ControlPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "control",
        Groups = new()
        {
            new OptionGroup
            {
                Id = "general",
                Options = new()
                {
                    new() { Id = "mouse_sens", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "mouse_sens", DefaultValue = "0.15", Min = 0.001, Max = 0.6, Step = 0.01 },
                    new() { Id = "mouse_sens_aim", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "mouse_sens_aim", DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.05 },
                    new() { Id = "mouse_invert", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "mouse_invert", DefaultValue = "false" },
                }
            },
            new OptionGroup
            {
                Id = "toggles",
                Options = new()
                {
                    new() { Id = "crouch_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_crouch_toggle", DefaultValue = "true" },
                    new() { Id = "walk_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_walk_toggle", DefaultValue = "false" },
                    new() { Id = "sprint_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_sprint_toggle", DefaultValue = "true" },
                    new() { Id = "lookout_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_lookout_toggle", DefaultValue = "false" },
                    new() { Id = "aim_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "wpn_aim_toggle", DefaultValue = "false", BoolToNum = true },
                }
            },
            new OptionGroup
            {
                Id = "misc",
                Options = new()
                {
                    new() { Id = "pickup_mode", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_multi_item_pickup", DefaultValue = "true" },
                    new() { Id = "simple_pda_mode", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_simple_pda", DefaultValue = "true" },
                }
            }
        }
    };
}
