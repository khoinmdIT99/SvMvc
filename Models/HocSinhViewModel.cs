using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN_WEBNC.Models
{
    public class HocSinhViewModel
    {
       
        public int MaBangDiem { get; set; }
        public string MaHocSinh { get; set; }
        public TenLoaiDiem LoaiDiem { get; set; }
        public string TenHocSinh { get; set; }
     
        public int MaMonHoc { get; set; }
    
        public int IDNamHoc { get; set; }
        public string TenMonHoc { get; set; }
        public string NamHoc { get; set; }
    }
}