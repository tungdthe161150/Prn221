﻿using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class LichKham
    {
        public LichKham()
        {
            BenhAns = new HashSet<BenhAn>();
        }

        public int IdlichKham { get; set; }
        public string ChuDe { get; set; }
        public string MoTa { get; set; }
        public DateTime? BatDau { get; set; }
        public DateTime? KetThuc { get; set; }
        public int? TrangThai { get; set; }
        public string ZoomInfo { get; set; }
        public string KetQuaKham { get; set; }
        public int? IdnguoiDung { get; set; }
        public int? IdquanTri { get; set; }

        public virtual NguoiDung IdnguoiDungNavigation { get; set; }
        public virtual QuanTri IdquanTriNavigation { get; set; }
        public virtual ICollection<BenhAn> BenhAns { get; set; }
    }
}
