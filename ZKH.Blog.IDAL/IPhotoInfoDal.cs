using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.Model;

namespace ZKH.Blog.IDAL
{
    public partial interface IPhotoInfoDal
    {
        bool Move(List<int> ids, int typeId);
    }
}
