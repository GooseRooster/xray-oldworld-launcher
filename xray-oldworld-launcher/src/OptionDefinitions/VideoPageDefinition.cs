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
            AdvancedMainGroup(),
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
                Description = "Display mode (uses functor, not direct console command)",
                Content = new()
                {
                    new() { Value = "1", DisplayText = "Fullscreen" },
                    new() { Value = "2", DisplayText = "Borderless Windowed" },
                    new() { Value = "3", DisplayText = "Windowed" },
                }},
            new() { Id = "lighting_style", ConsoleCommand = "r4_lighting_style", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_dynamic", Label = "Lighting Style",
                Content = new()
                {
                    new() { Value = "st_opt_dynamic", DisplayText = "Dynamic Lighting" },
                    new() { Value = "st_opt_static", DisplayText = "Static Lighting" },
                }},
            new() { Id = "static_lighting_quality", ConsoleCommand = "r4_static_lighting_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium", Label = "Static Lighting Quality",
                Description = "Quality tier for static lightmaps (only applies with static lighting)",
                Content = new()
                {
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                }},
            new() { Id = "hdr_enable", ConsoleCommand = "r4_hdr10_on", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true, Label = "HDR",
                Description = "Enable HDR display output" },
        }
    };

    private static OptionGroup AdvancedMainGroup() => new()
    {
        Id = "advanced/main",
        Label = "Advanced Rendering",
        Options = new()
        {
            // -- General --
            new() { Id = "ai_torch", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                ConsoleCommand = "ai_use_torch_dynamic_lights", DefaultValue = "true",
                Label = "AI Torch", Description = "Dynamic flashlight for NPCs" },
            new() { Id = "v_sync", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                ConsoleCommand = "rs_v_sync", DefaultValue = "false",
                Label = "V-Sync", Description = "Vertical synchronization" },
            new() { Id = "framelimit", Type = OptionType.Track, ValueType = OptionValueType.Float,
                ConsoleCommand = "r__framelimit", DefaultValue = "0", Min = 0, Max = 500, Step = 2, Precision = 0,
                Label = "Frame Limit", Description = "Maximum framerate (0 = unlimited)" },

            // -- Rendering Distance --
            new() { Type = OptionType.Title, Id = "_rendering_dist", Label = "Rendering Distance" },
            new() { Id = "vis_distance", ConsoleCommand = "rs_vis_distance", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.4, Max = 1.5, Step = 0.1,
                Label = "View Distance" },
            new() { Id = "optimize_static_geom", ConsoleCommand = "r__optimize_static_geom", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 4, Step = 1, Precision = 0,
                Label = "Static Geometry Optimization", Description = "Higher = more aggressive culling" },
            new() { Id = "optimize_dynamic_geom", ConsoleCommand = "r__optimize_dynamic_geom", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "2", Min = 0, Max = 4, Step = 1, Precision = 0,
                Label = "Dynamic Geometry Optimization", Description = "Higher = more aggressive culling" },
            new() { Id = "optimize_shadow_geom", ConsoleCommand = "r__optimize_shadow_geom", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Shadow Geometry Optimization" },

            // -- Rendering Quality --
            new() { Type = OptionType.Title, Id = "_rendering_quality", Label = "Rendering Quality" },
            new() { Id = "texture_lod", ConsoleCommand = "texture_lod", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0", Min = 0, Max = 4, Step = 1, Precision = 0,
                Label = "Texture LOD", Description = "Lower = higher quality textures" },
            new() { Id = "geometry_lod", ConsoleCommand = "r__geometry_lod", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.1, Max = 1.5, Step = 0.1,
                Label = "Geometry LOD" },
            new() { Id = "mipbias", ConsoleCommand = "r__tf_mipbias", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.0", Min = -0.5, Max = 0.5, Step = 0.1,
                Label = "Texture Mip Bias", Description = "Negative = sharper textures" },
            new() { Id = "tf_aniso", ConsoleCommand = "r__tf_aniso", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "16", Label = "Anisotropic Filtering",
                Content = new()
                {
                    new() { Value = "1", DisplayText = "Off" },
                    new() { Value = "4", DisplayText = "x4" },
                    new() { Value = "8", DisplayText = "x8" },
                    new() { Value = "16", DisplayText = "x16" },
                }},
            new() { Id = "ssample_list", ConsoleCommand = "r3_msaa", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_off", Label = "MSAA",
                Content = new()
                {
                    new() { Value = "st_opt_off", DisplayText = "Off" },
                    new() { Value = "2x", DisplayText = "2x" },
                    new() { Value = "4x", DisplayText = "4x" },
                    new() { Value = "8x", DisplayText = "8x" },
                }},
            new() { Id = "smaa", ConsoleCommand = "r2_smaa", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "high", Label = "SMAA Anti-Aliasing",
                Content = new()
                {
                    new() { Value = "off", DisplayText = "Off" },
                    new() { Value = "low", DisplayText = "Low" },
                    new() { Value = "medium", DisplayText = "Medium" },
                    new() { Value = "high", DisplayText = "High" },
                    new() { Value = "ultra", DisplayText = "Ultra" },
                }},
            new() { Id = "ssfx_taa", ConsoleCommand = "r3_ssfx_taa", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "TAA", Description = "Temporal Anti-Aliasing" },
            new() { Id = "detail_textures", ConsoleCommand = "r1_detail_textures", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Detail Textures" },
            new() { Id = "detail_bump", ConsoleCommand = "r2_detail_bump", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Detail Bump Mapping" },
            new() { Id = "steep_parallax", ConsoleCommand = "r2_steep_parallax", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Steep Parallax" },
            new() { Id = "enable_tessellation", ConsoleCommand = "r4_enable_tessellation", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Tessellation" },
            new() { Id = "material_style", ConsoleCommand = "r4_material_style", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_classic", Label = "Material Style",
                Description = "Classic or PBR material rendering (dynamic lighting only)",
                Content = new()
                {
                    new() { Value = "st_opt_classic", DisplayText = "Classic" },
                    new() { Value = "st_opt_pbr", DisplayText = "PBR" },
                }},

            // -- Grass --
            new() { Type = OptionType.Title, Id = "_grass", Label = "Grass" },
            new() { Id = "detail_density", ConsoleCommand = "r__detail_density", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.6", Min = 0.04, Max = 1.0, Step = 0.02,
                Label = "Grass Density" },
            new() { Id = "detail_radius", ConsoleCommand = "r__detail_radius", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "150", Min = 50, Max = 250, Step = 20, Precision = 0,
                Label = "Grass Render Distance" },
            new() { Id = "detail_height", ConsoleCommand = "r__detail_height", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.1,
                Label = "Grass Height" },

            // -- Lighting --
            new() { Type = OptionType.Title, Id = "_lighting", Label = "Lighting" },
            new() { Id = "slight_fade", ConsoleCommand = "r2_slight_fade", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.5", Min = 0.2, Max = 1.0, Step = 0.1,
                Label = "Light Fade Distance" },
            new() { Id = "ls_squality", ConsoleCommand = "r2_ls_squality", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 1.0, Step = 0.5,
                Label = "Light Source Quality" },
            new() { Id = "actor_shadow", ConsoleCommand = "r__actor_shadow", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Player Shadow" },
            new() { Id = "gloss_factor", ConsoleCommand = "r2_gloss_factor", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1", Min = 0, Max = 10, Step = 0.5,
                Label = "Gloss Factor" },
            new() { Id = "sun", ConsoleCommand = "r2_sun", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Sun Shadows" },
            new() { Id = "sun_quality", ConsoleCommand = "r2_sun_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium", Label = "Sun Shadow Quality",
                Content = new()
                {
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                    new() { Value = "st_opt_ultra", DisplayText = "Ultra" },
                    new() { Value = "st_opt_extreme", DisplayText = "Extreme" },
                }},
            new() { Id = "sunshafts_mode", ConsoleCommand = "r2_sunshafts_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "volumetric", Label = "Sunshafts Mode",
                Content = new()
                {
                    new() { Value = "off", DisplayText = "Off" },
                    new() { Value = "volumetric", DisplayText = "Volumetric" },
                    new() { Value = "screen_space", DisplayText = "Screen Space" },
                    new() { Value = "combined", DisplayText = "Combined" },
                }},
            new() { Id = "sunshafts_quality", ConsoleCommand = "r2_sunshafts_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_high", Label = "Sunshafts Quality",
                Content = new()
                {
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                }},
            new() { Id = "sunshafts_value", ConsoleCommand = "r2_sunshafts_value", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "1.0", Min = 0.5, Max = 2.0, Step = 0.1,
                Label = "Sunshafts Intensity" },
            new() { Id = "sunshafts_min", ConsoleCommand = "r2_sunshafts_min", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.15", Min = 0, Max = 0.5, Step = 0.05,
                Label = "Sunshafts Min Threshold" },
            new() { Id = "ssao_mode", ConsoleCommand = "r2_ssao_mode", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "ssdo", Label = "SSAO Mode",
                Content = new()
                {
                    new() { Value = "gtao", DisplayText = "XeGTAO" },
                    new() { Value = "ssdo", DisplayText = "SSDO" },
                }},
            new() { Id = "ssao", ConsoleCommand = "r2_ssao", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_opt_medium", Label = "SSAO Quality",
                Content = new()
                {
                    new() { Value = "st_opt_off", DisplayText = "Off" },
                    new() { Value = "st_opt_low", DisplayText = "Low" },
                    new() { Value = "st_opt_medium", DisplayText = "Medium" },
                    new() { Value = "st_opt_high", DisplayText = "High" },
                }},
            new() { Id = "volumetric_lights", ConsoleCommand = "r2_volumetric_lights", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Volumetric Lights" },
            new() { Id = "point_light_shadows", ConsoleCommand = "r4_point_light_shadows", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "Point Light Shadows", Description = "Dynamic lighting only" },

            // -- Effects --
            new() { Type = OptionType.Title, Id = "_effects", Label = "Effects" },
            new() { Id = "soft_particles", ConsoleCommand = "r2_soft_particles", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Soft Particles" },
            new() { Id = "dof_enable", ConsoleCommand = "r2_dof_enable", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Depth of Field" },
            new() { Id = "mblur_enable", ConsoleCommand = "r2_mblur_enabled", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false",
                Label = "Motion Blur" },
            new() { Id = "mblur", ConsoleCommand = "r2_mblur", Type = OptionType.Track, ValueType = OptionValueType.Float,
                DefaultValue = "0.4", Min = 0, Max = 1.0, Step = 0.05,
                Label = "Motion Blur Amount" },
            new() { Id = "soft_water", ConsoleCommand = "r2_soft_water", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Soft Water" },
            new() { Id = "ssfx_water", ConsoleCommand = "r3_ssfx_water", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "SSR Water" },
            new() { Id = "dynamic_wet_surfaces", ConsoleCommand = "r3_dynamic_wet_surfaces", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Dynamic Wet Surfaces" },
            new() { Id = "volumetric_smoke", ConsoleCommand = "r3_volumetric_smoke", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true",
                Label = "Volumetric Smoke" },
            new() { Id = "ssfx_fog", ConsoleCommand = "r3_ssfx_fog", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "Volumetric Fog" },
            new() { Id = "ssfx_shadows", ConsoleCommand = "r3_ssfx_shadows", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "Screenspace Shadows" },
            new() { Id = "ssfx_gi", ConsoleCommand = "r3_gi", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "false", BoolToNum = true,
                Label = "Global Illumination" },
            new() { Id = "hires_rts", ConsoleCommand = "r4_hires_rts", Type = OptionType.Check, ValueType = OptionValueType.Bool,
                DefaultValue = "true", BoolToNum = true,
                Label = "Hi-Res Render Targets" },
            new() { Id = "terrain_quality", ConsoleCommand = "r3_terrain_quality", Type = OptionType.List, ValueType = OptionValueType.String,
                DefaultValue = "st_terrain_low", Label = "Terrain Quality",
                Content = new()
                {
                    new() { Value = "st_terrain_low", DisplayText = "Low" },
                    new() { Value = "st_terrain_mid", DisplayText = "Medium" },
                    new() { Value = "st_terrain_high", DisplayText = "High" },
                }},
        }
    };
}
