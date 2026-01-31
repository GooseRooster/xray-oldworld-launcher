using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class GameplayPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "gameplay",
        Label = "Gameplay",
        Groups = new()
        {
            GeneralGroup(),
            SilentKillsGroup(),
            DisguiseGroup(),
            FastTravelGroup(),
            BackpackTravelGroup(),
            AmmoBreakdownGroup(),
            ArtefactConditionGroup(),
            FactionEconomyGroup(),
        }
    };

    private static OptionGroup GeneralGroup() => new()
    {
        Id = "general",
        Label = "General",
        Options = new()
        {
            new() { Id = "outfit_portrait", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Outfit Portrait",
                Description = "Show faction outfit portrait in inventory" },
            new() { Id = "hardcore_ai_aim", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Hardcore AI Aim",
                Description = "AI has improved accuracy — more challenging combat" },
            new() { Id = "corpse_max_count", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 0, Max = 30, Step = 1, Precision = 0,
                Label = "Max Corpse Count", Description = "Maximum number of corpses before oldest are removed" },
            new() { Id = "corpse_min_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "75", Min = 10, Max = 300, Step = 5, Precision = 0,
                Label = "Min Corpse Distance", Description = "Minimum distance from player before corpses can be removed" },
            new() { Id = "npc_loot_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 0, Max = 50, Step = 1, Precision = 0,
                Label = "NPC Loot Distance", Description = "Distance at which NPCs can loot" },
            new() { Id = "max_tasks", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 1, Max = 10, Step = 1, Precision = 0,
                Label = "Max Active Tasks", Description = "Maximum number of simultaneous active tasks" },
            new() { Id = "mechanic_feature", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Mechanic Feature",
                Description = "Enable mechanic NPC features" },
            new() { Id = "release_dropped_items", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Release Dropped Items",
                Description = "Dropped items are released from inventory" },
            new() { Id = "show_tip_reputation", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Show Reputation Tips",
                Description = "Show reputation change notifications" },
        }
    };

    private static OptionGroup SilentKillsGroup() => new()
    {
        Id = "silent_kills",
        Label = "Silent Kills",
        Options = new()
        {
            new() { Id = "sk_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Enable Silent Kills",
                Description = "Enable the silent kill system" },
            new() { Id = "sk_melee_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Melee Silent Kills",
                Description = "Allow silent kills with melee weapons" },
            new() { Id = "sk_gun_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Gun Silent Kills",
                Description = "Allow silent kills with suppressed firearms" },
            new() { Id = "sk_headshot_only", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Headshot Only",
                Description = "Silent kills require headshots" },
            new() { Id = "sk_all_melee_ok", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "All Melee Weapons",
                Description = "All melee weapons can perform silent kills (not just knife)" },
            new() { Id = "sk_fresh_time", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "300", Min = 10, Max = 1200, Step = 10, Precision = 0,
                Label = "Fresh Kill Time (s)", Description = "Time window before a silent kill body is discovered" },
            new() { Id = "sk_melee_hear_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 1, Max = 50, Step = 1, Precision = 0,
                Label = "Melee Hear Distance", Description = "Distance at which melee kills can be heard" },
            new() { Id = "sk_gun_hear_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "10", Min = 1, Max = 100, Step = 1, Precision = 0,
                Label = "Gun Hear Distance", Description = "Distance at which suppressed gun kills can be heard" },
            new() { Id = "sk_suspect_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "10", Min = 1, Max = 100, Step = 1, Precision = 0,
                Label = "Suspect Distance", Description = "Distance at which NPCs become suspicious of a kill" },
        }
    };

    private static OptionGroup DisguiseGroup() => new()
    {
        Id = "disguise",
        Label = "Disguise System",
        Options = new()
        {
            new() { Id = "state", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Enable Disguise System",
                Description = "Enable the faction disguise system" },
            new() { Id = "active_item", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Active Item Factor",
                Description = "Held items affect disguise effectiveness" },
            new() { Id = "weapon", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Weapon Factor",
                Description = "Visible weapon affects disguise" },
            new() { Id = "outfit", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Outfit Factor",
                Description = "Worn outfit affects disguise effectiveness" },
            new() { Id = "helmet", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Helmet Factor",
                Description = "Wearing a helmet affects disguise" },
            new() { Id = "distance", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Distance Factor",
                Description = "Distance affects disguise detection" },
            new() { Id = "speed", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Speed Factor",
                Description = "Movement speed affects disguise detection" },
            new() { Id = "stay_time", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Stay Time Factor",
                Description = "Time spent near NPCs affects disguise detection" },
        }
    };

    private static OptionGroup FastTravelGroup() => new()
    {
        Id = "fast_travel",
        Label = "Fast Travel",
        Options = new()
        {
            new() { Id = "state", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "1", Label = "Fast Travel Mode",
                Description = "Enable fast travel and set mode",
                Content = new()
                {
                    new() { Value = "0", DisplayText = "Disabled" },
                    new() { Value = "1", DisplayText = "Enabled" },
                    new() { Value = "2", DisplayText = "Guides Only" },
                }},
            new() { Id = "time", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Time Passes",
                Description = "Game time passes during fast travel" },
            new() { Id = "on_combat", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow During Combat",
                Description = "Allow fast travel while in combat" },
            new() { Id = "on_emission", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow During Emissions",
                Description = "Allow fast travel during emissions" },
            new() { Id = "on_overweight", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow While Overweight",
                Description = "Allow fast travel while overweight" },
            new() { Id = "on_damage", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow While Damaged",
                Description = "Allow fast travel while heavily damaged" },
        }
    };

    private static OptionGroup BackpackTravelGroup() => new()
    {
        Id = "backpack_travel",
        Label = "Backpack Stash Travel",
        Options = new()
        {
            new() { Id = "state", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Enable Backpack Travel",
                Description = "Enable travel via backpack stash system" },
            new() { Id = "time", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Time Passes",
                Description = "Game time passes during backpack travel" },
            new() { Id = "on_combat", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow During Combat" },
            new() { Id = "on_emission", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow During Emissions" },
            new() { Id = "on_overweight", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow While Overweight" },
            new() { Id = "on_damage", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Allow While Damaged" },
        }
    };

    private static OptionGroup AmmoBreakdownGroup() => new()
    {
        Id = "ammo_breakdown",
        Label = "Ammo Breakdown",
        Options = new()
        {
            new() { Id = "casing", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Casing Recovery %", Description = "Chance to recover casings when breaking down ammo" },
            new() { Id = "powder", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "100", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Powder Recovery %", Description = "Chance to recover powder" },
            new() { Id = "bullet", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "100", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Bullet Recovery %", Description = "Chance to recover bullets" },
            new() { Id = "new_salvage", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "90", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "New Salvage %", Description = "Salvage rate for new ammo" },
            new() { Id = "old_salvage", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "70", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Old Salvage %", Description = "Salvage rate for used ammo" },
        }
    };

    private static OptionGroup ArtefactConditionGroup() => new()
    {
        Id = "artefact_condition",
        Label = "Artefact Condition",
        Options = new()
        {
            new() { Id = "enable_condition_bar", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Show Condition Bar",
                Description = "Display condition bar on artefacts" },
            new() { Id = "enable_degraded_drops", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Degraded Drops",
                Description = "Artefacts can drop in degraded condition" },
            new() { Id = "mutant_part_condition_min", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Mutant Part Min Condition", Description = "Minimum condition of mutant part drops" },
            new() { Id = "mutant_part_condition_max", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "100", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Mutant Part Max Condition", Description = "Maximum condition of mutant part drops" },
        }
    };

    private static OptionGroup FactionEconomyGroup() => new()
    {
        Id = "faction_economy",
        Label = "Faction Economy",
        Options = new()
        {
            new() { Id = "use_faction_discounts", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Faction Discounts",
                Description = "Enable faction-based trading discounts" },
            new() { Id = "use_goodwill_mul", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Goodwill Multiplier",
                Description = "Goodwill affects trade prices" },
            new() { Id = "goodwill_mul", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.5", Min = 0.1, Max = 5.0, Step = 0.1,
                Label = "Goodwill Multiplier Value", Description = "How much goodwill affects prices" },
            new() { Id = "use_reputation_mul", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Reputation Multiplier",
                Description = "Reputation affects trade prices" },
            new() { Id = "rep_mul", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0.1, Max = 5.0, Step = 0.1,
                Label = "Reputation Multiplier Value", Description = "How much reputation affects prices" },
            new() { Id = "use_trade_GUI", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Trade GUI",
                Description = "Use the faction trade GUI interface" },
            new() { Id = "use_special_ammo_spawns", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Special Ammo Spawns",
                Description = "Enable special ammo availability at faction traders" },
            new() { Id = "ammo_amount", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "4", Min = 1, Max = 20, Step = 1, Precision = 0,
                Label = "Ammo Amount", Description = "Base amount of ammo available at traders" },
            new() { Id = "ammo_amount_special", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 1, Max = 20, Step = 1, Precision = 0,
                Label = "Special Ammo Amount", Description = "Amount of special ammo at traders" },
            new() { Id = "required_supply_level", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 5, Step = 1, Precision = 0,
                Label = "Required Supply Level", Description = "Minimum supply level for faction trading" },
            new() { Id = "chance_for_extra_item", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "40", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Extra Item Chance %", Description = "Chance for traders to have extra items" },
        }
    };
}
