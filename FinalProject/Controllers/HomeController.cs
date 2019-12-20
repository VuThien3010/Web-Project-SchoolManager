using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        SchoolManagementEntities1 db = new SchoolManagementEntities1();
        public ActionResult Index()
        {
            var subjects = db.Subjects.OrderBy(s => s.subjectId);
            return View(subjects);
        }

        public ActionResult About(int? subjectId)
        {
            ViewBag.Message = "Your application description page.";
            if (subjectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = db.Subjects.Find(subjectId);
            if (subject == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(subject);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}