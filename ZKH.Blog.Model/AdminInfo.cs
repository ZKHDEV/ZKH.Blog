//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZKH.Blog.Model
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class AdminInfo
    {
        public int Id { get; set; }
        public string UName { get; set; }
        public string Pwd { get; set; }
        public short DelFlag { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
        public bool IsSuperUser { get; set; }
    }
}
