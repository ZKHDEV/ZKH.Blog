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
    public partial class AdvertisingInfo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public string Signer { get; set; }
        public string Company { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public short DelFlag { get; set; }
        public System.DateTime DueDate { get; set; }
    }
}
