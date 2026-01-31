using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class VideoPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "video",
        Label = "Video",
        Groups = new()
        {
            BasicGroup(),
            HudGroup(),
            PlayerGroup(),
            NightGroup(),
            AdvancedMainGroup(),
            SsfxShadowsGroup(),
            SsfxWaterGroup(),
            SsfxFogGroup(),
            SsfxGrassGroup(),
            SsfxWindGroup(),
            SsfxIlGroup(),
            SsfxTaaGroup(),
            SsfxCascadesGroup(),
            SsfxParallaxGroup(),
            SsfxTerrainGroup(),
            SsfxSssGroup(),
            SsfxFloraGroup(),
        }
    };

    private static OptionGroup BasicGroup() => new()
    {
        Id = "basic",
        Label = "Basic",
        Options = new()
        {
            new() { Id = "fov", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "fov", DefaultValue = "75", Min = 5, Max = 140, Step = 1, Precision = 0,
                Label = "Field of View", Description = "Camera field of view in degrees" },
            new() { Id = "hud_fov", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "hud_fov", DefaultValue = "0.45", Min = 0.1, Max = 1.0, Step = 0.01,
                Label = "HUD FOV", Description = "First-person weapon/hands FOV multiplier" },
            new() { Id = "screen_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "2", Label = "Screen Mode",
                Content = new()
                {
                    new() { Value = "1", DisplayText = "Fullscreen" },
                    new() { Value = "2", DisplayText = "Borderless Windowed" },
                    new() { Value = "3", DisplayText = "Windowed" },
                }},
            new() { Id = "hdr_enable", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "HDR",
                Description = "Enable HDR display output" },
        }
    };

    private static OptionGroup HudGroup() => new()
    {
        Id = "hud",
        Label = "HUD",
        Options = new()
        {
            new() { Id = "show_minimap", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Show Minimap" },
            new() { Id = "show_crosshair", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Show Crosshair" },
            new() { Id = "show_slots", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Show Artifact Slots" },
            new() { Id = "show_enemy_health", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Show Enemy Health" },
            new() { Id = "3d_pda", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "3D PDA",
                Description = "Use 3D in-world PDA instead of overlay" },
            new() { Id = "autohide_stamina_bar", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Auto-hide Stamina Bar" },
            new() { Type = OptionType.Title, Id = "_crosshair_color", Label = "Crosshair Color" },
            new() { Id = "crosshair_clr_r", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "220", Min = 0, Max = 255, Step = 1, Precision = 0,
                Label = "Red" },
            new() { Id = "crosshair_clr_g", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "220", Min = 0, Max = 255, Step = 1, Precision = 0,
                Label = "Green" },
            new() { Id = "crosshair_clr_b", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "220", Min = 0, Max = 255, Step = 1, Precision = 0,
                Label = "Blue" },
            new() { Id = "crosshair_clr_a", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "255", Min = 0, Max = 255, Step = 1, Precision = 0,
                Label = "Alpha" },
        }
    };

    private static OptionGroup PlayerGroup() => new()
    {
        Id = "player",
        Label = "Player Visual Effects",
        Options = new()
        {
            new() { Id = "animations", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Extra Animations" },
            new() { Id = "item_swap_animation", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Item Swap Animation" },
            new() { Id = "mask_hud", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Mask HUD Overlay" },
            new() { Id = "visor_reflection", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Visor Reflection" },
            new() { Id = "rain_droplets", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Rain Droplets" },
            new() { Id = "breathing_fog", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Breathing Fog" },
            new() { Id = "radiation_effect", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Radiation Effect" },
            new() { Id = "bleed_effect", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Bleed Effect" },
            new() { Id = "blood_splash", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Blood Splash" },
            new() { Id = "shoot_effects", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Shoot Effects" },
        }
    };

    private static OptionGroup NightGroup() => new()
    {
        Id = "night",
        Label = "Night & Moon",
        Options = new()
        {
            new() { Id = "brightness", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "slight", Label = "Night Brightness",
                Content = new()
                {
                    new() { Value = "off", DisplayText = "Off (Realistic)" },
                    new() { Value = "slight", DisplayText = "Slight" },
                    new() { Value = "moderate", DisplayText = "Moderate" },
                    new() { Value = "bright", DisplayText = "Bright" },
                }},
            new() { Id = "moon_cycle", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "8", Min = 1, Max = 30, Step = 1, Precision = 0,
                Label = "Moon Cycle (days)", Description = "Length of the moon phase cycle" },
        }
    };

    private static OptionGroup AdvancedMainGroup() => new()
    {
        Id = "advanced/main",
        Label = "Advanced Rendering",
        Options = new()
        {
            new() { Id = "v_sync", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                ConsoleCommand = "rs_v_sync", DefaultValue = "false", BoolToNum = true,
                Label = "V-Sync", Description = "Vertical synchronization" },
            new() { Id = "vis_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "r__detail_density", DefaultValue = "1.5", Min = 0.5, Max = 3.0, Step = 0.1,
                Label = "View Distance" },
            new() { Id = "detail_density", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.38", Min = 0.0, Max = 1.0, Step = 0.02,
                Label = "Grass Density" },
            new() { Id = "detail_radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "210", Min = 50, Max = 500, Step = 10, Precision = 0,
                Label = "Grass Render Distance" },
            new() { Id = "geometry_lod", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.5", Min = 0.5, Max = 3.0, Step = 0.1,
                Label = "Geometry LOD" },
            new() { Id = "mipbias", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "-0.5", Min = -3.0, Max = 0.0, Step = 0.1,
                Label = "Texture Mip Bias", Description = "Negative = sharper textures" },
            new() { Id = "ssao", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_high", Label = "SSAO Quality",
                Content = new()
                {
                    new() { Value = "st_opt_off", DisplayText = "Off" },
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                }},
            new() { Id = "ssao_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "GTAO", Label = "SSAO Mode",
                Content = new()
                {
                    new() { Value = "default", DisplayText = "Default" },
                    new() { Value = "ssdo", DisplayText = "SSDO" },
                    new() { Value = "GTAO", DisplayText = "GTAO" },
                }},
            new() { Id = "sun_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_extreme", Label = "Sun Shadow Quality",
                Content = new()
                {
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                    new() { Value = "st_opt_extreme", DisplayText = "Extreme" },
                }},
            new() { Id = "smaa", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "off", Label = "SMAA Anti-Aliasing",
                Content = new()
                {
                    new() { Value = "off", DisplayText = "Off" },
                    new() { Value = "low", DisplayText = "Low" },
                    new() { Value = "medium", DisplayText = "Medium" },
                    new() { Value = "high", DisplayText = "High" },
                    new() { Value = "ultra", DisplayText = "Ultra" },
                }},
            new() { Type = OptionType.Title, Id = "_ssfx_toggles", Label = "SSFX Feature Toggles" },
            new() { Id = "ssfx_shadows", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "SSFX Shadows" },
            new() { Id = "ssfx_water", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "SSFX Water" },
            new() { Id = "ssfx_fog", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "SSFX Fog" },
            new() { Id = "ssfx_gi", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "SSFX Global Illumination" },
            new() { Id = "ssfx_taa", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "SSFX TAA" },
            new() { Id = "hires_rts", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Hi-Res RT Shadows" },
        }
    };

    private static OptionGroup SsfxShadowsGroup() => new()
    {
        Id = "ssfx_shadows",
        Label = "SSFX Shadows",
        Options = new()
        {
            new() { Id = "lod_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 1, Precision = 0, Label = "LOD Quality" },
            new() { Id = "lod_min", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "256", Min = 64, Max = 1536, Step = 64, Precision = 0, Label = "LOD Min Resolution" },
            new() { Id = "lod_max", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1536", Min = 256, Max = 2048, Step = 64, Precision = 0, Label = "LOD Max Resolution" },
            new() { Id = "volumetric_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 0.5, Max = 20, Step = 0.5, Label = "Volumetric Quality" },
            new() { Id = "volumetric_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 2, Step = 0.05, Label = "Volumetric Intensity" },
            new() { Id = "volumetric_force", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Force Volumetric" },
        }
    };

    private static OptionGroup SsfxWaterGroup() => new()
    {
        Id = "ssfx_water",
        Label = "SSFX Water",
        Options = new()
        {
            new() { Id = "ssr_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 4, Step = 1, Precision = 0, Label = "SSR Quality" },
            new() { Id = "ssr_res", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0.25, Max = 2, Step = 0.25, Label = "SSR Resolution" },
            new() { Id = "reflection_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 2, Step = 0.1, Label = "Reflection Intensity" },
            new() { Id = "specular_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 2, Step = 0.1, Label = "Specular Intensity" },
            new() { Id = "ripples_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 2, Step = 0.1, Label = "Ripples Intensity" },
            new() { Id = "distortion", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.6", Min = 0, Max = 2, Step = 0.1, Label = "Distortion" },
            new() { Id = "turbidity", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.3", Min = 0, Max = 1, Step = 0.05, Label = "Turbidity" },
            new() { Id = "blur", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.8", Min = 0, Max = 2, Step = 0.1, Label = "Blur" },
            new() { Id = "caustics_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.3", Min = 0, Max = 2, Step = 0.1, Label = "Caustics Intensity" },
            new() { Id = "softborder", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.3", Min = 0, Max = 2, Step = 0.1, Label = "Soft Border" },
            new() { Id = "parallax_height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.05", Min = 0, Max = 0.2, Step = 0.01, Label = "Parallax Height" },
            new() { Id = "parallax_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 8, Step = 1, Precision = 0, Label = "Parallax Quality" },
        }
    };

    private static OptionGroup SsfxFogGroup() => new()
    {
        Id = "ssfx_fog",
        Label = "SSFX Fog",
        Options = new()
        {
            new() { Id = "density", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 3, Step = 0.1, Label = "Density" },
            new() { Id = "height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "8", Min = 0, Max = 50, Step = 1, Precision = 0, Label = "Height" },
            new() { Id = "scattering", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.2", Min = 0, Max = 1, Step = 0.05, Label = "Scattering" },
            new() { Id = "suncolor", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0", Min = 0, Max = 1, Step = 0.1, Label = "Sun Color Influence" },
        }
    };

    private static OptionGroup SsfxGrassGroup() => new()
    {
        Id = "ssfx_grass",
        Label = "SSFX Grass Interaction",
        Options = new()
        {
            new() { Id = "enable", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Enable Grass Interaction" },
            new() { Id = "enable_player", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Player Interaction" },
            new() { Id = "enable_mutants", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Mutant Interaction" },
            new() { Id = "enable_anomalies", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Anomaly Interaction" },
            new() { Id = "radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0.1, Max = 5, Step = 0.1, Label = "Interaction Radius" },
            new() { Id = "max_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2000", Min = 100, Max = 5000, Step = 100, Precision = 0, Label = "Max Distance" },
            new() { Id = "max_entities", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "8", Min = 1, Max = 32, Step = 1, Precision = 0, Label = "Max Entities" },
            new() { Id = "horizontal_str", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "Horizontal Strength" },
            new() { Id = "vertical_str", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "Vertical Strength" },
            new() { Id = "shooting_str", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.3", Min = 0, Max = 2, Step = 0.1, Label = "Shooting Strength" },
            new() { Id = "shooting_range", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0.5, Max = 10, Step = 0.5, Label = "Shooting Range" },
            new() { Id = "explosions_str", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 5, Step = 0.1, Label = "Explosion Strength" },
            new() { Id = "explosions_speed", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "5", Min = 1, Max = 20, Step = 1, Precision = 0, Label = "Explosion Speed" },
        }
    };

    private static OptionGroup SsfxWindGroup() => new()
    {
        Id = "ssfx_wind",
        Label = "SSFX Wind",
        Options = new()
        {
            new() { Id = "min_speed", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.1", Min = 0, Max = 1, Step = 0.05, Label = "Min Wind Speed" },
            new() { Id = "grass_speed", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "9.5", Min = 1, Max = 30, Step = 0.5, Label = "Grass Wind Speed" },
            new() { Id = "grass_wave", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.4", Min = 0, Max = 2, Step = 0.1, Label = "Grass Wave" },
            new() { Id = "grass_turbulence", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.4", Min = 0, Max = 5, Step = 0.1, Label = "Grass Turbulence" },
            new() { Id = "grass_push", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.5", Min = 0, Max = 5, Step = 0.1, Label = "Grass Push" },
            new() { Id = "trees_speed", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "11", Min = 1, Max = 30, Step = 0.5, Label = "Trees Wind Speed" },
            new() { Id = "trees_bend", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 2, Step = 0.05, Label = "Trees Bend" },
            new() { Id = "trees_trunk", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.15", Min = 0, Max = 1, Step = 0.05, Label = "Trees Trunk Sway" },
        }
    };

    private static OptionGroup SsfxIlGroup() => new()
    {
        Id = "ssfx_il",
        Label = "SSFX Indirect Lighting",
        Options = new()
        {
            new() { Id = "global_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "Global Intensity" },
            new() { Id = "flora_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 3, Step = 0.1, Label = "Flora Intensity" },
            new() { Id = "hud_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "HUD Intensity" },
            new() { Id = "vibrance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.2", Min = 0, Max = 3, Step = 0.1, Label = "Vibrance" },
            new() { Id = "quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "48", Min = 8, Max = 128, Step = 8, Precision = 0, Label = "Quality (Samples)" },
            new() { Id = "distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "300", Min = 50, Max = 1000, Step = 50, Precision = 0, Label = "Distance" },
            new() { Id = "res", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.15", Min = 0.05, Max = 1.0, Step = 0.05, Label = "Resolution Scale" },
            new() { Id = "blur", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "Blur" },
        }
    };

    private static OptionGroup SsfxTaaGroup() => new()
    {
        Id = "ssfx_taa",
        Label = "SSFX TAA",
        Options = new()
        {
            new() { Id = "jitter", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0, Max = 1, Step = 0.05, Label = "Jitter" },
            new() { Id = "sharpness", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.6", Min = 0, Max = 2, Step = 0.05, Label = "Sharpness" },
        }
    };

    private static OptionGroup SsfxCascadesGroup() => new()
    {
        Id = "ssfx_cascades",
        Label = "SSFX Shadow Cascades",
        Options = new()
        {
            new() { Id = "size_1", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "20", Min = 5, Max = 100, Step = 5, Precision = 0, Label = "Cascade 1 Size" },
            new() { Id = "size_2", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "60", Min = 20, Max = 300, Step = 10, Precision = 0, Label = "Cascade 2 Size" },
            new() { Id = "size_3", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "160", Min = 50, Max = 500, Step = 10, Precision = 0, Label = "Cascade 3 Size" },
            new() { Id = "grass_shw_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "35", Min = 5, Max = 100, Step = 5, Precision = 0, Label = "Grass Shadow Distance" },
            new() { Id = "grass_shw_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0", Min = 0, Max = 3, Step = 1, Precision = 0, Label = "Grass Shadow Quality" },
        }
    };

    private static OptionGroup SsfxParallaxGroup() => new()
    {
        Id = "ssfx_parallax",
        Label = "SSFX Parallax",
        Options = new()
        {
            new() { Id = "height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.035", Min = 0, Max = 0.1, Step = 0.005, Precision = 3, Label = "Height" },
            new() { Id = "quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "16", Min = 4, Max = 64, Step = 4, Precision = 0, Label = "Quality (Steps)" },
            new() { Id = "range", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "12", Min = 2, Max = 50, Step = 2, Precision = 0, Label = "Range" },
            new() { Id = "ao", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.4", Min = 0, Max = 1, Step = 0.05, Label = "Ambient Occlusion" },
            new() { Id = "refine", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "Refine (Higher Quality)" },
        }
    };

    private static OptionGroup SsfxTerrainGroup() => new()
    {
        Id = "ssfx_terrain",
        Label = "SSFX Terrain",
        Options = new()
        {
            new() { Id = "distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "8", Min = 1, Max = 30, Step = 1, Precision = 0, Label = "Blend Distance" },
            new() { Id = "pom_height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.04", Min = 0, Max = 0.1, Step = 0.005, Precision = 3, Label = "POM Height" },
            new() { Id = "pom_quality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "12", Min = 4, Max = 64, Step = 4, Precision = 0, Label = "POM Quality" },
            new() { Id = "pom_range", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "20", Min = 5, Max = 100, Step = 5, Precision = 0, Label = "POM Range" },
            new() { Id = "pom_refine", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", Label = "POM Refine" },
            new() { Id = "grass_align", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Grass Slope Align" },
            new() { Id = "grass_slope", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "90", Min = 0, Max = 90, Step = 5, Precision = 0, Label = "Grass Slope Angle" },
            new() { Id = "pom_water_level", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 5, Step = 0.5, Label = "POM Water Level" },
        }
    };

    private static OptionGroup SsfxSssGroup() => new()
    {
        Id = "ssfx_sss",
        Label = "SSFX Subsurface Scattering",
        Options = new()
        {
            new() { Id = "enable_dir", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Enable Directional SSS" },
            new() { Id = "enable_point", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", Label = "Enable Point Light SSS" },
            new() { Id = "quality_dir", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "12", Min = 2, Max = 32, Step = 2, Precision = 0, Label = "Directional Quality" },
            new() { Id = "quality_point", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "4", Min = 2, Max = 16, Step = 2, Precision = 0, Label = "Point Quality" },
            new() { Id = "len_dir", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 5, Step = 0.1, Label = "Directional Length" },
            new() { Id = "len_point", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 5, Step = 0.1, Label = "Point Length" },
        }
    };

    private static OptionGroup SsfxFloraGroup() => new()
    {
        Id = "ssfx_flora",
        Label = "SSFX Flora",
        Options = new()
        {
            new() { Id = "sss_int", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 5, Step = 0.1, Label = "SSS Intensity" },
            new() { Id = "sss_color", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 3, Step = 0.1, Label = "SSS Color" },
        }
    };
}
