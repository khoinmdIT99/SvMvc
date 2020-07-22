using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN_WEBNC.Models;

namespace DOAN_WEBNC.Controllers
{
    public class NamHocController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NamHoc
        public ActionResult Index()
        {
            return View(db.NamHocs.ToList());
        }

        // GET: NamHoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            return View(namHoc);
        }

        // GET: NamHoc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NamHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNamHoc,TenNamHoc,StartYear,EndYear")] NamHoc namHoc)
        {
            if (ModelState.IsValid)
            {
                namHoc.TenNamHoc = namHoc.StartYear.Year.ToString() + " - " + namHoc.EndYear.Year.ToString() + "/ HK1";
                db.NamHocs.Add(namHoc);
                NamHoc namHoc2 = new NamHoc();
                namHoc2.IDNamHoc = namHoc.IDNamHoc;
                namHoc2.TenNamHoc = namHoc.StartYear.Year.ToString() + " - " + namHoc.EndYear.Year.ToString() + "/ HK2";
                namHoc2.StartYear = namHoc.StartYear;
                namHoc2.EndYear = namHoc.EndYear;
                db.NamHocs.Add(namHoc2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(namHoc);
        }

        // GET: NamHoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            return View(namHoc);
        }

        // POST: NamHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNamHoc,TenNamHoc,StartYear,EndYear")] NamHoc namHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(namHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(namHoc);
        }

        // GET: NamHoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            return View(namHoc);
        }

        // POST: NamHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NamHoc namHoc = db.NamHocs.Find(id);
            db.NamHocs.Remove(namHoc);
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
