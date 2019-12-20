using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class StudentController : Controller
    {
        SchoolManagementEntities1 db = new SchoolManagementEntities1();
        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(student);
        }

        //Student/Details/?
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // Student/Edit/?
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,studentName,DateofBirth,studentPhone")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(student);
        }

        //Get: Student/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Enrollments.Remove(db.Enrollments.Where(e => e.studentId == student.studentId).FirstOrDefault());
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Home()
        {
            string id = Session["userName"].ToString();
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult List()
        {
            List<Course> courses = db.Courses.ToList();
            List<Subject> subjects = db.Subjects.ToList();
            List<Enrollment> enrollments = db.Enrollments.ToList();
            List<Student> students = db.Students.ToList();
            int type = int.Parse(Session["userType"].ToString());
            if (type != 2)
            {
                return View("Error");
            }
            string studentId = Session["userName"].ToString();
            var courseRecord = from c in courses
                               join s in subjects on c.subjectId equals s.subjectId into table1
                               from s in table1.ToList()
                               join e in enrollments on c.courseId equals e.courseId into table2
                               from e in table2.ToList()
                               join st in students on e.studentId equals st.studentId into table3
                               from st in table3.ToList()

                               select new ViewModel
                               {
                                   course = c,
                                   subject = s,
                                   enrollment = e,
                                   student = st
                               };
            return View(courseRecord.Where(d => d.student.studentId == studentId));
        }

        public ActionResult ListDocs(int? courseId, string studentId)
        {
            // Check if controller receive courseId
            if (courseId == null || studentId.Equals(""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Make sure studdent has correct List of documents
            var isCorrectStudent = db.Enrollments
     .Where(e => e.studentId.Equals(studentId) && e.courseId==courseId
).FirstOrDefault();
            if (isCorrectStudent == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["current-courseId"] = courseId;
            List<Document> LinQResult = db.Documents
                .Where(d => d.courseId == courseId).ToList();
            return View(LinQResult);
        }

        public FileResult DownloadDoc(string url)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@url));
            Uri uri = new Uri(Server.MapPath(@url));
            string filename = Path.GetFileName(uri.LocalPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.studentId = Session["userName"].ToString();
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Home");
            }
            return View(enrollment);
        }
    }
}