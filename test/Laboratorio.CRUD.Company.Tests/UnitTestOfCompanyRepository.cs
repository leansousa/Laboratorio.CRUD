using AutoMapper;
using Laboratorio.CRUD.Company.Application.AutoMapper;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Laboratorio.CRUD.Company.Infra.Data.DBClient;
using Laboratorio.CRUD.Company.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Laboratorio.CRUD.Company.Tests
{
    [TestCaseOrderer(ordererTypeName: "Laboratorio.CRUD.Company.Tests.OrderHelper", ordererAssemblyName: "Laboratorio.CRUD.Company.Tests")]
    public class UnitTestOfCompanyRepository
    {
        private readonly DbContextOptionsBuilder<SqlServerContext> _dbContextOptions;
        private readonly ICompanyRepository _repository;
        private static IMapper? _mapper;
        private readonly IConfiguration _configuration;
        private static int idToTest;

        public UnitTestOfCompanyRepository()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, true).Build();

            var connectionString = _configuration.GetValue<string>("DBConnection:SQLServerConnectionString") ?? "";

            _dbContextOptions = new DbContextOptionsBuilder<SqlServerContext>()
                .UseSqlServer(connectionString);

            if (_mapper == null)
            {
                MapperConfiguration mapper = new(cfg =>
                {
                    cfg.AddProfile(new MappingModelProfile());
                });

                _mapper = mapper.CreateMapper();
            }

            if (_repository == null)
            {
                SqlServerContext context = new(_dbContextOptions.Options);
                SqlServerConnection connection = new(connectionString);

                _repository = new CompanyRepository(context, connection);
            }
        }

        [Fact]
        public void CTU_001_InserirEmpresaComSucesso()
        {
            var vo = new CompanyEntity
            {
                Id = 0,
                Name = "Test",
                SizeId = 1,
            };

            _repository.Insert(vo);

            idToTest = vo.Id;

            Assert.True(idToTest > 0);
        }

        [Fact]
        public void CTU_002_AlterarEmpresaComSucesso()
        {
            var vo = new CompanyEntity
            {
                Id = idToTest,
                Name = "Test Alter",
                SizeId = 2,
            };

            _repository.Update(vo);

            var result = _repository.GetById(idToTest);

            Assert.NotNull(result);

            Assert.NotNull(result.Size);

            Assert.True(result.Name == vo.Name && result.Size.Id == vo.SizeId);
        }

        [Fact]
        public void CTU_003_ExcluirEmpresaComSucesso()
        {
            _repository.Delete(idToTest);

            var result = _repository.GetById(idToTest);

            Assert.Null(result);
        }

        [Fact]
        public void CTU_004_ObterEmpresasComPaginacao()
        {
            var result = _repository.GetPaginated(3);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact]
        public void CTU_005_ObterEmpresasComPaginacaoPaginaInexistente()
        {
            var result = _repository.GetPaginated(10);

            Assert.NotNull(result);
            Assert.False(result.Any());
        }
    }
}