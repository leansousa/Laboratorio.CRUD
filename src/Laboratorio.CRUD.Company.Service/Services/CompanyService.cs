using AutoMapper;
using FluentValidation;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.CRUD.Company.Infra.CrossCutting.Exception;

namespace Laboratorio.CRUD.Company.Service.Services
{
    public class CompanyService : IBaseService<CompanyEntity>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IBaseRepository<CompanySizeEntity> _companySizeRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IBaseRepository<CompanySizeEntity> companySizeRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _companySizeRepository = companySizeRepository;
            _mapper = mapper;
        }

        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<CompanyEntity>
        {
            CompanyEntity entity = _mapper.Map<CompanyEntity>(inputModel);

            Activator.CreateInstance<TValidator>().Validate(entity);

            var companySize = _companySizeRepository.GetById(entity.SizeId) ?? throw new NotFoundException($"Company Size Id {entity.SizeId} is invalid");

            entity.Size = companySize;

            _companyRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public void Delete(int id)
        {
            _companyRepository.Delete(id);
        }

        public IEnumerable<TOutputModel> GetAll<TOutputModel>() where TOutputModel : class
        {
            var entities = _companyRepository.GetAll();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = _companyRepository.GetById(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }


        public IEnumerable<TOutputModel> GetPaginated<TOutputModel>(int page) where TOutputModel : class
        {
            var results = _companyRepository.GetPaginated(page);

            var outputModels = results.Select(s => _mapper.Map<TOutputModel>(s));
            return outputModels;

        }

        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<CompanyEntity>
        {
            CompanyEntity entity = _mapper.Map<CompanyEntity>(inputModel);

            Activator.CreateInstance<TValidator>().Validate(entity);

            var companySize = _companySizeRepository.GetById(entity.SizeId) ?? throw new NotFoundException($"Company Size Id {entity.SizeId} is invalid");

            entity.Size = companySize;
            _companyRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }
    }
}
