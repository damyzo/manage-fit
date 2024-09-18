using Entities.Client.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storage.Configuration
{
    public class ClientModelConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.Uid);
            builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(320)").IsRequired();
            builder.Property(x => x.Weight).HasColumnName("Weight").HasColumnType("float").IsRequired();
            builder.Property(x => x.Height).HasColumnName("Height").HasColumnType("float").IsRequired();
        }
    }
}
