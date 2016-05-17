using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.IDAL;

namespace ZKH.Blog.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        IBaseDal<T> CurrentDal { get; set; }

        IDbSession dbSession { get; set; }

        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetPageEntities<S>(int pageIndex, int pageSize, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc);

        T Add(T entity);

        bool Delete(T entity);

        bool DeleteListByLogical(List<int> ids);

        bool Update(T entity);

    }
}
