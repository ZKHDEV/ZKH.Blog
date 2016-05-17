using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.IBLL;
using ZKH.Blog.Model;

namespace ZKH.Blog.BLL
{
    public partial class PhotoInfoService
    {
        public bool Move(List<int> ids, int typeId)
        {
            return dbSession.PhotoInfoDal.Move(ids, typeId);
        }
    }
}
