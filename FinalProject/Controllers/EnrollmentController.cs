using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class EnrollmentController : Controller
    {
        SchoolManagementEntities1 db = new SchoolManagementEntities1();
        // GET: Enrollment
        public ActionResult Index()
        {
            return View(db.Enrollments.ToList());
        }

        //Course/create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(enrollment);
        }
        //Enrollment/Detelte
        public ActionResult Delete(int? cid, string sid)
        {
            if (sid == null && cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Where(e => e.studentId == sid && e.courseId == cid).Select(e => e).FirstOrDefault();
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/?
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string studentId, int courseId)
        {
            //try
            //{
                Enrollment enrollment = db.Enrollments.Find(courseId, studentId);
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //catch (Exception)
            //{
            //    return View("Error");
            //}
        }
    }
}