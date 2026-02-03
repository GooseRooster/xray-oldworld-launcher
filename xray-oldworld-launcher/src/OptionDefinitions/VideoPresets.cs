namespace XrayOldworldLauncher.OptionDefinitions;

/// <summary>
/// Preset definitions for video settings.
/// Contains separate presets for dynamic and static lighting modes.
/// </summary>
public static class VideoPresets
{
    public static readonly string[] PresetNames = { "low", "medium", "high", "ultra" };

    /// <summary>
    /// Gets preset values for dynamic lighting mode.
    /// </summary>
    public static Dictionary<string, string> GetDynamicPreset(string presetName)
    {
        return presetName.ToLowerInvariant() switch
        {
            "low" => DynamicLow,
            "medium" => DynamicMedium,
            "high" => DynamicHigh,
            "ultra" => DynamicUltra,
            _ => DynamicMedium
        };
    }

    /// <summary>
    /// Gets preset values for static lighting mode.
    /// </summary>
    public static Dictionary<string, string> GetStaticPreset(string presetName)
    {
        return presetName.ToLowerInvariant() switch
        {
            "low" => StaticLow,
            "medium" => StaticMedium,
            "high" => StaticHigh,
            "ultra" => StaticUltra,
            _ => StaticMedium
        };
    }

    // ========== Dynamic Lighting Presets ==========

    private static readonly Dictionary<string, string> DynamicLow = new()
    {
        // Textures & Quality
        ["texture_lod"] = "2",
        ["r__tf_mipbias"] = "0.0",
        ["r__tf_aniso"] = "4",

        // Anti-aliasing
        ["r3_msaa"] = "st_opt_off",
        ["r2_smaa"] = "low",
        ["r3_ssfx_taa"] = "false",

        // Grass (higher value = less dense)
        ["r__detail_density"] = "0.9",
        ["r__detail_radius"] = "70",

        // Lighting & Shadows
        ["r2_slight_fade"] = "0.5",
        ["r2_ls_squality"] = "0.5",
        ["r__actor_shadow"] = "false",
        ["r2_sun_quality"] = "st_opt_low",
        ["r2_ssao"] = "st_opt_off",
        ["r2_ssao_mode"] = "ssdo",
        ["r2_sunshafts_mode"] = "off",
        ["r2_volumetric_lights"] = "false",
        ["r4_point_light_shadows"] = "false",
        ["r3_ssfx_shadows"] = "false",

        // Materials & Effects
        ["r4_material_style"] = "st_opt_classic",
        ["r2_detail_bump"] = "false",
        ["r2_steep_parallax"] = "false",
        ["r4_enable_tessellation"] = "false",

        // Post-processing
        ["r2_soft_particles"] = "false",
        ["r2_dof_enable"] = "false",

        // Water & Environment
        ["r2_soft_water"] = "false",
        ["r3_ssfx_water"] = "false",
        ["r3_dynamic_wet_surfaces"] = "false",
        ["r3_ssfx_fog"] = "false",
        ["r3_volumetric_smoke"] = "false",
        ["r3_gi"] = "false",
    };

    private static readonly Dictionary<string, string> DynamicMedium = new()
    {
        // Textures & Quality
        ["texture_lod"] = "1",
        ["r__tf_mipbias"] = "0.0",
        ["r__tf_aniso"] = "8",

        // Anti-aliasing
        ["r3_msaa"] = "st_opt_off",
        ["r2_smaa"] = "medium",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.6",
        ["r__detail_radius"] = "100",

        // Lighting & Shadows
        ["r2_slight_fade"] = "0.7",
        ["r2_ls_squality"] = "0.5",
        ["r__actor_shadow"] = "true",
        ["r2_sun_quality"] = "st_opt_medium",
        ["r2_ssao"] = "st_opt_low",
        ["r2_ssao_mode"] = "ssdo",
        ["r2_sunshafts_mode"] = "volumetric",
        ["r2_volumetric_lights"] = "true",
        ["r4_point_light_shadows"] = "false",
        ["r3_ssfx_shadows"] = "false",

        // Materials & Effects
        ["r4_material_style"] = "st_opt_classic",
        ["r2_detail_bump"] = "true",
        ["r2_steep_parallax"] = "false",
        ["r4_enable_tessellation"] = "false",

        // Post-processing
        ["r2_soft_particles"] = "true",
        ["r2_dof_enable"] = "false",

        // Water & Environment
        ["r2_soft_water"] = "true",
        ["r3_ssfx_water"] = "false",
        ["r3_dynamic_wet_surfaces"] = "true",
        ["r3_ssfx_fog"] = "false",
        ["r3_volumetric_smoke"] = "true",
        ["r3_gi"] = "false",
    };

    private static readonly Dictionary<string, string> DynamicHigh = new()
    {
        // Textures & Quality
        ["texture_lod"] = "0",
        ["r__tf_mipbias"] = "-0.5",
        ["r__tf_aniso"] = "16",

        // Anti-aliasing
        ["r3_msaa"] = "2x",
        ["r2_smaa"] = "high",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.4",
        ["r__detail_radius"] = "150",

        // Lighting & Shadows
        ["r2_slight_fade"] = "1.0",
        ["r2_ls_squality"] = "1.0",
        ["r__actor_shadow"] = "true",
        ["r2_sun_quality"] = "st_opt_high",
        ["r2_ssao"] = "st_opt_medium",
        ["r2_ssao_mode"] = "gtao",
        ["r2_sunshafts_mode"] = "volumetric",
        ["r2_volumetric_lights"] = "true",
        ["r4_point_light_shadows"] = "true",
        ["r3_ssfx_shadows"] = "true",

        // Materials & Effects
        ["r4_material_style"] = "st_opt_pbr",
        ["r2_detail_bump"] = "true",
        ["r2_steep_parallax"] = "true",
        ["r4_enable_tessellation"] = "true",

        // Post-processing
        ["r2_soft_particles"] = "true",
        ["r2_dof_enable"] = "true",

        // Water & Environment
        ["r2_soft_water"] = "true",
        ["r3_ssfx_water"] = "true",
        ["r3_dynamic_wet_surfaces"] = "true",
        ["r3_ssfx_fog"] = "true",
        ["r3_volumetric_smoke"] = "true",
        ["r3_gi"] = "false",
    };

