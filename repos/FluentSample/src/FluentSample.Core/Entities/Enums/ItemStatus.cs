namespace FluentSample.Core.Entities.Enums;

/// <summary>
///     Represents the status of an order item
/// </summary>
public enum ItemStatus
{
    /// <summary>
    ///     Item has been created but not yet processed
    /// </summary>
    Pending,

    /// <summary>
    ///     Item is currently being processed
    /// </summary>
    Processing,

    /// <summary>
    ///     Item processing has been completed
    /// </summary>
    Completed,

    /// <summary>
    ///     Item processing failed
    /// </summary>
    Failed
}
