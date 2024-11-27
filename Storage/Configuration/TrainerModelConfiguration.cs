namespace Storage.Configuration
{
    using Entities.Client.Model;
    using Entities.Trainer.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TrainerModelConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainer");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(320)").IsRequired();

            builder
                .HasMany(x => x.Clients)
                .WithMany(x => x.Trainers)
                .UsingEntity(
                    "TrainerClient",
                    l => l.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)),
                    r => r.HasOne(typeof(Trainer)).WithMany().HasForeignKey("TrainerId").HasPrincipalKey(nameof(Trainer.Id)),
                    j => j.HasKey("ClientId", "TrainerId"));
        }
    }
}
