namespace OverPaw.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("Users");

            user.HasKey(key => key.UserId);

            user.Property(x => x.UserId)
                .IsRequired(true)
                .HasMaxLength(100);

            user.Property(x => x.Username)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

            user.Property(x => x.HashedPassword)
                .IsRequired(true)
                .HasMaxLength(100);

            user.Property(x => x.Email)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(100);

            user.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(100);

            user.Property(x => x.FamilyName)
                .IsRequired(true)
                .HasMaxLength(100);

            user.Property(x => x.CreatedOn)
                .IsRequired(true);

            user.Property(x => x.DeletedOn)
                .IsRequired(false);

            user.Property(x => x.IsDeleted)
                .IsRequired(true)
                .HasDefaultValue(false);
        }
    }
}
