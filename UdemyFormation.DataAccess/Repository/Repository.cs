using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;

namespace UdemyFormation.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext db;
        protected DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperty)
        {
            IQueryable<T> query = dbSet.Where(filter);
            if (includeProperty != null)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperty = null)
        {
            var query = dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperty != null)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}