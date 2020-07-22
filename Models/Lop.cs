using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOAN_WEBNC.Models
{
    public class Lop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDLop { get; set; }

        [Display(Name = "Tên lớp")]
        [Required(ErrorMessage = "Tên lớp không được để trống")]
        public string TenLop { get; set; }

        public ICollection<HocSinh> HocSinhs { get; set; }
    }
}