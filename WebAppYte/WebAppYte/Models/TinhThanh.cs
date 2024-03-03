using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class TinhThanh
    {
        public TinhThanh()
        {
            NguoiDungs = new HashSet<NguoiDung>();
            TrungTamGanNhats = new HashSet<TrungTamGanNhat>();
        }

        public int Idtinh { get; set; }
        public string TenTinh { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<TrungTamGanNhat> TrungTamGanNhats { get; set; }
    }
}
