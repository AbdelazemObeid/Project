using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class cart_itemconfiguration : IEntityTypeConfiguration<Cart_item>
    {
        public void Configure(EntityTypeBuilder<Cart_item> c)
        {
            c.ToTable("cart_items");
            c.HasKey(c => c.id);
            c.Property(c => c.id)
                .UseIdentityColumn(1, 1);
            c.Property(c => c.quantity)
                .IsRequired();
            c.HasOne(c => c.product)
                .WithMany(p => p.cart_item)
                .HasForeignKey(c => c.product_id)
                .OnDelete(DeleteBehavior.Restrict);
            c.HasOne(c => c.cart)
                .WithMany(ca => ca.cart_item)
                .HasForeignKey(c => c.cart_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
