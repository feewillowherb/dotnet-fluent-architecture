namespace FluentSample.Core.Configuration;

/// <summary>
///     Application-level settings configuration
/// </summary>
public class AppSettings
{
    /// <summary>
    ///     Application name
    /// </summary>
    public string AppName { get; set; } = "FluentSample";

    /// <summary>
    ///     Maximum retry count for HTTP operations
    /// </summary>
    public int MaxRetryCount { get; set; } = 3;

    /// <summary>
    ///     Request timeout in seconds
    /// </summary>
    public int RequestTimeoutSeconds { get; set; } = 30;

    /// <summary>
    ///     Validates the settings configuration
    /// </summary>
    /// <returns>True if settings are valid</returns>
    public bool IsValid() =>
        !string.IsNullOrWhiteSpace(AppName)
        && MaxRetryCount > 0
        && RequestTimeoutSeconds > 0;
}
