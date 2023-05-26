using Laboratorio.CRUD.Company.Domain.Entities.Base;

namespace Laboratorio.CRUD.Company.Domain.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(int id);

        IList<TEntity> GetAll();

        TEntity? GetById(int id);
    }
}
