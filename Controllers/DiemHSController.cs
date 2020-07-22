using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN_WEBNC.Models;

namespace DOAN_WEBNC.Controllers
{
    public class DiemHSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DiemHS
        public ActionResult Index()
        {
            var diemHocSinhs = db.DiemHocSinhs.Include(d => d.HocSinh).Include(d => d.MonHoc).Include(d => d.NamHoc);
            return View(diemHocSinhs.ToList());
        }

        // GET: DiemHS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHS diemHS = db.DiemHocSinhs.Find(id);
            if (diemHS == null)
            {
                return HttpNotFound();
            }
            return View(diemHS);
        }

        // GET: DiemHS/Create
        public ActionResult Create()
        {
            ViewBag.MaHocSinh = new SelectList(db.HocSinhs, "IDHocSinh", "HoTen");
            ViewBag.IDNamHoc = new SelectList(db.NamHocs, "IDNamHoc", "TenNamHoc");
            return View();
        }

        // POST: DiemHS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBangDiem,MaHocSinh,IDNamHoc")] DiemHS diemHS)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                
                var mh = db.MonHocs.ToList();
                foreach(var i in mh)
                {
                    DiemHS diem = new DiemHS();
                    diem.MaMonHoc = i.IDMonHoc;
                    diem.IDNamHoc = diemHS.IDNamHoc;
                    diem.MaHocSinh = diemHS.MaHocSinh;

                    db.DiemHocSinhs.Add(diem);
                    
                }
                db.SaveChanges();
                //tao bang diem chi tiet cho tung mon
                var bangDiem = db.DiemHocSinhs.Where(m => m.MaHocSinh == diemHS.MaHocSinh && m.IDNamHoc == diemHS.IDNamHoc).ToList();
                foreach (var item in bangDiem)
                {
                    ChiTietDiem ctDIem = new ChiTietDiem();
                    ctDIem.LoaiDiem = TenLoaiDiem.Loai1;
                    ctDIem.LanThi = 1;
                    ctDIem.Diem = 0;
                    ctDIem.MaBangDiem = item.MaBangDiem;
                    db.ChiTietDiems.Add(ctDIem);

                    for (int i = 0; i < 3; i++)
                    {
                        ChiTietDiem ctDIem2 = new ChiTietDiem();
                        ctDIem2.LoaiDiem = TenLoaiDiem.Loai2;
                        ctDIem2.LanThi = i + 1;
                        ctDIem2.Diem = 0;
                        ctDIem2.MaBangDiem = item.MaBangDiem;
                        db.ChiTietDiems.Add(ctDIem2);
                    }

                    for (int i = 0; i < 2; i++) 
                    {
                        ChiTietDiem ctDIem3 = new ChiTietDiem();
                        ctDIem3.LoaiDiem = TenLoaiDiem.Loai3;
                        ctDIem3.LanThi = i + 1;
                        ctDIem3.Diem = 0;
                        ctDIem3.MaBangDiem = item.MaBangDiem;
                        db.ChiTietDiems.Add(ctDIem3);
                    }
                    ChiTietDiem ctDIem4 = new ChiTietDiem();
                    ctDIem4.LoaiDiem = TenLoaiDiem.THI;
                    ctDIem4.LanThi = 1;
                    ctDIem4.Diem = 0;
                    ctDIem4.MaBangDiem = item.MaBangDiem;
                    db.ChiTietDiems.Add(ctDIem4);

                }
               
                    db.SaveChanges();
                return RedirectToAction("Index");
                //var diemMieng = new ChiTietDiem(
                    
                //    );
                //}
                //catch (DbUpdateException ex)
                //{
                //    SqlException innerException = ex.InnerException.InnerException as SqlException;
                //    if (innerException != null && innerException.Number == 2601)
                //    {
                //        ModelState.AddModelError("UniqueHocKy", "Học kỳ {0} này đã tồn tại trong hệ thống. Vui lòng nhập lại Email khác");
                //        return View("Create", diemHS);
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("UniqueHocKy", "Có vẫn đề đã xảy ra khi lưu dữ liệu, try again!");
                //        return View("Create", diemHS);
                //    }
                //}


            }

            ViewBag.MaHocSinh = new SelectList(db.HocSinhs, "IDHocSinh", "HoTen", diemHS.MaHocSinh);
            ViewBag.MaMonHoc = new SelectList(db.MonHocs, "IDMonHoc", "TenMonHoc", diemHS.MaMonHoc);
            ViewBag.IDNamHoc = new SelectList(db.NamHocs, "IDNamHoc", "TenNamHoc", diemHS.IDNamHoc);
            return View(diemHS);
        }

        // GET: DiemHS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHS diemHS = db.DiemHocSinhs.Find(id);
            if (diemHS == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHocSinh = new SelectList(db.HocSinhs, "IDHocSinh", "HoTen", diemHS.MaHocSinh);
            ViewBag.MaMonHoc = new SelectList(db.MonHocs, "IDMonHoc", "TenMonHoc", diemHS.MaMonHoc);
            ViewBag.IDNamHoc = new SelectList(db.NamHocs, "IDNamHoc", "TenNamHoc", diemHS.IDNamHoc);
            return View(diemHS);
        }

        // POST: DiemHS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBangDiem,MaHocSinh,MaMonHoc,IDNamHoc")] DiemHS diemHS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemHS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHocSinh = new SelectList(db.HocSinhs, "IDHocSinh", "HoTen", diemHS.MaHocSinh);
            ViewBag.MaMonHoc = new SelectList(db.MonHocs, "IDMonHoc", "TenMonHoc", diemHS.MaMonHoc);
            ViewBag.IDNamHoc = new SelectList(db.NamHocs, "IDNamHoc", "TenNamHoc", diemHS.IDNamHoc);
            return View(diemHS);
        }

        // GET: DiemHS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHS diemHS = db.DiemHocSinhs.Find(id);
            if (diemHS == null)
            {
                return HttpNotFound();
            }
            return View(diemHS);
        }

        // POST: DiemHS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemHS diemHS = db.DiemHocSinhs.Find(id);
            db.DiemHocSinhs.Remove(diemHS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
