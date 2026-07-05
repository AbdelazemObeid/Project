using Microsoft.EntityFrameworkCore;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class SupCategoriesConfiguration : IEntityTypeConfiguration<Sup_category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Sup_category> s)
        {
            s.ToTable("sup_categories");
            s.HasKey(e => e.Id);
            s.Property(e => e.Id)
                .UseIdentityColumn(1, 1);
            s.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
