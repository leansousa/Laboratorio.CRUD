using AutoMapper;
using Laboratorio.CRUD.Company.Application.AutoMapper;
using Laboratorio.CRUD.Company.Application.Models;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Laboratorio.CRUD.Company.Infra.Data.DBClient;
using Laboratorio.CRUD.Company.Infra.Data.Repository;
using Laboratorio.CRUD.Company.Infra.Data.Repository.Base;
using Laboratorio.CRUD.Company.Service.Services;
using Laboratorio.DDD.Company.Service.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Laboratorio.CRUD.Company.Tests
{
    [TestCaseOrderer(ordererTypeName: "Laboratorio.CRUD.Company.Tests.OrderHelper", ordererAssemblyName: "Laboratorio.CRUD.Company.Tests")]
    public class UnitTestOfCompanyService
    {
        private readonly DbContextOptionsBuilder<SqlServerContext> _dbContextOptions;
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IBaseRepository<CompanySizeEntity> _repositoryCompanySize;
        private readonly ICompanyService _serviceCompany;
        private static IMapper? _mapper;
        private readonly IConfiguration _configuration;
        private static int idToTest;

        public UnitTestOfCompanyService()
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

            SqlServerContext context = new(_dbContextOptions.Options);

            if (_repositoryCompany == null)
            {
                SqlServerConnection connection = new(connectionString);

                _repositoryCompany = new CompanyRepository(context, connection);
            }

            if (_repositoryCompanySize == null)
            {
                _repositoryCompanySize = new BaseRepository<CompanySizeEntity>(context);
            }

            if (_serviceCompany == null)
            {
                _serviceCompany = new CompanyService(_repositoryCompany, _repositoryCompanySize, _mapper);
            }
        }

        [Fact]
        public void CTU_001_InserirEmpresaComSucesso()
        {
            var model = new AddCompanyModel
            {
                Name = "Test",
                Size = new AddUpdateCompanySizeModel { Id = 1 }
            };

            var result = _serviceCompany.Add<AddCompanyModel, CompanyModel, CompanyValidator>(model);

            idToTest = result.Id;

            Assert.True(idToTest > 0);
        }

        [Fact]
        public void CTU_002_AlterarEmpresaComSucesso()
        {
            var model = new UpdateCompanyModel
            {
                Id = idToTest,
                Name = "Test Alter",
                Size = new AddUpdateCompanySizeModel { Id = 2 }
            };

            var resultUpdate = _serviceCompany.Update<UpdateCompanyModel, CompanyModel, CompanyValidator>(model);

            var resultById = _serviceCompany.GetById<CompanyModel>(idToTest);

            Assert.NotNull(resultUpdate);
            Assert.NotNull(resultById);

            Assert.NotNull(resultUpdate.Size);
            Assert.NotNull(resultById.Size);

            Assert.True(resultUpdate.Name == resultById.Name && resultUpdate.Size.Id == resultById.Size.Id);
        }

        [Fact]
        public void CTU_003_ExcluirEmpresaComSucesso()
        {
            _serviceCompany.Delete(idToTest);

            var resultById = _serviceCompany.GetById<CompanyModel>(idToTest);

            Assert.Null(resultById);
        }

        [Fact]
        public void CTU_004_ObterEmpresasComPaginacao()
        {
            var result = _serviceCompany.GetPaginated<GridCompanyModel>(2);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact]
        public void CTU_005_ObterEmpresasComPaginacaoPaginaInexistente()
        {
            var result = _serviceCompany.GetPaginated<GridCompanyModel>(20);

            Assert.NotNull(result);
            Assert.False(result.Any());
        }
    }
}