using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Laboratorio.CRUD.Company.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Laboratorio.CRUD.Company.Tests
{
    [TestCaseOrderer(ordererTypeName: "Laboratorio.CRUD.Company.Tests.OrderHelper", ordererAssemblyName: "Laboratorio.CRUD.Company.Tests")]
    public class UnitTestOfCompanySizeRepository
    {
        private readonly DbContextOptionsBuilder<SqlServerContext> _dbContextOptions;
        private readonly IBaseRepository<CompanySizeEntity> _repository;
        private readonly IConfiguration _configuration;

        public UnitTestOfCompanySizeRepository()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, true).Build();

            var connectionString = _configuration.GetValue<string>("DBConnection:SQLServerConnectionString") ?? "";

            _dbContextOptions = new DbContextOptionsBuilder<SqlServerContext>()
                .UseSqlServer(connectionString);

            if (_repository == null)
            {
                SqlServerContext context = new(_dbContextOptions.Options);
                _repository = new BaseRepository<CompanySizeEntity>(context);
            }
        }

        [Fact]
        public void CTU_001_ObterTodoOsPortesDeEmpresaComSucesso()
        {
            var result = _repository.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void CTU_002_ObterOPorteDaEmpresa03GrandeComSucesso()
        {
            var result = _repository.GetById(3);
            Assert.NotNull(result);
            Assert.True(result.Description == "Grande");
        }
    }
}