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
                Label = "General",
                Options = new()
                {
                    new() { Id = "mouse_sens", Type = OptionType.Track, ValueType = OptionValueType.Float,
                        ConsoleCommand = "mouse_sens", DefaultValue = "0.091", Min = 0.01, Max = 1.0, Step = 0.001,
                        Label = "Mouse Sensitivity", Description = "Mouse look sensitivity", Precision = 3 },
                    new() { Id = "disassembly_warning", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                        DefaultValue = "true", Label = "Disassembly Warning",
                        Description = "Show warning before disassembling items" },
                }
            }
        }
    };
}
