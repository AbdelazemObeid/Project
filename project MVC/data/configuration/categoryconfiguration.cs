using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> c)
        {
            c.ToTable("categories");
            c.HasKey(c => c.id);
            c.Property(c => c.id)
                .UseIdentityColumn(1, 1);
            c.Property(c => c.name)
                .IsRequired()
                .HasMaxLength(50);
            c.HasIndex(c => c.name)
                .IsUnique(true);
        }
    }
}
