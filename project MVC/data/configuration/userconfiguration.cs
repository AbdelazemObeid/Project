using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class userconfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> u)
        {
            u.ToTable("users");
            u.HasKey(u => u.id);
            u.Property(u => u.id)
                .UseIdentityColumn(1, 1);
            u.Property(u => u.name)
                .HasColumnType("varchar")
                .IsRequired(true)
                .HasMaxLength(100);
            u.Property(u => u.email)
                .IsRequired(true)
                .HasMaxLength(50);
            u.Property(u => u.password)
                .IsRequired(true)
                .HasMaxLength(50);
            u.Property(u => u.phone_number)
                .IsRequired(false)
                .HasMaxLength(11);
            u.HasIndex(u => u.email)
                .IsUnique(true);
            u.Property(u => u.role)
                .IsRequired(true);
        }
    }
}
