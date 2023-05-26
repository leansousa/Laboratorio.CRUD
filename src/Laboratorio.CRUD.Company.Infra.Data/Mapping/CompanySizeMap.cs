using Laboratorio.CRUD.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laboratorio.CRUD.Company.Infra.Data.Mapping
{
    public class CompanySizeMap : IEntityTypeConfiguration<CompanySizeEntity>
    {
        public void Configure(EntityTypeBuilder<CompanySizeEntity> builder)
        {
            builder.ToTable("SizeCompany");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasMaxLength(20)
                .HasColumnType("VARCHAR");
        }
    }
}