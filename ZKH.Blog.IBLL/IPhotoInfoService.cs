using System.Collections.Generic;
using ZKH.Blog.Model;

namespace ZKH.Blog.IBLL
{
    public partial interface IPhotoInfoService
    {
        bool Move(List<int> ids, int typeId);
    }
}
