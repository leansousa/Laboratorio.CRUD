using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.CRUD.Company.Infra.Data.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext()
        { }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<CompanySizeEntity> CompanySizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>(new CompanyMap().Configure);
            modelBuilder.Entity<CompanySizeEntity>(new CompanySizeMap().Configure);

            GenerateCompanySize(ref modelBuilder);

            GenerateCompany(ref modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string _connectionToMigration = "Data Source=.;Initial Catalog=laboratorio_crud_opea;User ID=laboratorio;Password=laboratorio;TrustServerCertificate=True";

                optionsBuilder.UseSqlServer(_connectionToMigration).EnableSensitiveDataLogging();
            }
        }

        #region Private Methods

        private static void GenerateCompanySize(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanySizeEntity>().HasData(new CompanySizeEntity
            {
                Id = 1,
                Description = "Pequena",
            });

            modelBuilder.Entity<CompanySizeEntity>().HasData(new CompanySizeEntity
            {
                Id = 2,
                Description = "Média",
            });

            modelBuilder.Entity<CompanySizeEntity>().HasData(new CompanySizeEntity
            {
                Id = 3,
                Description = "Grande",
            });
        }

        private static void GenerateCompany(ref ModelBuilder modelBuilder)
        {
            Random randNum = new();

            for (var i = 1; i <= 50; i++)
            {
                var idSize = randNum.Next(1, 3);

                modelBuilder.Entity<CompanyEntity>().HasData(new CompanyEntity
                {
                    Id = i,
                    Name = $"Empresa {i.ToString().PadLeft(2, '0')}",
                    SizeId = idSize,
                });
            }
        }

        #endregion Private Methods
    }
}