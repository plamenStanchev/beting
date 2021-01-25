namespace OddsSystem.Infrastructure.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OddsSystem.Core.Entities.Models;

    public class EventConfigurations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasIndex(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.OddsForFirstTeam)
                .HasColumnType("decimal(9,2)")
                .IsRequired();
            builder.Property(e => e.OddsForSecondTeam)
                .HasColumnType("decimal(9,2)")
                .IsRequired();
            builder.Property(e => e.OddsForDraw)
                .HasColumnType("decimal(9,2)")
                .IsRequired();

            builder.Property(e => e.EventStartDate)
                .IsRequired();

        }
    }
}
