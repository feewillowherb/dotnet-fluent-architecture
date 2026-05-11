using FluentSample.Core.Entities;
using FluentSample.Core.Entities.Enums;
using Shouldly;
using Xunit;

namespace FluentSample.Core.Tests;

/// <summary>
///     Unit tests for SampleItem entity.
///     Demonstrates test-first approach with Shouldly assertions.
/// </summary>
public class SampleItemTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_Correctly()
    {
        // Act
        var item = new SampleItem("Test Item", 10, 25.50m);

        // Assert
        item.Name.ShouldBe("Test Item");
        item.Quantity.ShouldBe(10);
        item.UnitPrice.ShouldBe(25.50m);
        item.Status.ShouldBe(ItemStatus.Pending);
        item.CreationTime.ShouldNotBe(default);
    }

    [Fact]
    public void CalculateTotalPrice_Should_Return_Quantity_Times_UnitPrice()
    {
        // Arrange
        var item = new SampleItem("Widget", 5, 10.00m);

        // Act
        var total = item.CalculateTotalPrice();

        // Assert
        total.ShouldBe(50.00m);
    }

    [Fact]
    public void UpdateQuantity_Should_Update_Quantity_And_ModificationTime()
    {
        // Arrange
        var item = new SampleItem("Widget", 5, 10.00m);

        // Act
        item.UpdateQuantity(20);

        // Assert
        item.Quantity.ShouldBe(20);
        item.LastModificationTime.ShouldNotBeNull();
    }

    [Fact]
    public void UpdateQuantity_Should_Throw_For_Negative_Quantity()
    {
        // Arrange
        var item = new SampleItem("Widget", 5, 10.00m);

        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() => item.UpdateQuantity(-1));
    }

    [Theory]
    [InlineData("", typeof(ArgumentException))]
    [InlineData("   ", typeof(ArgumentException))]
    public void Constructor_Should_Throw_For_Invalid_Name(string name, Type expectedException)
    {
        Should.Throw(() => new SampleItem(name, 1, 1.00m), expectedException);
    }

    [Fact]
    public void TransitionTo_Should_Allow_Valid_Transitions()
    {
        var item = new SampleItem("Widget", 1, 1.00m);

        // Pending -> Processing (valid)
        item.TransitionTo(ItemStatus.Processing);
        item.Status.ShouldBe(ItemStatus.Processing);

        // Processing -> Completed (valid)
        item.TransitionTo(ItemStatus.Completed);
        item.Status.ShouldBe(ItemStatus.Completed);
    }

    [Fact]
    public void TransitionTo_Should_Throw_For_Invalid_Transitions()
    {
        var item = new SampleItem("Widget", 1, 1.00m);

        // Pending -> Completed (invalid, must go through Processing)
        Should.Throw<InvalidOperationException>(
            () => item.TransitionTo(ItemStatus.Completed));

        // Pending -> Failed (invalid)
        Should.Throw<InvalidOperationException>(
            () => item.TransitionTo(ItemStatus.Failed));
    }

    [Fact]
    public void TransitionTo_Should_Allow_Failed_To_Processing_Retry()
    {
        var item = new SampleItem("Widget", 1, 1.00m);
        item.TransitionTo(ItemStatus.Processing);
        item.TransitionTo(ItemStatus.Failed);

        // Failed -> Processing (retry, valid)
        item.TransitionTo(ItemStatus.Processing);
        item.Status.ShouldBe(ItemStatus.Processing);
    }
}
