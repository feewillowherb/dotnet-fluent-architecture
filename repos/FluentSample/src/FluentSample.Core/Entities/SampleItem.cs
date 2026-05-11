using FluentSample.Core.Entities.Enums;
using Volo.Abp.Domain.Entities;

namespace FluentSample.Core.Entities;

/// <summary>
///     Represents a sample item in the system.
///     Demonstrates rich domain model with business behavior.
/// </summary>
public class SampleItem : Entity<Guid>
{
    /// <summary>
    ///     Item name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Item description
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    ///     Item quantity
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    ///     Unit price
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    ///     Current item status
    /// </summary>
    public ItemStatus Status { get; private set; }

    /// <summary>
    ///     Creation time (audit field)
    /// </summary>
    public DateTime CreationTime { get; private set; }

    /// <summary>
    ///     Last modification time
    /// </summary>
    public DateTime? LastModificationTime { get; private set; }

    /// <summary>
    ///     Creates a new sample item with required fields
    /// </summary>
    public SampleItem(
        string name,
        int quantity,
        decimal unitPrice,
        ItemStatus status = ItemStatus.Pending)
    {
        Name = GuardName(name);
        Quantity = GuardQuantity(quantity);
        UnitPrice = GuardUnitPrice(unitPrice);
        Status = status;
        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     EF Core constructor (do not use directly)
    /// </summary>
    private SampleItem()
    {
        Name = string.Empty;
    }

    /// <summary>
    ///     Calculates the total price for this item
    /// </summary>
    public decimal CalculateTotalPrice() => Quantity * UnitPrice;

    /// <summary>
    ///     Updates the item quantity with validation
    /// </summary>
    /// <param name="newQuantity">New quantity value</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when quantity is negative</exception>
    public void UpdateQuantity(int newQuantity)
    {
        Quantity = GuardQuantity(newQuantity);
        LastModificationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Transitions the item to a new status
    /// </summary>
    /// <param name="newStatus">Target status</param>
    /// <exception cref="InvalidOperationException">Thrown when status transition is invalid</exception>
    public void TransitionTo(ItemStatus newStatus)
    {
        if (!IsValidTransition(Status, newStatus))
        {
            throw new InvalidOperationException(
                $"Cannot transition from {Status} to {newStatus}");
        }

        Status = newStatus;
        LastModificationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Checks whether a status transition is valid
    /// </summary>
    private static bool IsValidTransition(ItemStatus from, ItemStatus to)
    {
        return (from, to) switch
        {
            (ItemStatus.Pending, ItemStatus.Processing) => true,
            (ItemStatus.Processing, ItemStatus.Completed) => true,
            (ItemStatus.Processing, ItemStatus.Failed) => true,
            (ItemStatus.Failed, ItemStatus.Processing) => true,
            _ => false
        };
    }

    private static string GuardName(string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Name cannot be empty", nameof(name))
            : name.Trim();
    }

    private static int GuardQuantity(int quantity)
    {
        return quantity < 0
            ? throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative")
            : quantity;
    }

    private static decimal GuardUnitPrice(decimal unitPrice)
    {
        return unitPrice < 0
            ? throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative")
            : unitPrice;
    }
}
