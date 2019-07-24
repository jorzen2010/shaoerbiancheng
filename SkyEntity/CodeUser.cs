using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkyEntity
{
    public class CodeUser
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "密码")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "姓名")]
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "用户类型")]
        public string UserType { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        




    }
}
