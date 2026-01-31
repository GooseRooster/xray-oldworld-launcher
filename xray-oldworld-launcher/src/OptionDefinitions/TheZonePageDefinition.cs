using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class TheZonePageDefinition
{
    private static readonly string[] Factions = {
        "stalker", "army", "bandit", "csky", "dolg", "ecolog",
        "freedom", "greh", "isg", "killer", "monolith", "renegade", "zombied"
    };

    private static readonly Dictionary<string, string> FactionNames = new()
    {
        {"stalker", "Loners"}, {"army", "Military"}, {"bandit", "Bandits"},
        {"csky", "Clear Sky"}, {"dolg", "Duty"}, {"ecolog", "Ecologists"},
        {"freedom", "Freedom"}, {"greh", "Sin"}, {"isg", "ISG"},
        {"killer", "Mercenaries"}, {"monolith", "Monolith"},
        {"renegade", "Renegades"}, {"zombied", "Zombified"}
    };

    public static OptionPage Build() => new()
    {
        Id = "alife",
        Label = "The Zone",
        Groups = BuildGroups()
    };

    private static List<OptionGroup> BuildGroups()
    {
        var groups = new List<OptionGroup>
        {
            GeneralGroup(),
            EventsGroup(),
            DynamicNewsGroup(),
            NeutralZoneGroup(),
            NpcFleeingGroup(),
            NpcWoundedGroup(),
            WarfareGeneralGroup(),
            WarfareAzazelGroup(),
        };

        // Add per-faction warfare groups
        foreach (var faction in Factions)
        {
            groups.Add(BuildFactionWarfareGroup(faction));
        }

        return groups;
    }

    private static OptionGroup GeneralGroup() => new()
    {
        Id = "general",
        Label = "A-Life General",
        Options = new()
        {
            new() { Id = "alife_stalker_pop", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.3", Min = 0.1, Max = 2.0, Step = 0.1,
                Label = "Stalker Population", Description = "Population multiplier for stalkers" },
            new() { Id = "alife_mutant_pop", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0.1, Max = 2.0, Step = 0.1,
                Label = "Mutant Population", Description = "Population multiplier for mutants" },
            new() { Id = "dynamic_anomalies", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Dynamic Anomalies",
                Description = "Anomalies shift position after blowouts" },
            new() { Id = "dynamic_relations", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Dynamic Relations",
                Description = "Faction relations change dynamically" },
            new() { Id = "excl_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "75", Min = 10, Max = 300, Step = 5, Precision = 0,
                Label = "Exclusion Distance", Description = "Minimum distance for A-Life spawning near player" },
            new() { Id = "offline_combat", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "full", Label = "Offline Combat",
                Description = "How NPCs fight when player is not nearby",
                Content = new()
                {
                    new() { Value = "off", DisplayText = "Disabled" },
                    new() { Value = "simple", DisplayText = "Simple" },
                    new() { Value = "full", DisplayText = "Full" },
                }},
            new() { Id = "heli_spawn", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Helicopter Spawns",
                Description = "Enable military helicopter patrols" },
            new() { Id = "heli_engine_sound", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Helicopter Engine Sound",
                Description = "Hear helicopter engine from distance" },
            new() { Id = "war_goodwill_reset", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "War Goodwill Reset",
                Description = "Reset goodwill on warfare events" },
        }
    };

    private static OptionGroup EventsGroup() => new()
    {
        Id = "event",
        Label = "Zone Events",
        Options = new()
        {
            new() { Id = "emission_state", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Emissions Enabled",
                Description = "Enable periodic emission events" },
            new() { Id = "emission_frequency", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "24", Min = 1, Max = 168, Step = 1, Precision = 0,
                Label = "Emission Frequency (hours)", Description = "Average hours between emissions" },
            new() { Id = "emission_task", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Emission Tasks",
                Description = "Generate tasks related to emissions" },
            new() { Id = "emission_fate", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "kill_at_wave", Label = "Emission Fate",
                Description = "What happens to NPCs caught in emissions",
                Content = new()
                {
                    new() { Value = "kill_at_wave", DisplayText = "Kill at Wave" },
                    new() { Value = "kill_at_peak", DisplayText = "Kill at Peak" },
                    new() { Value = "damage_only", DisplayText = "Damage Only" },
                }},
            new() { Id = "emission_warning", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "siren_radio", Label = "Emission Warning",
                Description = "How the player is warned of emissions",
                Content = new()
                {
                    new() { Value = "siren", DisplayText = "Siren Only" },
                    new() { Value = "radio", DisplayText = "Radio Only" },
                    new() { Value = "siren_radio", DisplayText = "Siren + Radio" },
                    new() { Value = "none", DisplayText = "None" },
                }},
            new() { Id = "psi_storm_state", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Psi Storms Enabled",
                Description = "Enable periodic psi storm events" },
            new() { Id = "psi_storm_frequency", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "24", Min = 1, Max = 168, Step = 1, Precision = 0,
                Label = "Psi Storm Frequency (hours)", Description = "Average hours between psi storms" },
            new() { Id = "psi_storm_task", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Psi Storm Tasks" },
            new() { Id = "psi_storm_fate", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "kill_at_vortex", Label = "Psi Storm Fate",
                Content = new()
                {
                    new() { Value = "kill_at_vortex", DisplayText = "Kill at Vortex" },
                    new() { Value = "kill_at_peak", DisplayText = "Kill at Peak" },
                    new() { Value = "damage_only", DisplayText = "Damage Only" },
                }},
            new() { Id = "psi_storm_warning", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "siren", Label = "Psi Storm Warning",
                Content = new()
                {
                    new() { Value = "siren", DisplayText = "Siren" },
                    new() { Value = "radio", DisplayText = "Radio" },
                    new() { Value = "siren_radio", DisplayText = "Siren + Radio" },
                    new() { Value = "none", DisplayText = "None" },
                }},
        }
    };

    private static OptionGroup DynamicNewsGroup() => new()
    {
        Id = "dynamic_news",
        Label = "Dynamic News",
        Options = new()
        {
            new() { Id = "message_duration", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "10", Min = 1, Max = 60, Step = 1, Precision = 0,
                Label = "Message Duration (s)", Description = "How long news messages display" },
            new() { Id = "cycle_of_random_news", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "240", Min = 30, Max = 1200, Step = 30, Precision = 0,
                Label = "Random News Cycle (s)" },
            new() { Id = "cycle_of_special_news", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "240", Min = 30, Max = 1200, Step = 30, Precision = 0,
                Label = "Special News Cycle (s)" },
            new() { Id = "cycle_of_companions_news", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "240", Min = 30, Max = 1200, Step = 30, Precision = 0,
                Label = "Companion News Cycle (s)" },
            new() { Id = "cycle_of_task_news", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "300", Min = 30, Max = 1200, Step = 30, Precision = 0,
                Label = "Task News Cycle (s)" },
            new() { Type = OptionType.Line, Id = "_line1" },
            new() { Id = "bounty_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Bounty News" },
            new() { Id = "death_stalker_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Stalker Death News" },
            new() { Id = "death_mutant_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Mutant Death News" },
            new() { Id = "factions_report_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Faction Reports" },
            new() { Id = "surge_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Surge News" },
            new() { Id = "weather_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Weather News" },
            new() { Id = "time_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Time News" },
            new() { Id = "zone_activity_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Zone Activity News" },
            new() { Id = "nearby_activity_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Nearby Activity News" },
            new() { Id = "companions_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Companion News" },
            new() { Id = "reaction_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Reaction News" },
            new() { Id = "random_msg_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Random Messages" },
            new() { Id = "found_artifact_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Found Artifact News" },
            new() { Id = "found_dead_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Found Dead News" },
            new() { Id = "loot_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Loot News" },
            new() { Id = "heli_call_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Helicopter Call News" },
            new() { Id = "kill_wounded_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Kill Wounded News" },
            new() { Id = "dumb_zombie_news", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Zombie News" },
        }
    };

    private static OptionGroup NeutralZoneGroup() => new()
    {
        Id = "neutral_zone",
        Label = "Neutral Zones",
        Options = new()
        {
            new() { Id = "rostok", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Rostok (Bar)",
                Description = "Rostok/Bar area is a neutral zone" },
            new() { Id = "yanov", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Yanov Station",
                Description = "Yanov Station is a neutral zone" },
            new() { Id = "jupiter_bunker", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Jupiter Bunker" },
            new() { Id = "yantar_bunker", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Yantar Bunker" },
        }
    };

    private static OptionGroup NpcFleeingGroup() => new()
    {
        Id = "npc_fleeing",
        Label = "NPC Fleeing Behavior",
        Options = new()
        {
            new() { Id = "cancel_on_hit", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Cancel on Hit" },
            new() { Id = "hits_to_cancel", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "3", Min = 1, Max = 10, Step = 1, Precision = 0,
                Label = "Hits to Cancel Flee" },
            new() { Id = "threshold_mult", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.25", Min = 0.1, Max = 5.0, Step = 0.05,
                Label = "Flee Threshold Multiplier" },
            new() { Id = "start_flee_time", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "7", Min = 1, Max = 60, Step = 1, Precision = 0,
                Label = "Start Flee Time (s)" },
            new() { Id = "cover_time", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "12", Min = 1, Max = 60, Step = 1, Precision = 0,
                Label = "Cover Time (s)" },
            new() { Id = "flee_protection_time", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "12", Min = 1, Max = 60, Step = 1, Precision = 0,
                Label = "Flee Protection Time (s)" },
            new() { Id = "enemies_radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "125", Min = 10, Max = 500, Step = 5, Precision = 0,
                Label = "Enemies Radius" },
            new() { Id = "friends_radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "70", Min = 10, Max = 500, Step = 5, Precision = 0,
                Label = "Friends Radius" },
            new() { Id = "companion_pwr", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 0, Max = 20, Step = 1, Precision = 0,
                Label = "Companion Power" },
            new() { Id = "smoke_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Smoke Grenades" },
            new() { Id = "flee_smart_min_dist", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "90", Min = 10, Max = 500, Step = 5, Precision = 0,
                Label = "Flee Smart Min Distance" },
        }
    };

    private static OptionGroup NpcWoundedGroup() => new()
    {
        Id = "npc_wounded",
        Label = "NPC Wounded Behavior",
        Options = new()
        {
            new() { Id = "handle_weapon", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "1", Label = "Wounded Weapon Handling",
                Description = "How wounded NPCs handle their weapons",
                Content = new()
                {
                    new() { Value = "0", DisplayText = "Drop Weapon" },
                    new() { Value = "1", DisplayText = "Keep Weapon" },
                    new() { Value = "2", DisplayText = "Stash Weapon" },
                }},
        }
    };

    private static OptionGroup WarfareGeneralGroup() => new()
    {
        Id = "warfare/general",
        Label = "Warfare — General",
        Options = new()
        {
            new() { Id = "fog_of_war", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Fog of War" },
            new() { Id = "fog_of_war_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 10, Max = 300, Step = 10, Precision = 0,
                Label = "Fog of War Distance" },
            new() { Id = "extra_loner_fog_of_war", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Extra Loner Fog of War" },
            new() { Id = "all_out_war", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "All Out War",
                Description = "All factions fight each other" },
            new() { Id = "auto_capture", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Auto Capture",
                Description = "Squads automatically capture smart terrains" },
            new() { Id = "debug_logging", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Debug Logging" },
            new() { Id = "territory_diplo_penalty", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Territory Diplomacy Penalty" },
            new() { Id = "offline_combat_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 10, Max = 300, Step = 10, Precision = 0,
                Label = "Offline Combat Distance" },
            new() { Id = "enable_mutant_offline_combat", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Mutant Offline Combat" },
            new() { Id = "prevent_mainbase_attacks", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Prevent Main Base Attacks" },
            new() { Id = "random_stalker_chance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Random Stalker Chance %" },
            new() { Id = "random_monster_chance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                Label = "Random Monster Chance %" },
            new() { Id = "zombies_act_as_faction", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Zombies Act as Faction" },
        }
    };

    private static OptionGroup WarfareAzazelGroup() => new()
    {
        Id = "warfare/azazel",
        Label = "Warfare — Azazel Mode",
        Options = new()
        {
            new() { Id = "state", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Enable Azazel Mode",
                Description = "Respawn as another stalker on death" },
            new() { Id = "respawn_as_nearest", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Respawn as Nearest" },
            new() { Id = "respawn_as_actor_faction", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Respawn as Own Faction" },
            new() { Id = "respawn_as_companions", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Respawn as Companions" },
            new() { Id = "respawn_as_allies", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Respawn as Allies" },
            new() { Id = "respawn_as_neutrals", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Respawn as Neutrals" },
            new() { Id = "respawn_as_enemies", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Respawn as Enemies" },
        }
    };

    private static OptionGroup BuildFactionWarfareGroup(string factionId)
    {
        var name = FactionNames.GetValueOrDefault(factionId, factionId);
        return new OptionGroup
        {
            Id = $"warfare/{factionId}",
            Label = $"Warfare — {name}",
            Options = new()
            {
                new() { Id = "participate_in_warfare", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                    DefaultValue = "true", Label = "Participate in Warfare" },
                new() { Id = "spawn_on_new_game", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                    DefaultValue = "true", Label = "Spawn on New Game" },
                new() { Id = "expansion_aggression", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                    Label = "Expansion Aggression" },
                new() { Id = "base_priority", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "50", Min = -100, Max = 100, Step = 5, Precision = 0,
                    Label = "Base Priority" },
                new() { Id = "territory_priority", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "0", Min = -100, Max = 100, Step = 5, Precision = 0,
                    Label = "Territory Priority" },
                new() { Id = "resource_priority", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "50", Min = -100, Max = 100, Step = 5, Precision = 0,
                    Label = "Resource Priority" },
                new() { Id = "offline_power_multiplier", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "1.5", Min = 0.1, Max = 5.0, Step = 0.1,
                    Label = "Offline Power Multiplier" },
                new() { Id = "resurgence_chance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "50", Min = 0, Max = 100, Step = 5, Precision = 0,
                    Label = "Resurgence Chance %" },
                new() { Id = "night_activity_chance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "5", Min = 0, Max = 100, Step = 1, Precision = 0,
                    Label = "Night Activity Chance %" },
                new() { Id = "keep_last_base", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                    DefaultValue = "false", Label = "Keep Last Base" },
                new() { Id = "linked_level_targeting", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                    DefaultValue = "true", Label = "Linked Level Targeting" },
                new() { Id = "random_patrols", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                    DefaultValue = "false", Label = "Random Patrols" },
                new() { Id = "max_invasion_size", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "1", Min = 0.1, Max = 5.0, Step = 0.1,
                    Label = "Max Invasion Size" },
                new() { Id = "min_invasion_size", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "0.5", Min = 0.1, Max = 5.0, Step = 0.1,
                    Label = "Min Invasion Size" },
                new() { Id = "max_smart_targets_per_base", Type = OptionType.Track, ValueType = OptionValueType.Float,
                    DefaultValue = "2", Min = 0, Max = 10, Step = 1, Precision = 0,
                    Label = "Max Smart Targets Per Base" },
            }
        };
    }
}
