using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN_WEBNC.Models
{
    public class LopViewModel
    {
        public int IDLop { get; set; }
        public string TenLop { get; set; }
        public string IDHocSinh { get; set; }
        public string TenHocSinh { get; set; }

        public int MaBangDiem { get; set; }
        public string TenMonHoc { get; set; }
        public int IDnamHoc { get; set; }
        public string TenNamHoc { get; set; }
        public TenLoaiDiem LoaiDiem { get; set; }
        public int LanThi { get; set; }
        public double Diem { get; set; }

    }
}