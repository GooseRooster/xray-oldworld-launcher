using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class ControlPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "control",
        Label = "Controls",
        Groups = new()
        {
            new OptionGroup
            {
                Id = "general",
                Label = "Mouse",
                Options = new()
                {
                    new() { Id = "mouse_sens", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "mouse_sens", DefaultValue = "0.15", Min = 0.001, Max = 0.6, Step = 0.01,
                        Label = "Mouse Sensitivity", Description = "Mouse look sensitivity" },
                    new() { Id = "mouse_sens_aim", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "mouse_sens_aim", DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.05,
                        Label = "Aim Sensitivity", Description = "Mouse sensitivity multiplier while aiming" },
                    new() { Id = "mouse_invert", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "mouse_invert", DefaultValue = "false",
                        Label = "Invert Mouse", Description = "Invert vertical mouse look" },
                }
            },
            new OptionGroup
            {
                Id = "toggles",
                Label = "Toggles",
                Options = new()
                {
                    new() { Id = "crouch_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_crouch_toggle", DefaultValue = "true",
                        Label = "Crouch Toggle", Description = "Toggle crouch instead of hold" },
                    new() { Id = "walk_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_walk_toggle", DefaultValue = "false",
                        Label = "Walk Toggle", Description = "Toggle walk instead of hold" },
                    new() { Id = "sprint_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_sprint_toggle", DefaultValue = "true",
                        Label = "Sprint Toggle", Description = "Toggle sprint instead of hold" },
                    new() { Id = "lookout_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_lookout_toggle", DefaultValue = "false",
                        Label = "Lean Toggle", Description = "Toggle lean instead of hold" },
                    new() { Id = "aim_toggle", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "wpn_aim_toggle", DefaultValue = "false", BoolToNum = true,
                        Label = "Aim Toggle", Description = "Toggle aim instead of hold" },
                }
            },
            new OptionGroup
            {
                Id = "misc",
                Label = "Miscellaneous",
                Options = new()
                {
                    new() { Id = "pickup_mode", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_multi_item_pickup", DefaultValue = "true",
                        Label = "Multi-Item Pickup", Description = "Pick up multiple items at once" },
                    new() { Id = "simple_pda_mode", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        ConsoleCommand = "g_simple_pda", DefaultValue = "true",
                        Label = "Simple PDA", Description = "Use simplified PDA interface" },
                }
            }
        }
    };
}
