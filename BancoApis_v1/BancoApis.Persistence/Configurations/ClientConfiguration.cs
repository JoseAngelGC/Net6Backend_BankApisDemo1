using BancoApis.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoApis.Persistence.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.BirthDay)
                .IsRequired();
            builder.Property(p => p.Telephone)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Address)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.Age);
            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
