using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.Model;

namespace ZKH.Blog.DAL
{
    public partial class PhotoInfoDal
    {
        public bool Move(List<int> ids, int typeId)
        {
            foreach (var id in ids)
            {
                var entity = db.Set<PhotoInfo>().Find(id);
                db.Entry(entity).Property("PhotoTypeId").CurrentValue = typeId;
                db.Entry(entity).Property("PhotoTypeId").IsModified = true;
            }
            return db.SaveChanges() > 0;
        }
    }
}
