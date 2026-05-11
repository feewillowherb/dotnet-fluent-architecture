using FluentSample.Core.Entities.Enums;

namespace FluentSample.Core.Events;

/// <summary>
///     Message published when an item's status changes.
///     Used for ReactiveUI MessageBus communication between ViewModels.
/// </summary>
/// <param name="ItemId">The unique identifier of the item</param>
/// <param name="NewStatus">The new status after the transition</param>
public class ItemStatusChangedMessage(Guid ItemId, ItemStatus NewStatus)
{
    /// <summary>
    ///     The unique identifier of the affected item
    /// </summary>
    public Guid ItemId { get; } = ItemId;

    /// <summary>
    ///     The new status after the transition
    /// </summary>
    public ItemStatus NewStatus { get; } = NewStatus;
}
