using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_MVC.Models;

namespace project_MVC.data.configuration
{
    public class contactconfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> c)
        {
            c.ToTable("contacts");
            c.HasKey(c => c.Id);
            c.Property(c => c.Id)
                .UseIdentityColumn(1, 1);
            c.Property(c => c.subject)
                .IsRequired(false)
                .HasMaxLength(50);
            c.Property(c => c.massage)
                .IsRequired()
                .HasMaxLength(500);
            c.HasOne(c => c.user)
                .WithMany(u => u.contact)
                .HasForeignKey(c => c.user_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
