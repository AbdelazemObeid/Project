using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class cartconfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> c)
        {
            c.ToTable("carts");
            c.HasKey(c => c.id);
            c.Property(c => c.id)
                .UseIdentityColumn(1, 1);
            c.HasOne(c => c.user)
                .WithOne(u => u.cart)
                .HasForeignKey<Cart>(c => c.User_id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
