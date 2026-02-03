using TauriApi;
using XrayOldworldLauncher.Models.Options;
using XrayOldworldLauncher.OptionDefinitions;
using MudBlazor;

namespace XrayOldworldLauncher.Services;

/// <summary>
/// Manages option state: current values, pending changes, defaults.
/// Uses TauriApi to communicate with the Rust backend.
/// </summary>
public class OptionsStateService
{
    private readonly Tauri _tauri;

    /// All known option definitions that have a console command, with page/group context.
    private readonly List<(OptionDefinition Opt, string PageId, string GroupId)> _allCmdOptions;

    /// Current values from user.ltx (console commands)
    public Dictionary<string, string> CmdValues { get; private set; } = new();


    /// Pending unsaved changes keyed by full option path
    public Dictionary<string, OptionChange> PendingChanges { get; } = new();

    public bool HasPendingChanges => PendingChanges.Count > 0;

    public event Action? OnStateChanged;

    public OptionsStateService(Tauri tauri)
    {
        _tauri = tauri;
        _allCmdOptions = BuildAllCmdOptions();
    }

    /// Collect every option definition with a console command from all page definitions.
    private static List<(OptionDefinition Opt, string PageId, string GroupId)> BuildAllCmdOptions()
    {
        var pages = new[]
        {
            VideoPageDefinition.Build(),
            SoundPageDefinition.Build(),
            ControlPageDefinition.Build(),
        };

        var result = new List<(OptionDefinition, string, string)>();
        foreach (var page in pages)
        {
            foreach (var group in page.Groups)
            {
                foreach (var opt in group.Options)
                {
                    if (opt.ConsoleCommand != null && opt.Type is not (OptionType.Line or OptionType.Title))
                        result.Add((opt, page.Id, group.Id));
                }
            }
        }
        return result;
    }