    private static readonly Dictionary<string, string> DynamicUltra = new()
    {
        // Textures & Quality
        ["texture_lod"] = "0",
        ["r__tf_mipbias"] = "-0.5",
        ["r__tf_aniso"] = "16",

        // Anti-aliasing
        ["r3_msaa"] = "4x",
        ["r2_smaa"] = "ultra",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.2",
        ["r__detail_radius"] = "200",

        // Lighting & Shadows
        ["r2_slight_fade"] = "1.0",
        ["r2_ls_squality"] = "1.0",
        ["r__actor_shadow"] = "true",
        ["r2_sun_quality"] = "st_opt_extreme",
        ["r2_ssao"] = "st_opt_high",
        ["r2_ssao_mode"] = "gtao",
        ["r2_sunshafts_mode"] = "combined",
        ["r2_volumetric_lights"] = "true",
        ["r4_point_light_shadows"] = "true",
        ["r3_ssfx_shadows"] = "true",

        // Materials & Effects
        ["r4_material_style"] = "st_opt_pbr",
        ["r2_detail_bump"] = "true",
        ["r2_steep_parallax"] = "true",
        ["r4_enable_tessellation"] = "true",

        // Post-processing
        ["r2_soft_particles"] = "true",
        ["r2_dof_enable"] = "true",

        // Water & Environment
        ["r2_soft_water"] = "true",
        ["r3_ssfx_water"] = "true",
        ["r3_dynamic_wet_surfaces"] = "true",
        ["r3_ssfx_fog"] = "true",
        ["r3_volumetric_smoke"] = "true",
        ["r3_gi"] = "true",
    };

    // ========== Static Lighting Presets ==========
    // Many options are hidden in static lighting mode, so these are simpler

    private static readonly Dictionary<string, string> StaticLow = new()
    {
        ["r4_static_lighting_quality"] = "st_opt_low",

        // Textures & Quality
        ["texture_lod"] = "2",
        ["r__tf_mipbias"] = "0.0",
        ["r__tf_aniso"] = "4",

        // Anti-aliasing
        ["r3_msaa"] = "st_opt_off",
        ["r2_smaa"] = "low",
        ["r3_ssfx_taa"] = "false",

        // Grass
        ["r__detail_density"] = "0.9",
        ["r__detail_radius"] = "70",

        // Lighting
        ["r2_slight_fade"] = "0.5",
        ["r2_ls_squality"] = "0.5",
        ["r__actor_shadow"] = "false",

        // Environment
        ["r3_volumetric_smoke"] = "false",
    };

    private static readonly Dictionary<string, string> StaticMedium = new()
    {
        ["r4_static_lighting_quality"] = "st_opt_medium",

        // Textures & Quality
        ["texture_lod"] = "1",
        ["r__tf_mipbias"] = "0.0",
        ["r__tf_aniso"] = "8",

        // Anti-aliasing
        ["r3_msaa"] = "st_opt_off",
        ["r2_smaa"] = "medium",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.6",
        ["r__detail_radius"] = "100",

        // Lighting
        ["r2_slight_fade"] = "0.7",
        ["r2_ls_squality"] = "0.5",
        ["r__actor_shadow"] = "true",

        // Environment
        ["r3_volumetric_smoke"] = "true",
    };

    private static readonly Dictionary<string, string> StaticHigh = new()
    {
        ["r4_static_lighting_quality"] = "st_opt_high",

        // Textures & Quality
        ["texture_lod"] = "0",
        ["r__tf_mipbias"] = "-0.5",
        ["r__tf_aniso"] = "16",

        // Anti-aliasing
        ["r3_msaa"] = "2x",
        ["r2_smaa"] = "high",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.4",
        ["r__detail_radius"] = "150",

        // Lighting
        ["r2_slight_fade"] = "1.0",
        ["r2_ls_squality"] = "1.0",
        ["r__actor_shadow"] = "true",

        // Environment
        ["r3_volumetric_smoke"] = "true",
    };

    private static readonly Dictionary<string, string> StaticUltra = new()
    {
        ["r4_static_lighting_quality"] = "st_opt_high", // No ultra for static quality

        // Textures & Quality
        ["texture_lod"] = "0",
        ["r__tf_mipbias"] = "-0.5",
        ["r__tf_aniso"] = "16",

        // Anti-aliasing
        ["r3_msaa"] = "4x",
        ["r2_smaa"] = "ultra",
        ["r3_ssfx_taa"] = "true",

        // Grass
        ["r__detail_density"] = "0.2",
        ["r__detail_radius"] = "200",

        // Lighting
        ["r2_slight_fade"] = "1.0",
        ["r2_ls_squality"] = "1.0",
        ["r__actor_shadow"] = "true",

        // Environment
        ["r3_volumetric_smoke"] = "true",
    };
}
