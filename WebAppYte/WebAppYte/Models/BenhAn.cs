using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class BenhAn
    {
        public int IdbenhAn { get; set; }
        public string KetQua { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string BacSyKham { get; set; }
        public string DonThuoc { get; set; }
        public string GhiChu { get; set; }
        public int? IdnguoiDung { get; set; }
        public int? IdlichKham { get; set; }

        public virtual LichKham IdlichKhamNavigation { get; set; }
        public virtual NguoiDung IdnguoiDungNavigation { get; set; }
    }
}