    /// Reload current values from config files.
    public async Task RefreshCurrentValuesAsync()
    {
        try
        {
            var state = await _tauri.Core.Invoke<OptionsState>("get_options");
            if (state != null)
            {
                CmdValues = state.UserLtx;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to refresh options: {ex.Message}");
        }
    }

    /// Get the current display value for an option.
    /// Priority: PendingChanges → CmdValues/AxrValues → Defaults → hardcoded default.
    public string GetCurrentValue(OptionDefinition opt, string pageId, string groupId)
    {
        var fullPath = $"{pageId}/{groupId}/{opt.Id}";

        // 1. Check pending changes
        if (PendingChanges.TryGetValue(fullPath, out var pending))
            return pending.Value;

        // 2. Check current file values
        if (opt.ConsoleCommand != null)
        {
            if (CmdValues.TryGetValue(opt.ConsoleCommand, out var cmdVal))
                return NormalizeBoolValue(cmdVal, opt);
        }
        
        // 3. Hardcoded default from C# definition
        return opt.DefaultValue;
    }

    /// Record a pending change for an option.
    public void SetPendingChange(OptionDefinition opt, string pageId, string groupId, string value)
    {
        var fullPath = $"{pageId}/{groupId}/{opt.Id}";

        OptionChange change = new();
        if (opt.ConsoleCommand != null)
        {
            var formattedValue = FormatValueForUserLtx(value, opt);
            change = OptionChange.ForUserLtx(opt.ConsoleCommand, formattedValue);
        }
      

        PendingChanges[fullPath] = change;
        OnStateChanged?.Invoke();
    }

    /// Save all pending changes to the backend.
    /// Also writes default values for any options missing from user.ltx,
    /// ensuring deleted/missing config entries are restored.
    public async Task SaveAllAsync()
    {
        if (!HasPendingChanges) return;

        // Console commands the user is explicitly changing
        var pendingCmds = new HashSet<string>(
            PendingChanges.Values
                .Where(c => c.Cmd != null)
                .Select(c => c.Cmd!));

        // Fill in defaults for any options missing from user.ltx
        var changes = new List<OptionChange>();
        foreach (var (opt, _, _) in _allCmdOptions)
        {
            if (!CmdValues.ContainsKey(opt.ConsoleCommand!) && !pendingCmds.Contains(opt.ConsoleCommand!))
            {
                var defaultValue = FormatValueForUserLtx(opt.DefaultValue, opt);
                changes.Add(OptionChange.ForUserLtx(opt.ConsoleCommand!, defaultValue));
            }
        }

        // User's pending changes come after so they override any matching defaults
        changes.AddRange(PendingChanges.Values);

        await _tauri.Core.Invoke("save_options", new { changes });

        PendingChanges.Clear();
        await RefreshCurrentValuesAsync();
        OnStateChanged?.Invoke();
    }

    /// Reset all options on a page to their default values.
    public async Task ResetToDefaultsAsync(OptionPage page)
    {
        var resetOptions = new List<object>();

        foreach (var group in page.Groups)
        {
            foreach (var opt in group.Options)
            {
                if (opt.Type is OptionType.Line or OptionType.Title)
                    continue;

                var fullPath = $"{page.Id}/{group.Id}/{opt.Id}";
                var defaultValue = GetDefaultValue(opt, page.Id, group.Id);

                if (opt.ConsoleCommand != null)
                {
                    var formattedDefault = FormatValueForUserLtx(defaultValue, opt);
                    resetOptions.Add(new
                    {
                        path = fullPath,
                        defaultValue = formattedDefault,
                        storageType = "userLtx",
                        cmd = opt.ConsoleCommand
                    });
                }
                else
                {
                    resetOptions.Add(new
                    {
                        path = fullPath,
                        defaultValue,
                        storageType = "axrOptions"
                    });
                }
            }
        }

        await _tauri.Core.Invoke("reset_options_to_defaults", new { options = resetOptions });

        // Clear any pending changes for this page
        var pagePrefix = $"{page.Id}/";
        var keysToRemove = PendingChanges.Keys.Where(k => k.StartsWith(pagePrefix)).ToList();
        foreach (var key in keysToRemove)
            PendingChanges.Remove(key);

        await RefreshCurrentValuesAsync();
        OnStateChanged?.Invoke();
    }

    /// Discard all pending changes.
    public void DiscardPendingChanges()
    {
        PendingChanges.Clear();
        OnStateChanged?.Invoke();
    }

    private string GetDefaultValue(OptionDefinition opt, string pageId, string groupId)
    {

        return opt.DefaultValue;
    }

    /// Format a value for writing to user.ltx.
    /// Handles bool-to-num conversion (1/0 vs on/off).
    private static string FormatValueForUserLtx(string value, OptionDefinition opt)
    {
        if (opt.ValueType != OptionValueType.Bool) return value;

        var isTruthy = value.Equals("true", StringComparison.OrdinalIgnoreCase)
                       || value == "1" || value.Equals("on", StringComparison.OrdinalIgnoreCase);

        return opt.BoolToNum
            ? (isTruthy ? "1" : "0")
            : (isTruthy ? "on" : "off");
    }

    /// Normalize bool values from user.ltx (which might be "on"/"off" or "1"/"0")
    /// to "true"/"false" for consistent frontend display.
    private static string NormalizeBoolValue(string value, OptionDefinition opt)
    {
        if (opt.ValueType != OptionValueType.Bool) return value;

        return value switch
        {
            "on" or "1" or "true" or "yes" => "true",
            "off" or "0" or "false" or "no" => "false",
            _ => value
        };
    }

    /// <summary>
    /// Gets a merged view of CmdValues with pending changes applied.
    /// Used for visibility condition checks to reflect unsaved changes.
    /// </summary>
    public Dictionary<string, string> GetEffectiveValues()
    {
        var result = new Dictionary<string, string>(CmdValues);

        // Overlay pending changes
        foreach (var (_, change) in PendingChanges)
        {
            if (change.Cmd != null)
            {
                result[change.Cmd] = change.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// Gets the current lighting style value (checks pending changes first).
    /// </summary>
    public string GetLightingStyle()
    {
        if (PendingChanges.TryGetValue("video/basic/lighting_style", out var pending))
            return pending.Value;
        if (CmdValues.TryGetValue("r4_lighting_style", out var val))
            return val;
        return "st_opt_dynamic";
    }

    /// <summary>
    /// Applies a video preset by setting pending changes for all preset options.
    /// </summary>
    /// <param name="presetName">Name of the preset (low, medium, high, ultra)</param>
    /// <param name="lightingStyle">Current lighting style value</param>
    public void ApplyPreset(string presetName, string lightingStyle)
    {
        var presetValues = lightingStyle == "st_opt_static"
            ? VideoPresets.GetStaticPreset(presetName)
            : VideoPresets.GetDynamicPreset(presetName);

        foreach (var (cmd, value) in presetValues)
        {
            // Find the option definition by console command
            var match = _allCmdOptions.FirstOrDefault(x => x.Opt.ConsoleCommand == cmd);
            if (match.Opt != null)
            {
                SetPendingChange(match.Opt, match.PageId, match.GroupId, value);
            }
        }
    }
}
