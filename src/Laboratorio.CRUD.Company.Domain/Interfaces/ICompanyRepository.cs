using Laboratorio.CRUD.Company.Domain.DTOs;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;

namespace Laboratorio.CRUD.Company.Domain.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity>
    {
        IEnumerable<CompanyPaginatedDTO> GetPaginated(int page);

        bool NameExists(int id, string name);
    }
}