using Laboratorio.CRUD.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
