using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;

namespace Laboratorio.CRUD.Company.Domain.Interfaces
{
    public interface ICompanyService : IBaseService<CompanyEntity>
    {
        IEnumerable<TOutputModel> GetPaginated<TOutputModel>(int page) where TOutputModel : class;
    }
}