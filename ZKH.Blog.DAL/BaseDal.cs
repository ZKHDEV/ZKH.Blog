using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.Model.Enum;

namespace ZKH.Blog.DAL
{
    public class BaseDal<T> where T : class, new()
    {

        public DbContext db = DbContextFactory.GetCurrentDbContext();

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<T> GetPageEntities<S>(int pageIndex, int pageSize, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            total = db.Set<T>().Where(whereLambda).Count();

            if (isAsc)
            {
                var temp = db.Set<T>().Where(whereLambda)
                    .OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();

                return temp;
            }
            else
            {
                var temp = db.Set<T>().Where(whereLambda)
                    .OrderByDescending<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();

                return temp;
            }
        }

        public T Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public bool Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public bool DeleteListByLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = db.Set<T>().Find(id);
                db.Entry(entity).Property("DelFlag").CurrentValue = (short)DelFlagEnum.Deleted;
                db.Entry(entity).Property("DelFlag").IsModified = true;
            }
            return db.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
    }
}
