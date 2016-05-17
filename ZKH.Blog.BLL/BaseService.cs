using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.IDAL;

namespace ZKH.Blog.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession dbSession { get; set; }

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }

        public IQueryable<T> GetPageEntities<S>(int pageIndex, int pageSize, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageIndex, pageSize, out total, whereLambda, orderByLambda, isAsc);
        }

        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            return entity;
        }

        public bool Delete(T entity)
        {
            return CurrentDal.Delete(entity);
        }

        public bool DeleteListByLogical(List<int> ids)
        {
            return CurrentDal.DeleteListByLogical(ids);
        }

        public bool Update(T entity)
        {
           return CurrentDal.Update(entity);
        }
    }
}
