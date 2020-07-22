using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOAN_WEBNC.Models
{
    public class HocSinh
    {
        public HocSinh()
         {

            Image = "~/Content/Images/thumbnail.png";
        }

        [Key]
        public string IDHocSinh{get;set;}
        [Required(ErrorMessage = "Tên Học Sinh không được để trống")]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Giới tính không được để trống")]
        [DisplayName("Giới Tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Ngày Sinh")]
        public DateTime NgaySinh { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        //[Column(TypeName = "VARCHAR")]
        //[DataType(DataType.EmailAddress)]
        //[Index(IsUnique = true)]
        public string MSSV { get; set; }

        [Display(Name ="Lớp")]
        [ForeignKey("Lop")]
        public int IDLop { get; set; }
        public Lop Lop { get; set; }

        [Display(Name ="Hình ảnh")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

      
    }
}