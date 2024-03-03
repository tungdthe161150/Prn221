using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppYte.Models
{
    public class DangNhap
    {
        [Key]
        [Display(Name = "Tài khoản")]
        public string TaiKhoan { get; set; }
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }
}
