using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class TrungTamGanNhat
    {
        public int IdtrungTam { get; set; }
        public string TenTrungTam { get; set; }
        public string Mota { get; set; }
        public int? Idtinh { get; set; }

        public virtual TinhThanh IdtinhNavigation { get; set; }
    }
}
