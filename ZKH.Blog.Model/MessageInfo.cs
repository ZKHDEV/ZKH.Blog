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
    public partial class MessageInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MessageInfo()
        {
            this.ReplyInfo = new HashSet<ReplyInfo>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public System.DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyInfo> ReplyInfo { get; set; }
    }
}
