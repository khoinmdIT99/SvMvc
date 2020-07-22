using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOAN_WEBNC.Models
{

   
    public class NamHoc
    {
        [Key]
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDNamHoc { get; set; }      
        [Display(Name ="Năm học")]
        public string TenNamHoc { get; set; }
        [Display(Name ="Bắt đầu")]
        public DateTime StartYear { get; set; }
        [Display(Name ="Kết thúc")]
        public DateTime EndYear { get; set; }

        public ICollection<DiemHS> DiemHS { get; set; }
    }

}