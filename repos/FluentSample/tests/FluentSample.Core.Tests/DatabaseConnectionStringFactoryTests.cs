using FluentSample.Core.Utils;
using Shouldly;
using Xunit;

namespace FluentSample.Core.Tests;

/// <summary>
///     Unit tests for DatabaseConnectionStringFactory.
/// </summary>
public class DatabaseConnectionStringFactoryTests
{
    [Fact]
    public void FixConnectionString_Should_Convert_Relative_Path()
    {
        // Arrange
        var relative = "Data Source=app.db";

        // Act
        var result = DatabaseConnectionStringFactory.FixConnectionString(relative);

        // Assert
        result.ShouldStartWith("Data Source=");
        result.ShouldContain("app.db");
        result.ShouldNotBe(relative);
    }

    [Fact]
    public void FixConnectionString_Should_Keep_Absolute_Path_Unchanged()
    {
        // Arrange
        var absolute = $"Data Source={Path.GetTempPath()}app.db";

        // Act
        var result = DatabaseConnectionStringFactory.FixConnectionString(absolute);

        // Assert
        result.ShouldBe(absolute);
    }

    [Fact]
    public void FixConnectionString_Should_Preserve_Other_Parameters()
    {
        // Arrange
        var connStr = "Data Source=app.db;Version=3;New=True";

        // Act
        var result = DatabaseConnectionStringFactory.FixConnectionString(connStr);

        // Assert
        result.ShouldContain("Version=3");
        result.ShouldContain("New=True");
    }
}
