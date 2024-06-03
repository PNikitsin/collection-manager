using Collections.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collections.Infrastructure.Data.EntityConfigurations
{
    internal class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collections");

            builder.HasKey(collection => collection.Id);

            builder.Property(category => category.Name).HasMaxLength(128)
                .IsRequired();

            builder.Property(category => category.Description).HasMaxLength(512)
                .IsRequired();

            builder.Property(category => category.Author).HasMaxLength(64)
                .IsRequired();

            builder.Property(collection => collection.UserId)
                .IsRequired();

            builder.Property(collection => collection.CategoryId)
                .IsRequired();
        }
    }
}