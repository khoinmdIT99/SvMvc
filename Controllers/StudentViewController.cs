using DOAN_WEBNC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace DOAN_WEBNC.Controllers
{
    public class StudentViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: StudentView
        public ActionResult Index()
        {
            string userID = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if(userIdClaim != null)
                {
                    userID = userIdClaim.Value;
                }
            }
            var student = db.HocSinhs.Include(m => m.Lop).FirstOrDefault(x => x.IDHocSinh == userID);
            
            if (student != null)
            {
                return View(student);
            }
            else return HttpNotFound();
        }
        public PartialViewResult DetailStudent()
        {
            string id = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            var bangDiem = db.DiemHocSinhs.Include(d => d.HocSinh)
                .Include(d => d.MonHoc).Include(d => d.NamHoc)
                .Where(m => m.MaHocSinh == id).ToList();
            CTDiemTungHSVM cTDiemTungHS = new CTDiemTungHSVM();
            ViewBag.NamHoc = bangDiem.First().NamHoc.TenNamHoc;
            ViewBag.TenHS = bangDiem.First().HocSinh.HoTen;
            var listDiem = bangDiem.Select(m => new CTDiemTungHSVM
            {
                MaBangDiem = m.MaBangDiem,
                TenMon = m.MonHoc.TenMonHoc,
                DiemTong = 0
            }).ToList();
            foreach (var item in listDiem)
            {
                var chiTietDiem = db.ChiTietDiems.Where(m => m.MaBangDiem == item.MaBangDiem).ToList();
                foreach (var tmp in chiTietDiem)
                {
                    switch (tmp.LoaiDiem)
                    {
                        case TenLoaiDiem.Loai1:
                            {
                                item.DiemTong += tmp.Diem;

                                break;
                            }
                        case TenLoaiDiem.Loai2:
                            {
                                double temp = 0;
                                if (tmp.LanThi == 1) { temp += tmp.Diem; }
                                if (tmp.LanThi == 2) { temp += tmp.Diem; }
                                if (tmp.LanThi == 3) { temp += tmp.Diem; }
                                item.DiemTong += temp;
                                break;
                            }
                        case TenLoaiDiem.Loai3:
                            {
                                double temp = 0;
                                if (tmp.LanThi == 1) { temp += tmp.Diem * 2; }
                                if (tmp.LanThi == 2) { temp += tmp.Diem * 2; }
                                item.DiemTong += temp;
                                break;
                            }
                        case TenLoaiDiem.THI:
                            {
                                item.DiemTong += tmp.Diem * 3;
                                item.DiemTong = Math.Round(item.DiemTong = item.DiemTong / 11, 2);
                                break;
                            }

                    }

                }
            }
            return PartialView(listDiem);
        }
    }
}