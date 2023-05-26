using AutoMapper;
using Laboratorio.CRUD.Company.Application.AutoMapper;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.CRUD.Company.Infra.Data.Repository;
using Laboratorio.CRUD.Company.Infra.Data.Repository.Base;
using Laboratorio.CRUD.Company.Service.Services;
using Laboratorio.CRUD.Company.Service.Services.Base;

namespace Laboratorio.CRUD.Company.Application.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            MapperConfiguration mapper = new(cfg =>
            {
                cfg.AddProfile(new MappingModelProfile());
            });

            services.AddSingleton(mapper.CreateMapper());

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IBaseRepository<CompanySizeEntity>, BaseRepository<CompanySizeEntity>>();
            services.AddScoped<IBaseService<CompanySizeEntity>, BaseService<CompanySizeEntity>>();
        }
    }
}