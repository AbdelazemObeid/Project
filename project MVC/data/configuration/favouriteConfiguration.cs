using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class favouriteConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> f)
        {
            f.ToTable("favourites");
            f.HasKey(f => f.favourite_id);
            f.Property(f => f.favourite_id)
                .UseIdentityColumn(1, 1);
            f.HasOne(f => f.user)
                .WithMany(u => u.favourites)
                .HasForeignKey(f => f.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            f.HasOne(f => f.product)
                .WithMany(p => p.favourites)
                .HasForeignKey(f => f.product_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
    
