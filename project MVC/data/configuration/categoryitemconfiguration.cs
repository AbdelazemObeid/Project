using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class categoryitemconfiguration : IEntityTypeConfiguration<Categoryitems>
    {
        public void Configure(EntityTypeBuilder<Categoryitems> c)
        {
            c.ToTable("categoryitems");
            c.HasKey(c => new { c.category_id, c.sup_category_id });
            c.HasOne(c => c.categories)
                .WithMany(s => s.items)
                .HasForeignKey(c => c.category_id);
            c.HasOne(c => c.sup_categories)
                .WithMany(s => s.category)
                .HasForeignKey(c => c.sup_category_id);
        }
    }
}
