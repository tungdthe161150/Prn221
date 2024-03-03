using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppYte.Models
{
    public partial class QuanTri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuanTri()
        {
            HoiDaps = new HashSet<HoiDap>();
            LichKhams = new HashSet<LichKham>();
        }

        [Key]
        public int IdquanTri { get; set; }

        [StringLength(50)]
        [Display(Name = " Tài khoản")]
        public string TaiKhoan { get; set; }
        [Display(Name = " Mật khảu")]
        [StringLength(20)]
        public string MatKhau { get; set; }

        [Display(Name = " Vai trò")]
        public int? VaiTro { get; set; }

        [Display(Name = " Thông tin bác sĩ")]
        public string ThongTinBacSi { get; set; }

        [StringLength(50)]
        [Display(Name = " Trình độ")]
        public string TrinhDo { get; set; }
        [Display(Name = " Khoa")]
        public int? Idkhoa { get; set; }
        [Display(Name = " Họ tên")]
        public string HoTen { get; set; }

        [StringLength(50)]
        [Display(Name = " Ảnh bìa")]
        public string AnhBia { get; set; }
        [StringLength(100)]
        [Display(Name = " Tài khoản ZOOM")]
        public string ThongtinZoom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoiDap> HoiDaps { get; set; }

        public virtual Khoa IdkhoaNavigation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichKham> LichKhams { get; set; }
    }
}
