using TauriApi;
using XrayOldworldLauncher.Models.Options;

namespace XrayOldworldLauncher.Services;

/// <summary>
/// Manages option state: current values, pending changes, defaults.
/// Uses TauriApi to communicate with the Rust backend.
/// </summary>
public class OptionsStateService
{
    private readonly Tauri _tauri;
    private bool _initialized;

    /// Current values from axr_options.ltx [options] section
    public Dictionary<string, string> AxrValues { get; private set; } = new();

    /// Current values from user.ltx (console commands)
    public Dictionary<string, string> CmdValues { get; private set; } = new();

    /// Default values from gamedata/configs/plugins/defaults/
    public Dictionary<string, Dictionary<string, string>> Defaults { get; private set; } = new();

    /// Pending unsaved changes keyed by full option path
    public Dictionary<string, OptionChange> PendingChanges { get; } = new();

    public bool HasPendingChanges => PendingChanges.Count > 0;
    public bool IsInitialized => _initialized;

    public event Action? OnStateChanged;

    public OptionsStateService(Tauri tauri)
    {
        _tauri = tauri;
    }

    /// Load defaults and current values from the backend.
    public async Task InitializeAsync()
    {
        if (_initialized) return;

        try
        {
            Defaults = await _tauri.Core.Invoke<Dictionary<string, Dictionary<string, string>>>("get_all_defaults") ?? new();
            await RefreshCurrentValuesAsync();
            _initialized = true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to initialize options: {ex.Message}");
        }
    }

    /// Reload current values from config files.
    public async Task RefreshCurrentValuesAsync()
    {
        try
        {
            var state = await _tauri.Core.Invoke<OptionsState>("get_options");
            if (state != null)
            {
                AxrValues = state.AxrOptions;
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
        else
        {
            if (AxrValues.TryGetValue(fullPath, out var axrVal))
                return axrVal;
        }

        // 3. Check defaults from LTX
        var defaultsSection = $"{pageId}_{groupId}";
        if (Defaults.TryGetValue(defaultsSection, out var sectionDefaults)
            && sectionDefaults.TryGetValue(opt.Id, out var defVal))
            return defVal;

        // 4. Hardcoded default from C# definition
        return opt.DefaultValue;
    }

    /// Record a pending change for an option.
    public void SetPendingChange(OptionDefinition opt, string pageId, string groupId, string value)
    {
        var fullPath = $"{pageId}/{groupId}/{opt.Id}";

        OptionChange change;
        if (opt.ConsoleCommand != null)
        {
            var formattedValue = FormatValueForUserLtx(value, opt);
            change = OptionChange.ForUserLtx(opt.ConsoleCommand, formattedValue);
        }
        else
        {
            change = OptionChange.ForAxrOptions(fullPath, value);
        }

        PendingChanges[fullPath] = change;
        OnStateChanged?.Invoke();
    }

    /// Save all pending changes to the backend.
    public async Task SaveAllAsync()
    {
        if (!HasPendingChanges) return;

        var changes = PendingChanges.Values.ToList();
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
        var defaultsSection = $"{pageId}_{groupId}";
        if (Defaults.TryGetValue(defaultsSection, out var sectionDefaults)
            && sectionDefaults.TryGetValue(opt.Id, out var defVal))
            return defVal;

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
}
