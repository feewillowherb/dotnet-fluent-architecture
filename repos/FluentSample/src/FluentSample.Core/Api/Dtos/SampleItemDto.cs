namespace FluentSample.Core.Api.Dtos;

/// <summary>
///     DTO for creating a new sample item
/// </summary>
public record CreateSampleItemDto(string Name, int Quantity, decimal UnitPrice);

/// <summary>
///     DTO for sample item response
/// </summary>
public record SampleItemDto(Guid Id, string Name, string? Description, int Quantity, decimal UnitPrice, string Status, DateTime CreationTime)
{
    /// <summary>
    ///     Calculates the total price from quantity and unit price
    /// </summary>
    public decimal TotalPrice => Quantity * UnitPrice;
}

/// <summary>
///     DTO for updating item status
/// </summary>
public record UpdateItemStatusDto(ItemStatus Status);
