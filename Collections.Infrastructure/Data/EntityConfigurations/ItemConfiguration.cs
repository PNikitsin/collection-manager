using Collections.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collections.Infrastructure.Data.EntityConfigurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.Property(category => category.Name).HasMaxLength(128)
                .IsRequired();

            builder.Property(category => category.Description).HasMaxLength(512)
                .IsRequired();

            builder.Property(collection => collection.CollectionId)
                .IsRequired();
        }
    }
}