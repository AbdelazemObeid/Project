using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> p)
        {
            p.ToTable("products");
            p.HasKey(p => p.id);
            p.Property(p => p.id)
                .UseIdentityColumn(1, 1);
            p.Property(p => p.name)
                .HasMaxLength(100)
                .IsRequired();
            p.Property(p => p.price)
                .IsRequired()
                .HasColumnType("money");
            p.HasOne(p => p.category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.category_id)
                .OnDelete(DeleteBehavior.Restrict);
            p.Property(p => p.description)
                .IsRequired(false)
                .HasMaxLength(500);
            p.Property(p => p.quantity)
                .IsRequired();
            p.HasOne(p => p.sup_category)
                .WithMany(s => s.products)
                .HasForeignKey(p => p.sup_category_id)
                .OnDelete(DeleteBehavior.Restrict);
            p.HasOne(p => p.user)
                .WithMany(u => u.products)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            p.HasMany(p => p.colors)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            p.HasMany(p => p.size)
                .WithOne(ps => ps.Product)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            p.HasMany(p => p.productimages)
                .WithOne(pi => pi.product)
                .HasForeignKey(pi => pi.product_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
