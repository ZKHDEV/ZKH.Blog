using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKH.Blog.Model
{
    [MetadataType(typeof(AdminInfoExtPar))]
    public partial class AdminInfoExt
    {
    }

    public partial class AdminInfoExtPar
    {
        [Required(ErrorMessage = "*必填")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string Folk { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string Kultur { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string Business { get; set; }
        [Required(ErrorMessage = "*必填")]
        [RegularExpression(@"^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$", ErrorMessage = "手机号码格式错误")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*必填")]
        [RegularExpression(@"^[1-9]\d{4,9}$", ErrorMessage = "QQ号码格式错误")]
        public string QQ { get; set; }
        [Required(ErrorMessage = "*必填")]
        [RegularExpression(@"^[A-Za-z0-9]+([._\\-]*[A-Za-z0-9])*@([A-Za-z0-9]+[a-zA-Z0-9]*[a-zA-Z0-9]+.){1,63}[a-zA-Z0-9]+$", ErrorMessage = "邮箱格式错误")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string School { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string BeAware { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string City { get; set; }
        [Required(ErrorMessage = "*必填")]
        public string Country { get; set; }
    }
}
