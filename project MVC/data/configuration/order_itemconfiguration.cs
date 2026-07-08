using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class order_itemconfiguration : IEntityTypeConfiguration<Order_item>
    {
        public void Configure(EntityTypeBuilder<Order_item> o)
        {
            o.ToTable("order_items");
            o.HasKey(o => o.id);
            o.Property(o => o.id)
                .UseIdentityColumn(1, 1);
            o.Property(o => o.quantity)
                .HasMaxLength(100);
            o.HasOne(o => o.product)
                .WithMany(p => p.order_item)
                .HasForeignKey(o => o.product_id)
                .OnDelete(DeleteBehavior.Restrict);
            o.HasOne(o => o.order)
                .WithMany(p => p.order_items)
                .HasForeignKey(o => o.order_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
