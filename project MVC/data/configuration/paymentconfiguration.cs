using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class paymentconfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> p)
        {
            p.ToTable("Payment");
            p.HasKey(p => p.id);
            p.Property(p => p.id)
                .UseIdentityColumn(1, 1);
            p.Property(p => p.status)
                .IsRequired(false)
                .HasMaxLength(50);
            p.HasOne(p => p.order)
                .WithOne(o => o.payment)
                .HasForeignKey<Order>(p => p.id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
