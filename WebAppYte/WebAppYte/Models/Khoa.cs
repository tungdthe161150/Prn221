using System;
using System.Collections.Generic;

namespace WebAppYte.Models
{
    public partial class Khoa
    {
        public Khoa()
        {
            QuanTris = new HashSet<QuanTri>();
        }

        public int Idkhoa { get; set; }
        public string TenKhoa { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<QuanTri> QuanTris { get; set; }
    }
}
