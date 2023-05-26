using Laboratorio.CRUD.Company.Domain.Entities.Base;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.CRUD.Company.Infra.Data.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlServerContext _context;

        public BaseRepository(SqlServerContext context)
        {
            _context = context;
        }

        public virtual void Insert(TEntity obj)
        {            
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entitie = GetById(id);
            if (entitie != null && entitie.Id > 0)
            {
                _context.Set<TEntity>().Remove(entitie);
                _context.SaveChanges();
            }
        }

        public virtual IList<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
