using Microsoft.EntityFrameworkCore;
using StudentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntitiy, new() where TContext : DbContext
    {
        protected readonly TContext _context;
        public EfEntityRepositoryBase(TContext context)
        {

            _context = context;

        }
        public void Add(TEntity entity)
        {
         
                var addedData=_context.Entry(entity);
                addedData.State = EntityState.Added;
                _context.SaveChanges();

            
        }

        public void Delete(TEntity entity)
        {
           
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            
                return _context.Set<TEntity>().SingleOrDefault(predicate);
           
        }

        public List<TEntity> GetAll()
        {

                return _context.Set<TEntity>().ToList();
           
        }

        public void Update(TEntity entity)
        {
          
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();

        }
    }
}
