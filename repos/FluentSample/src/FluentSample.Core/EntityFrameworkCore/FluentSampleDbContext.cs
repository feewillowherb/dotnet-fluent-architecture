using FluentSample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FluentSample.Core.EntityFrameworkCore;

/// <summary>
///     Main database context for FluentSample.
///     Inherits from ABP DbContext to get auditing, soft delete, and multi-tenancy support.
/// </summary>
public class FluentSampleDbContext : AbpDbContext<FluentSampleDbContext>
{
    public DbSet<SampleItem> SampleItems => Set<SampleItem>();

    public FluentSampleDbContext(DbContextOptions<FluentSampleDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureSampleItem(modelBuilder);
    }

    private static void ConfigureSampleItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SampleItem>(b =>
        {
            b.ToTable("SampleItems");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Description)
                .HasMaxLength(1000);

            b.Property(x => x.Quantity)
                .IsRequired();

            b.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            b.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            b.Property(x => x.CreationTime)
                .IsRequired();

            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.Name);
        });
    }
}
