using SendMail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_WEBNC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SendMail(FormCollection form)
        {

            string email = form["email"];
            string fullname = form["fullname"];
            string feedbackContent = form["feedbackContent"];
            string body = System.IO.File.ReadAllText(Server.MapPath("~/MailHelper/MailHelper.html"));
            body = body.Replace("{{mail}}", email);
            body = body.Replace("{{name}}", fullname);
            body = body.Replace("{{content}}", feedbackContent);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Feedback", body);
            new MailHelper().SendMail(toEmail, "Feedback từ người dùng", body);
            return RedirectToAction("Index","Home");
        }
    }
}