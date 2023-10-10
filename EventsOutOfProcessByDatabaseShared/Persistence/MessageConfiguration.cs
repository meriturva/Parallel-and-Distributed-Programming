using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsOutOfProcessByDatabaseShared.Persistence
{
    internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(name: "Messages");

            builder.HasKey(ob => ob.Id);

            builder.Property(ob => ob.Id).HasColumnName("Id");
            builder.Property(ob => ob.Type).HasColumnName("Type");
            builder.Property(ob => ob.Content).HasColumnName("Content");
            builder.Property(ob => ob.ProcessedOn).HasColumnName("ProcessedOn");
            builder.Property(ob => ob.OccurredOn).HasColumnName("OccurredOn");

            builder.HasIndex(ob => ob.OccurredOn);
            builder.HasIndex(ob => ob.ProcessedOn);
        }
    }
}