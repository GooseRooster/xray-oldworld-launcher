using System.Net.Http.Json;

namespace XrayOldworldLauncher.Services;

public class LocalizationService
{
    private readonly HttpClient _http;
    private Dictionary<string, string> _strings = new();

    public string CurrentLanguage { get; private set; } = "en";
    public event Action? OnLanguageChanged;

    public LocalizationService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Initialize with the persisted language from LauncherConfig.
    /// Must be called before first render.
    /// </summary>
    public async Task InitializeAsync(string language)
    {
        await LoadLanguageAsync(language);
    }

    /// <summary>
    /// Switch language at runtime. Fires OnLanguageChanged so components re-render.
    /// </summary>
    public async Task SetLanguageAsync(string language)
    {
        if (CurrentLanguage == language) return;
        await LoadLanguageAsync(language);
        OnLanguageChanged?.Invoke();
    }

    /// <summary>
    /// Primary lookup. Returns the localized string, or the key itself as fallback.
    /// </summary>
    public string T(string key)
    {
        return _strings.TryGetValue(key, out var value) ? value : key;
    }

    /// <summary>
    /// Optional lookup. Returns the localized string or null if the key doesn't exist.
    /// Use for conditional display (e.g. descriptions that not every option has).
    /// </summary>
    public string? TryGet(string key)
    {
        return _strings.TryGetValue(key, out var value) ? value : null;
    }

    /// <summary>
    /// Two-tier content text lookup for List/Radio display values.
    /// 1. Option-specific: "opt.{optionId}.content.{value}"
    /// 2. Global: "content.{value}"
    /// 3. Raw value as fallback
    /// </summary>
    public string ContentText(string optionId, string value)
    {
        var specificKey = $"opt.{optionId}.content.{value}";
        if (_strings.TryGetValue(specificKey, out var specific))
            return specific;

        var globalKey = $"content.{value}";
        if (_strings.TryGetValue(globalKey, out var global))
            return global;

        return value;
    }

    private async Task LoadLanguageAsync(string language)
    {
        try
        {
            var dict = await _http.GetFromJsonAsync<Dictionary<string, string>>($"locales/{language}.json");
            _strings = dict ?? new();
            CurrentLanguage = language;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load locale '{language}': {ex.Message}");
            // If we have no strings and requested language wasn't English, try English fallback
            if (_strings.Count == 0 && language != "en")
            {
                try
                {
                    var fallback = await _http.GetFromJsonAsync<Dictionary<string, string>>("locales/en.json");
                    _strings = fallback ?? new();
                    CurrentLanguage = "en";
                }
                catch
                {
                    _strings = new();
                }
            }
        }
    }
}
