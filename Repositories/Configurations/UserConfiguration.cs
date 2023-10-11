using fsw.web.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fsw.web.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.id);

            builder.Property(x => x.id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
               .HasColumnName("Username");

            builder.Property(x => x.Password)
               .HasColumnName("Password");

        }
    }
}
