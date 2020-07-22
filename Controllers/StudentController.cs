using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DOAN_WEBNC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DOAN_WEBNC.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public static string TaoMaTuDong(string sMa) //s="17DH110798", sMa = "0798"
        {
            var so = int.Parse(sMa); //"0798" => 798
            so++; //799
            switch (sMa.Length)
            {
                case 1:
                    sMa = string.Format("{0:0}", so); //799 => "799"
                    break;
                case 2:
                    sMa = string.Format("{0:00}", so); //799 => "799"
                    break;
                case 3:
                    sMa = string.Format("{0:000}", so); //799 => "799"
                    break;
                case 4:
                    sMa = string.Format("{0:0000}", so); //799 => "0799"
                    break;
                case 5:
                    sMa = string.Format("{0:00000}", so); //799 => "00799"
                    break;
                case 6:
                    sMa = string.Format("{0:000000}", so); //799 => "000799"
                    break;
                case 7:
                    sMa = string.Format("{0:0000000}", so); //799 => "0000799"
                    break;
                case 8:
                    sMa = string.Format("{0:00000000}", so); //799 => "00000799"
                    break;
                case 9:
                    sMa = string.Format("{0:000000000}", so); //799 => "000000799"
                    break;
            }
            return sMa;  //"17DH11"+"0799"
        }

        // GET: Student
        public ActionResult Index()
        {
            var hocSinhs = db.HocSinhs.Include(h => h.Lop);
            return View(hocSinhs.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.IDLop = new SelectList(db.Lops, "IDLop", "TenLop");
            return View();
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDHocSinh,HoTen,GioiTinh,NgaySinh,DiaChi,Email,IDLop,Image,ImageUpload")] HocSinh hocSinh)
        {
            //
            string MSSV = "";
            var countSt = db.HocSinhs.Count();
            var _begin = DateTime.Now.Year.ToString().Substring(0, 2) + "ST";
            var _end = "";
            if (countSt == 0)
            {
                _end = TaoMaTuDong("100000");
            }
            else
            {
                var lastStudent = db.HocSinhs.ToList().OrderBy(m => m.MSSV).Last();
                _end = TaoMaTuDong(lastStudent.MSSV.Substring(4, lastStudent.MSSV.Length - 4));
            }

            MSSV = _begin + _end;

            if (ModelState.IsValid)
            {
              
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                
                var email = MSSV + "@gmail.com";
                var user = new ApplicationUser { UserName = email, Email = email};
                        

                string userID = user.Id;

                HocSinh hs = new HocSinh();
                hs.IDHocSinh = userID;
                hs.HoTen = hocSinh.HoTen;
                hs.GioiTinh = hocSinh.GioiTinh;
                hs.NgaySinh = hocSinh.NgaySinh;
                hs.DiaChi = hocSinh.DiaChi;
                hs.MSSV = MSSV;
                hs.IDLop = hocSinh.IDLop;
                if (hocSinh.ImageUpload != null)
                {
                    string fileNameImg = Path.GetFileNameWithoutExtension(hocSinh.ImageUpload.FileName);
                    string extension = Path.GetExtension(hocSinh.ImageUpload.FileName);
                    fileNameImg = fileNameImg + extension;
                    hocSinh.Image = "~/Content/Images/" + fileNameImg;
                    hocSinh.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileNameImg));
                    hs.Image = hocSinh.Image;
                }


                ViewBag.IDLop = new SelectList(db.Lops, "IDLop", "TenLop", hocSinh.IDLop);

                db.HocSinhs.Add(hs);
                //try
                //{
                    db.SaveChanges();
                //taoMonHoc cho hs
               
                    var result = await UserManager.CreateAsync(user, "123456@aA");

                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(user.Id, "Student");
                    }
                //}
                //catch (DbUpdateException ex)
                //{
                //    SqlException innerException = ex.InnerException.InnerException as SqlException;
                //    if (innerException != null && innerException.Number == 2601)
                //    {
                //        ModelState.AddModelError("UniqueEmail", "Email này đã tồn tại trong hệ thống. Vui lòng nhập lại Email khác");
                //        return View("Create", hocSinh);
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("UniqueEmail", "Có vẫn đề đã xảy ra khi lưu dữ liệu, try again!");
                //        return View("Create", hocSinh);
                //    }
                //}

                //db.SaveChanges();
                return RedirectToAction("Index");
            }

           
         
            return View(hocSinh);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLop = new SelectList(db.Lops, "IDLop", "TenLop", hocSinh.IDLop);
            return View(hocSinh);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHocSinh,HoTen,GioiTinh,NgaySinh,DiaChi,Email,IDLop,Image")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocSinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLop = new SelectList(db.Lops, "IDLop", "TenLop", hocSinh.IDLop);
            return View(hocSinh);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(hocSinh);
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
