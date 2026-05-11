namespace FluentSample.Core.Utils;

/// <summary>
///     Static factory for database connection string manipulation.
///     Demonstrates the Factory Method Pattern for configuration-unrelated logic.
/// </summary>
public static class DatabaseConnectionStringFactory
{
    /// <summary>
    ///     Converts a relative SQLite database path to an absolute path
    ///     based on the application's base directory.
    ///     This ensures the database can be accessed when the app is launched
    ///     from any working directory.
    /// </summary>
    /// <param name="connectionString">Original connection string</param>
    /// <returns>Connection string with absolute path</returns>
    public static string FixConnectionString(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Connection string cannot be empty", nameof(connectionString));
        }

        // Extract the Data Source path from the connection string
        var dataSourceIndex = connectionString.IndexOf("Data Source=", StringComparison.OrdinalIgnoreCase);
        if (dataSourceIndex < 0)
        {
            return connectionString;
        }

        var pathStart = dataSourceIndex + "Data Source=".Length;
        var pathEnd = connectionString.IndexOf(';', pathStart);
        var relativePath = pathEnd < 0
            ? connectionString[pathStart..]
            : connectionString[pathStart..pathEnd];

        if (Path.IsPathRooted(relativePath))
        {
            return connectionString;
        }

        var absolutePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, relativePath));

        return pathEnd < 0
            ? $"Data Source={absolutePath}"
            : connectionString[..pathStart] + absolutePath + connectionString[pathEnd..];
    }
}
