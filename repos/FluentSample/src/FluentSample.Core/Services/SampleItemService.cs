using FluentSample.Core.Entities;
using FluentSample.Core.Entities.Enums;
using FluentSample.Core.Events;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System.Reactive.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace FluentSample.Core.Services;

/// <summary>
///     Sample item service interface
/// </summary>
public interface ISampleItemService
{
    Task<SampleItem> CreateItemAsync(string name, int quantity, decimal unitPrice);
    Task<SampleItem?> GetItemAsync(Guid id);
    Task<IReadOnlyList<SampleItem>> GetAllItemsAsync();
    Task TransitionStatusAsync(Guid id, ItemStatus newStatus);
    Task DeleteItemAsync(Guid id);
}

/// <summary>
///     Sample item service implementation.
///     Demonstrates ABP domain service with AutoConstructor,
///     repository pattern, and ReactiveUI MessageBus integration.
/// </summary>
[AutoConstructor]
public partial class SampleItemService : DomainService, ISampleItemService, ITransientDependency
{
    private readonly IRepository<SampleItem, Guid> _repository;
    private readonly ILogger<SampleItemService> _logger;

    /// <inheritdoc />
    public async Task<SampleItem> CreateItemAsync(string name, int quantity, decimal unitPrice)
    {
        var item = new SampleItem(name, quantity, unitPrice);
        var created = await _repository.InsertAsync(item, autoSave: true);

        _logger.LogInformation(
            "Created item {ItemId}: {Name}, Qty={Quantity}, Price={UnitPrice}",
            created.Id, created.Name, created.Quantity, created.UnitPrice);

        // Publish event via ReactiveUI MessageBus
        MessageBus.Current.SendMessage(new ItemStatusChangedMessage(created.Id, ItemStatus.Pending));

        return created;
    }

    /// <inheritdoc />
    public async Task<SampleItem?> GetItemAsync(Guid id)
    {
        return await _repository.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<SampleItem>> GetAllItemsAsync()
    {
        return await _repository.GetListAsync();
    }

    /// <inheritdoc />
    public async Task TransitionStatusAsync(Guid id, ItemStatus newStatus)
    {
        var item = await _repository.GetAsync(id);
        item.TransitionTo(newStatus);
        await _repository.UpdateAsync(item, autoSave: true);

        _logger.LogInformation(
            "Item {ItemId} status transitioned to {Status}",
            id, newStatus);

        MessageBus.Current.SendMessage(new ItemStatusChangedMessage(id, newStatus));
    }

    /// <inheritdoc />
    public async Task DeleteItemAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        _logger.LogInformation("Deleted item {ItemId}", id);
    }
}
