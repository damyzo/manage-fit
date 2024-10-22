namespace Storage.Configuration
{
    using Entities.Trainer.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TrainerModelConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainer");

            builder.HasKey(x => x.Uid);
            builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(320)").IsRequired();

            builder
                .HasMany(x => x.Clients)
                .WithMany(x => x.Trainers);
        }
    }
}
