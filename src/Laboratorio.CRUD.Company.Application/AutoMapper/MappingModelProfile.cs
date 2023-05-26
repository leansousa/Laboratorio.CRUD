using AutoMapper;
using Laboratorio.CRUD.Company.Application.Models;
using Laboratorio.CRUD.Company.Domain.DTOs;
using Laboratorio.CRUD.Company.Domain.Entities;

namespace Laboratorio.CRUD.Company.Application.AutoMapper
{
    public class MappingModelProfile : Profile
    {
        public MappingModelProfile()
        {
            CreateMap<AddCompanyModel, CompanyEntity>();
            CreateMap<UpdateCompanyModel, CompanyEntity>();
            CreateMap<CompanyEntity, CompanyModel>().ForMember(d => d.Size, opt => opt.MapFrom(s => s.Size));          
            CreateMap<CompanySizeEntity, CompanySizeModel>();
            CreateMap<CompanySizeModel, CompanySizeEntity>();

            CreateMap<CompanySizeEntity, AddUpdateCompanySizeModel>();
            CreateMap<AddUpdateCompanySizeModel, CompanySizeEntity >();
            CreateMap<CompanyPaginatedDTO, GridCompanyModel>();
        }
    }
}
