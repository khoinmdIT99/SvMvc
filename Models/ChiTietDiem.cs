using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN_WEBNC.Models
{
    public class ChiTietDiem
    {
        [ForeignKey("DiemHS")]
        [Key, Column(Order = 1)]
        public int MaBangDiem { get; set; }
      
        [Key, Column(Order = 2)]
        public TenLoaiDiem LoaiDiem { get; set; }
        [Key, Column(Order = 3)]
        [Range(1, 3, ErrorMessage = "Lần phải nằm trong khoảng 1 - 3")]
        [Display(Name ="Lần thi")]
        public int LanThi { get; set; }

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Điểm không được phép để trống")]
        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng 0 - 10")]
        public double Diem { get; set; } = 0;

        public DiemHS DiemHS { get; set; }
       
    }
}