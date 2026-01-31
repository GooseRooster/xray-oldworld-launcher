using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.OptionDefinitions;

public static class VideoPageDefinition
{
    public static OptionPage Build() => new()
    {
        Id = "video",
        Groups = new()
        {
            BasicGroup(),
            AdvancedMainGroup(),
        }
    };

    private static OptionGroup BasicGroup() => new()
    {
        Id = "basic",
        Options = new()
        {
            new() { Id = "fov", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "fov", DefaultValue = "75", Min = 5, Max = 140, Step = 1, Precision = 0 },
            new() { Id = "hud_fov", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "hud_fov", DefaultValue = "0.45", Min = 0.1, Max = 1.0, Step = 0.01 },
            new() { Id = "screen_mode", ConsoleCommand="rs_screenmode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "borderless",
                Content = new() { "fullscreen", "borderless", "windowed" } },
            new() { Id = "lighting_style", ConsoleCommand = "r4_lighting_style", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_dynamic",
                Content = new() { "st_opt_dynamic", "st_opt_static" } },
            new() { Id = "static_lighting_quality", ConsoleCommand = "r4_static_lighting_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium",
                Content = new() { "st_opt_low", "st_opt_medium", "st_opt_high" } },
            new() { Id = "hdr_enable", ConsoleCommand = "r4_hdr10_on", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
        }
    };

    private static OptionGroup AdvancedMainGroup() => new()
    {
        Id = "advanced/main",
        Options = new()
        {
            // -- General --
            new() { Id = "ai_torch", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                ConsoleCommand = "ai_use_torch_dynamic_lights", DefaultValue = "true" },
            new() { Id = "v_sync", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                ConsoleCommand = "rs_v_sync", DefaultValue = "false" },
            new() { Id = "framelimit", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "r__framelimit", DefaultValue = "0", Min = 0, Max = 500, Step = 2, Precision = 0 },

            // -- Rendering Distance --
            new() { Type = OptionType.Title, Id = "_rendering_dist" },
            new() { Id = "vis_distance", ConsoleCommand = "rs_vis_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.4, Max = 1.5, Step = 0.1 },
            new() { Id = "optimize_static_geom", ConsoleCommand = "r__optimize_static_geom", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 4, Step = 1, Precision = 0 },
            new() { Id = "optimize_dynamic_geom", ConsoleCommand = "r__optimize_dynamic_geom", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 4, Step = 1, Precision = 0 },
            new() { Id = "optimize_shadow_geom", ConsoleCommand = "r__optimize_shadow_geom", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },

            // -- Rendering Quality --
            new() { Type = OptionType.Title, Id = "_rendering_quality" },
            new() { Id = "texture_lod", ConsoleCommand = "texture_lod", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0", Min = 0, Max = 4, Step = 1, Precision = 0 },
            new() { Id = "geometry_lod", ConsoleCommand = "r__geometry_lod", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.1, Max = 1.5, Step = 0.1 },
            new() { Id = "mipbias", ConsoleCommand = "r__tf_mipbias", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.0", Min = -0.5, Max = 0.5, Step = 0.1 },
            new() { Id = "tf_aniso", ConsoleCommand = "r__tf_aniso", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "16",
                Content = new() { "1", "4", "8", "16" } },
            new() { Id = "ssample_list", ConsoleCommand = "r3_msaa", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_off",
                Content = new() { "st_opt_off", "2x", "4x", "8x" } },
            new() { Id = "smaa", ConsoleCommand = "r2_smaa", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "high",
                Content = new() { "off", "low", "medium", "high", "ultra" } },
            new() { Id = "ssfx_taa", ConsoleCommand = "r3_ssfx_taa", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
            new() { Id = "detail_textures", ConsoleCommand = "r1_detail_textures", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "detail_bump", ConsoleCommand = "r2_detail_bump", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "steep_parallax", ConsoleCommand = "r2_steep_parallax", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "enable_tessellation", ConsoleCommand = "r4_enable_tessellation", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "material_style", ConsoleCommand = "r4_material_style", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_classic",
                Content = new() { "st_opt_classic", "st_opt_pbr" } },

            // -- Grass --
            new() { Type = OptionType.Title, Id = "_grass" },
            new() { Id = "detail_density", ConsoleCommand = "r__detail_density", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.6", Min = 0.04, Max = 1.0, Step = 0.02 },
            new() { Id = "detail_radius", ConsoleCommand = "r__detail_radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "150", Min = 50, Max = 250, Step = 20, Precision = 0 },
            new() { Id = "detail_height", ConsoleCommand = "r__detail_height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.1 },

            // -- Lighting --
            new() { Type = OptionType.Title, Id = "_lighting" },
            new() { Id = "slight_fade", ConsoleCommand = "r2_slight_fade", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0.2, Max = 1.0, Step = 0.1 },
            new() { Id = "ls_squality", ConsoleCommand = "r2_ls_squality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 1.0, Step = 0.5 },
            new() { Id = "actor_shadow", ConsoleCommand = "r__actor_shadow", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "gloss_factor", ConsoleCommand = "r2_gloss_factor", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 10, Step = 0.5 },
            new() { Id = "sun", ConsoleCommand = "r2_sun", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "sun_quality", ConsoleCommand = "r2_sun_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium",
                Content = new() { "st_opt_low", "st_opt_medium", "st_opt_high", "st_opt_ultra", "st_opt_extreme" } },
            new() { Id = "sunshafts_mode", ConsoleCommand = "r2_sunshafts_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "volumetric",
                Content = new() { "off", "volumetric", "screen_space", "combined" } },
            new() { Id = "sunshafts_quality", ConsoleCommand = "r2_sunshafts_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_high",
                Content = new() { "st_opt_low", "st_opt_medium", "st_opt_high" } },
            new() { Id = "sunshafts_value", ConsoleCommand = "r2_sunshafts_value", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.1 },
            new() { Id = "sunshafts_min", ConsoleCommand = "r2_sunshafts_min", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.15", Min = 0, Max = 0.5, Step = 0.05 },
            new() { Id = "ssao_mode", ConsoleCommand = "r2_ssao_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "ssdo",
                Content = new() { "gtao", "ssdo" } },
            new() { Id = "ssao", ConsoleCommand = "r2_ssao", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium",
                Content = new() { "st_opt_off", "st_opt_low", "st_opt_medium", "st_opt_high" } },
            new() { Id = "volumetric_lights", ConsoleCommand = "r2_volumetric_lights", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "point_light_shadows", ConsoleCommand = "r4_point_light_shadows", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },

            // -- Effects --
            new() { Type = OptionType.Title, Id = "_effects" },
            new() { Id = "soft_particles", ConsoleCommand = "r2_soft_particles", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "dof_enable", ConsoleCommand = "r2_dof_enable", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "mblur_enable", ConsoleCommand = "r2_mblur_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false" },
            new() { Id = "mblur", ConsoleCommand = "r2_mblur", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.4", Min = 0, Max = 1.0, Step = 0.05 },
            new() { Id = "soft_water", ConsoleCommand = "r2_soft_water", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "ssfx_water", ConsoleCommand = "r3_ssfx_water", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
            new() { Id = "dynamic_wet_surfaces", ConsoleCommand = "r3_dynamic_wet_surfaces", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "volumetric_smoke", ConsoleCommand = "r3_volumetric_smoke", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true" },
            new() { Id = "ssfx_fog", ConsoleCommand = "r3_ssfx_fog", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
            new() { Id = "ssfx_shadows", ConsoleCommand = "r3_ssfx_shadows", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
            new() { Id = "ssfx_gi", ConsoleCommand = "r3_gi", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true },
            new() { Id = "hires_rts", ConsoleCommand = "r4_hires_rts", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", BoolToNum = true },
            new() { Id = "terrain_quality", ConsoleCommand = "r3_terrain_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_terrain_low",
                Content = new() { "st_terrain_low", "st_terrain_mid", "st_terrain_high" } },
        }
    };
}
