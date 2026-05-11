using FluentSample.Core.Api.Dtos;
using Refit;

namespace FluentSample.Core.Api;

/// <summary>
///     Refit API interface for sample item remote operations.
///     Demonstrates type-safe HTTP client definition pattern.
/// </summary>
public interface ISampleApi
{
    /// <summary>
    ///     Get all sample items
    /// </summary>
    [Get("/api/items")]
    Task<IReadOnlyList<SampleItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get a single item by ID
    /// </summary>
    [Get("/api/items/{id}")]
    Task<SampleItemDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create a new sample item
    /// </summary>
    [Post("/api/items")]
    Task<SampleItemDto> CreateAsync([Body] CreateSampleItemDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete an item by ID
    /// </summary>
    [Delete("/api/items/{id}")]
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
