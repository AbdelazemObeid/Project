using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class orderconfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> o)
        {
            o.ToTable("orders");
            o.HasKey(o => o.id);
            o.Property(o => o.id)
                .UseIdentityColumn(1, 1);
            o.HasOne(o => o.user)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            o.Property(o => o.country)
                .IsRequired()
                .HasMaxLength(50);
            o.Property(o => o.city)
                .IsRequired()
                .HasMaxLength(50);
            o.Property(o => o.address)
                .IsRequired()
                .HasMaxLength(255);
            o.Property(o => o.note)
                .IsRequired(false)
                .HasMaxLength(500);
        }
    }
}
