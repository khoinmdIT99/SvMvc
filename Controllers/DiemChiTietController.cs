using DOAN_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace DOAN_WEBNC.Controllers
{
    public class DiemChiTietController : Controller
    {
        // GET: DiemChiTiet
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var chiTietList = db.DiemHocSinhs.Include(d => d.HocSinh).Include(d => d.MonHoc).Include(d => d.NamHoc).ToList();

            HocSinhViewModel hs = new HocSinhViewModel();

          
            List<HocSinhViewModel> hsViewModelList = chiTietList.Select(x => new HocSinhViewModel
                {
                    MaBangDiem = x.MaBangDiem,
                    MaHocSinh = x.MaHocSinh,
                    TenHocSinh = x.HocSinh.HoTen,
                    MaMonHoc = x.MaMonHoc,
                    IDNamHoc = x.IDNamHoc,
                    TenMonHoc=x.MonHoc.TenMonHoc,
                    NamHoc=x.NamHoc.TenNamHoc,
                   
                }
                ).ToList();

            return View(hsViewModelList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var chiTietDiem = db.ChiTietDiems.Where(x=> x.MaBangDiem==id).ToList();
            var detail = db.DiemHocSinhs.FirstOrDefault(m => m.MaBangDiem == id);
            ViewBag.TenMH = db.MonHocs.Find(detail.MaMonHoc).TenMonHoc;
            ViewBag.TenHS = db.HocSinhs.Find(detail.MaHocSinh).HoTen;
            ViewBag.MaHocSinh = db.HocSinhs.Find(detail.MaHocSinh).IDHocSinh;
            ViewBag.MaBangDiem = detail.MaBangDiem;
            if (chiTietDiem == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDiem);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chiTietDiem = db.ChiTietDiems.Where(x => x.MaBangDiem == id).ToList();
            var detail = db.DiemHocSinhs.FirstOrDefault(m => m.MaBangDiem == id);
            ViewBag.TenMH = db.MonHocs.Find(detail.MaMonHoc).TenMonHoc;
            ViewBag.TenHS = db.HocSinhs.Find(detail.MaHocSinh).HoTen;
            if (chiTietDiem == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDiem);
        }
        [HttpPost]
        public ActionResult EditDiem(FormCollection form, int? id)
        {

            if (ModelState.IsValid)
            {
                var bangDiem = db.ChiTietDiems.Where(m => m.MaBangDiem == id).ToList();
                foreach (var item in bangDiem)
                {
                    switch (item.LoaiDiem)
                    {
                        case TenLoaiDiem.Loai1:
                            {
                                item.Diem = Convert.ToDouble(form["Diem_Loai1_1"]);

                                break;
                            }
                        case TenLoaiDiem.Loai2:
                            {
                                if (item.LanThi == 1) item.Diem = Convert.ToDouble(form["Diem_Loai2_1"]);
                                if (item.LanThi == 2) item.Diem = Convert.ToDouble(form["Diem_Loai2_2"]);
                                if (item.LanThi == 3) item.Diem = Convert.ToDouble(form["Diem_Loai2_3"]);
                                break;
                            }
                        case TenLoaiDiem.Loai3:
                            {
                                if (item.LanThi == 1) item.Diem = Convert.ToDouble(form["Diem_Loai3_1"]);
                                if (item.LanThi == 2) item.Diem = Convert.ToDouble(form["Diem_Loai3_2"]);
                                break;
                            }
                        case TenLoaiDiem.THI:
                            {
                                item.Diem = Convert.ToDouble(form["Diem_THI_1"]);
                                break;
                            }
                    }
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Details", "DiemChiTiet", new { id = bangDiem.First().MaBangDiem });
            }
            return View();
           
        }
       
    }
}