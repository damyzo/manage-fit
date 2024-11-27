namespace Storage.Configuration
{
    using Entities.Client.Model;
    using Entities.Trainer.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClientModelConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(320)").IsRequired();
            builder.Property(x => x.Weight).HasColumnName("Weight").HasColumnType("float").IsRequired();
            builder.Property(x => x.Height).HasColumnName("Height").HasColumnType("float").IsRequired();

            builder
                .HasMany(x => x.Trainers)
                .WithMany(x => x.Clients)
                .UsingEntity(
                    "TrainerClient",
                    l => l.HasOne(typeof(Trainer)).WithMany().HasForeignKey("TrainerId").HasPrincipalKey(nameof(Trainer.Id)),
                    r => r.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)),
                    j => j.HasKey("ClientId", "TrainerId"));
        }
    }
}
