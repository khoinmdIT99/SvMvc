using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using DOAN_WEBNC.Models;
using Microsoft.Ajax.Utilities;

namespace DOAN_WEBNC.Controllers
{
    public class LopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lop
        public ActionResult Index()
        {
            return View(db.Lops.ToList());
        }

        // GET: Lop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            var listHocSinh = db.HocSinhs.Include(x => x.Lop).Where(x => x.Lop.IDLop == id).ToList();           
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(listHocSinh);
        }

        //detal hoc sinh
        public ActionResult DetailStudent(string id)
        {

            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bangDiem = db.DiemHocSinhs.Include(d => d.HocSinh)
                .Include(d => d.MonHoc).Include(d => d.NamHoc)
                .Where(m => m.MaHocSinh == id).OrderBy(m => m.IDNamHoc).ToList();

            var bangDiemHK1 = bangDiem.Where(m => m.NamHoc.TenNamHoc.Substring(m.NamHoc.TenNamHoc.Length -3,3) == "HK1");
            var bangDiemHK2 = bangDiem.Where(m => m.NamHoc.TenNamHoc.Substring(m.NamHoc.TenNamHoc.Length - 3, 3) == "HK2");

            CTDiemTungHSVM cTDiemTungHS = new CTDiemTungHSVM();
            ViewBag.HK1 = bangDiemHK1.First().NamHoc.TenNamHoc;
            ViewBag.HK2 = bangDiemHK2.First().NamHoc.TenNamHoc;
            ViewBag.TenHS = bangDiem.First().HocSinh.HoTen;
            var listDiemHK1 = bangDiemHK1.Select(m => new CTDiemTungHSVM
            {
                MaBangDiem = m.MaBangDiem,
                TenMon = m.MonHoc.TenMonHoc,
                DiemTong = 0
            }).ToList();
            var listDiemHK2 = bangDiemHK2.Select(m => new CTDiemTungHSVM
            {
                MaBangDiem = m.MaBangDiem,
                TenMon = m.MonHoc.TenMonHoc,
                DiemTong = 0
            }).ToList();

            var listDiemCN = bangDiemHK2.Select(m => new DiemCaNamVM
            {
                TenMH = m.MonHoc.TenMonHoc,
                DiemTBCN = 0
            }).ToList();
            double TBHK1 = 0, TBHK2 = 0, TBCN = 0;
            
            foreach (var item in listDiemHK1)
            {
                var chiTietDiem = db.ChiTietDiems.Where(m => m.MaBangDiem == item.MaBangDiem).ToList();
                foreach(var tmp in chiTietDiem)
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
                TBHK1 += item.DiemTong;
            }
            foreach (var item in listDiemHK2)
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
                TBHK2 += item.DiemTong;
            }
            for (int i = 0; i < listDiemCN.Count(); i++)
            {
                if(listDiemCN[i].TenMH == listDiemHK1[i].TenMon && listDiemCN[i].TenMH == listDiemHK2[i].TenMon)
                {
                    listDiemCN[i].DiemTBCN =  Math.Round((listDiemHK1[i].DiemTong + listDiemHK2[i].DiemTong)/2, 1);
                }
                TBCN += listDiemCN[i].DiemTBCN;
            }
            ViewData["DiemHK1"] = listDiemHK1;
            ViewData["DiemHK2"] = listDiemHK2;
            ViewData["DiemCN"] = listDiemCN;
            ViewBag.TBHK1 = Math.Round(TBHK1 / 13, 1);
            ViewBag.TBHK2 = Math.Round(TBHK2 / 13, 1);
            ViewBag.TBCN = Math.Round(TBCN / 13, 1);
            return View();
        }


        // GET: Lop/Create
        public ActionResult Create()
        {
            return View();
        }

       
        // POST: Lop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLop,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Lops.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lop);
        }

        // GET: Lop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLop,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lop);
        }

        // GET: Lop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lop lop = db.Lops.Find(id);
            db.Lops.Remove(lop);
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
