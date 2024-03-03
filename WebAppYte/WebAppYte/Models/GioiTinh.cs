using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class GioiTinh
    {
        public GioiTinh()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        public int IdgioiTinh { get; set; }
        public string GioiTinh1 { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
