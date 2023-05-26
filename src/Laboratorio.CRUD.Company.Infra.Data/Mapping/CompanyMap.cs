using Laboratorio.CRUD.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laboratorio.CRUD.Company.Infra.Data.Mapping
{
    public class CompanyMap : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(60)
                .HasColumnType("VARCHAR");

            builder.HasOne(d => d.Size).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(s => s.SizeId);
        }
    }
}