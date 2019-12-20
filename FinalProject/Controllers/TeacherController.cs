using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
namespace FinalProject.Controllers
{
    public class TeacherController : Controller
    {
        SchoolManagementEntities1 db = new SchoolManagementEntities1();

        public ActionResult ListOfTeachers()
        {
            return View(db.Teachers.ToList());
        }
        // GET: Teacher
        public ActionResult Index()
        {
            string teacherId = Session["userName"].ToString();
            Teacher model = db.Teachers.Find(teacherId);
            return View(model);
        }

        /**
         * View list of Courses that teacher is assigned to teacher
        **/
        public ActionResult ListCourseTeaching()
        {
            string teacherId = Session["userName"].ToString();
            List<CourseViewModel> result = new List<CourseViewModel>();
            var LinQResult = db.Courses.Where(c => c.teacherId.Equals(teacherId))
                .Select( c => new { SubjectName = c.Subject.subjectName,
                CourseId = c.courseId}).ToList();
            foreach(var item in LinQResult)
            {
                result.Add(new CourseViewModel(item.SubjectName
                    , teacherId, item.CourseId));
            }
            return View(result);
        }

        /**
         * View list of student that enroll a course
        **/ 
        public ActionResult ListStudents(int? id, string subjectName)
        {
            // Check if controller receive courseId
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Make sure teacher manipulate correct List of Students
            string teacherId = Session["userName"].ToString();
            var isCorrectTeacher = db.Courses
                .Where(c => c.teacherId.Equals(teacherId) && c.courseId == id)
                .FirstOrDefault();
            if(isCorrectTeacher == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (subjectName != null) Session["current-subjectName"] = subjectName;
            ViewBag.SubjectName = Session["current-subjectName"].ToString();
            Session["current-courseId"] = id;
            List<StudentViewModel> result = new List<StudentViewModel>();
            var LinQResult = db.Enrollments
                .Where(e => e.courseId == id)
                .Select(e => new {
                    StudentId = e.studentId,
                    StudentName = e.Student.studentName,
                    BirthDate = e.Student.DateofBirth,
                    Grade = e.Grade
                }).OrderBy(e => e.StudentId).ToList();
            foreach (var item in LinQResult)
            {
                result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
            }
            return View(result);
        }
  
        [HttpPost]
        /**
         * Searching + sorting
        **/ 
        //[ValidateAntiForgeryToken]
        public JsonResult Searching(string SearchBy, string SearchValue, string SortBy)
        {
            int id = int.Parse(Session["current-courseId"].ToString());
            List<StudentViewModel> result = new List<StudentViewModel>();
            if (SortBy.Equals(""))
            {
                if (SearchBy.Equals("SearchByStudentName"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.studentName.Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentName).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentId"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                    .Where(e => e.courseId == id && e.Student.studentId.Contains(SearchValue))
                    .Select(e => new
                    {
                        StudentId = e.studentId,
                        StudentName = e.Student.studentName,
                        BirthDate = e.Student.DateofBirth,
                        Grade = e.Grade
                    }).OrderBy(e => e.StudentId).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                     , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentBirthDay"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.DateofBirth.ToString().Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.BirthDate).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Grade == int.Parse(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.Grade).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (SortBy.Equals("SortByStudentName"))
            {
                if (SearchBy.Equals("SearchByStudentName"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.studentName.Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentName).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentId"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                    .Where(e => e.courseId == id && e.Student.studentId.Contains(SearchValue))
                    .Select(e => new
                    {
                        StudentId = e.studentId,
                        StudentName = e.Student.studentName,
                        BirthDate = e.Student.DateofBirth,
                        Grade = e.Grade
                    }).OrderBy(e => e.StudentName).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentBirthDay"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.DateofBirth.ToString().Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentName).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                      , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Grade == int.Parse(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentName).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (SortBy.Equals("SortByStudentId"))
            {
                if (SearchBy.Equals("SearchByStudentName"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.studentName.Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentId).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                     , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentId"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                    .Where(e => e.courseId == id && e.Student.studentId.Contains(SearchValue))
                    .Select(e => new
                    {
                        StudentId = e.studentId,
                        StudentName = e.Student.studentName,
                        BirthDate = e.Student.DateofBirth,
                        Grade = e.Grade
                    }).OrderBy(e => e.StudentId).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentBirthDay"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.DateofBirth.ToString().Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentId).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Grade == int.Parse(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.StudentId).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                                       , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (SortBy.Equals("SortByStudentBirthDay"))
            {
                if (SearchBy.Equals("SearchByStudentName"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.studentName.Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.BirthDate).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentId"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                    .Where(e => e.courseId == id && e.Student.studentId.Contains(SearchValue))
                    .Select(e => new
                    {
                        StudentId = e.studentId,
                        StudentName = e.Student.studentName,
                        BirthDate = e.Student.DateofBirth,
                        Grade = e.Grade
                    }).OrderBy(e => e.BirthDate).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                                         , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentBirthDay"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.DateofBirth.ToString().Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.BirthDate).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Grade == int.Parse(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.BirthDate).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                if (SearchBy.Equals("SearchByStudentName"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.studentName.Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.Grade).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                     , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentId"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                    .Where(e => e.courseId == id && e.Student.studentId.Contains(SearchValue))
                    .Select(e => new
                    {
                        StudentId = e.studentId,
                        StudentName = e.Student.studentName,
                        BirthDate = e.Student.DateofBirth,
                        Grade = e.Grade
                    }).OrderBy(e => e.Grade).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                    , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (SearchBy.Equals("SearchByStudentBirthDay"))
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Student.DateofBirth.ToString().Contains(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.Grade).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                                        , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));
                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        var LinQResult = db.Enrollments
                        .Where(e => e.courseId == id && e.Grade == int.Parse(SearchValue))
                        .Select(e => new
                        {
                            StudentId = e.studentId,
                            StudentName = e.Student.studentName,
                            BirthDate = e.Student.DateofBirth,
                            Grade = e.Grade
                        }).OrderBy(e => e.Grade).ToList();
                        foreach (var item in LinQResult)
                        {
                            result.Add(new StudentViewModel(item.StudentId, item.StudentName
                                                , item.Grade, item.BirthDate.GetValueOrDefault().ToString("dd-MM-yyyy")));

                        }
                    }
                    catch (Exception)
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /**
         * List all documents which have been uploaded by teacher
        **/
        public ActionResult ListDocs(int? id, string subjectName)
        {
            // Check if controller receive courseId
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Make sure teacher manipulate correct List of documents
            string teacherId = Session["userName"].ToString();
            var isCorrectTeacher = db.Courses
                .Where(c => c.teacherId.Equals(teacherId) && c.courseId == id).FirstOrDefault();
            if (isCorrectTeacher == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (subjectName != null) Session["current-subjectName"] = subjectName;
            ViewBag.SubjectName = Session["current-subjectName"].ToString();
            Session["current-courseId"] = id;
            List<DocumentViewModel> result = new List<DocumentViewModel>();
            var LinQResult = db.Documents
                .Where(d => d.courseId == id)
                .Select(e => new {
                    DocId = e.docId,
                    Description = e.description,
                    DocUrl = e.docUrl
                }).ToList();
            foreach (var item in LinQResult)
            {
              result.Add(new DocumentViewModel(item.DocId, id,
               Session["userName"].ToString(), item.DocUrl,item.Description));
            }
            return View(result);
        }

        public ActionResult AddDocs()
        {
            return View();
        }

        /**
         * Adding a new document
        **/
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public JsonResult AddDocs(HttpPostedFileWrapper file, string description)
        {
            if (file != null || description.Equals(""))
            {
                try
                {
                    #region upload file
                    string relativePath = "/UploadedDocuments/" + DateTime.Now.Ticks.ToString() + "_" + file.FileName;
                    string physicalPath = Server.MapPath(relativePath);
                    string DocFolder = Path.GetDirectoryName(physicalPath);
                    if (Directory.Exists(DocFolder)==false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(physicalPath));
                    }
                    file.SaveAs(physicalPath);
                    #endregion

                    #region create DocumentViewModel Object and Document Object
                    DocumentViewModel dvm = new DocumentViewModel();
                    dvm.courseId = int.Parse(Session["current-courseId"].ToString());
                    dvm.teacherId = Session["userName"].ToString();
                    dvm.docUrl = relativePath;
                    dvm.description = description;
                    Document doc = new Document();
                    dvm.UpdateDocument(doc);
                    #endregion
                    db.Documents.Add(doc);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(new { Result = false });
                }
                return Json(new { Result = Session["current-courseId"] });
            }
            else
            {
                return Json(new { Result = false });
            }
        }

        /**
         * Download document
        **/
        public FileResult DownloadDoc(string url)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@url));
            Uri uri = new Uri(Server.MapPath(@url));
            string filename = Path.GetFileName(uri.LocalPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        /**
         * View edit grade page
        **/
        [HttpGet]
        public ActionResult EditGrade(string studentId) 
        {
            if (studentId.Equals(""))
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            int courseId = int.Parse(Session["current-courseId"].ToString());
            var LinQResult = db.Enrollments
                                .Where(e => e.courseId == courseId && e.studentId == studentId)
                                .Select(e => new
                                {
                                    CourseId = e.courseId,
                                    StudentId = e.studentId,
                                    StudentName = e.Student.studentName,
                                    Grade = e.Grade
                                }).FirstOrDefault();

            EnrollmentViewModel evm =
                new EnrollmentViewModel(LinQResult.CourseId, LinQResult.StudentId
                    , LinQResult.Grade, LinQResult.StudentName);
            return View(evm);
        }

        /**
         * Do update grade
        **/
        [HttpPost]
        public JsonResult UpdateGrade(string studentId,int Grade)
        {
            if (studentId.Equals(""))
            { return Json(new { Result = false }); }
            int courseId = int.Parse(Session["current-courseId"].ToString());
            Enrollment enrollment = db.Enrollments
                .FirstOrDefault(e => e.courseId == courseId 
                && e.studentId.Equals(studentId));
            if (enrollment == null)
            {
                return Json(new { Result = false });
            }
            db.Enrollments.Attach(enrollment);
            enrollment.Grade = Grade;
            db.SaveChanges();
            return Json(new { Result = courseId });
        }

        /**
         * Delete a documents
        **/
        [HttpPost]
        public JsonResult DeleteDoc(int? docId)
        {
            if(docId == null) return Json(new { Result = false });
            try
            {
                string teacherId = Session["userName"].ToString();
                var doc = db.Documents
                               .Where(d => d.docId == docId 
                               && d.teacherId.Contains(teacherId)).FirstOrDefault();

                db.Documents.Remove(doc);
                db.SaveChanges();
                return Json(new { Result = Session["current-courseId"] });
            }
            catch (Exception) { return Json(new { Result = false }); }
        }

        [HttpGet]
        public ActionResult EditDoc(int? docId)
        {
            if (docId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string teacherId = Session["userName"].ToString();
            Document doc = db.Documents
               .Where(d => d.docId == docId
               && d.teacherId.Equals(teacherId)).FirstOrDefault();
            if (doc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentViewModel dvm = new DocumentViewModel(doc.docId, doc.courseId,
            doc.teacherId, doc.docUrl, doc.description);

            return View(dvm);

        }

        [HttpPost]
        public JsonResult EditDoc(int? docId, HttpPostedFileWrapper file, string description)
        {
            if (docId == null)
            {
                return Json(new { Result = false });
            }
            string teacherId = Session["userName"].ToString();
            int courseId = int.Parse(Session["current-courseId"].ToString());
            Document document = db.Documents
                                .FirstOrDefault(d => d.courseId == courseId 
                                && d.teacherId.Equals(teacherId)
                                && d.docId == docId);
            if (document == null)
            {
                return Json(new { Result = false });
            }
            if (file != null)
            {
                try
                {
                    #region upload file
                    string relativePath = "/UploadedDocuments/" + DateTime.Now.Ticks.ToString() + "_" + file.FileName;
                    string physicalPath = Server.MapPath(relativePath);
                    string DocFolder = Path.GetDirectoryName(physicalPath);
                    if (Directory.Exists(DocFolder) == false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(physicalPath));
                    }
                    file.SaveAs(physicalPath);
                    #endregion

                    document.docUrl = relativePath;
                }
                catch (Exception)
                {
                    return Json(new { Result = false });
                }
            }
            if (!description.Equals(""))
            {
                document.description = description;
            }
            db.Entry(document).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Result = Session["current-courseId"] });
        }

        //Get: Teacher/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                List<Course> listCourseTeaching = db.Courses
                .Where(c => c.teacherId.Equals(id)).ToList();
                db.SaveChanges();
                foreach (Course c in listCourseTeaching)
                {
                    List<Enrollment> enrollments = db.Enrollments
                        .Where(e => e.courseId == c.courseId).ToList();
                    db.Enrollments.RemoveRange(enrollments);
                    db.SaveChanges();
                    db.Courses.Remove(c);
                    db.SaveChanges();
                }
                Teacher t = db.Teachers.Find(id);
                db.Teachers.Remove(t);
                db.SaveChanges();
                return RedirectToAction("ListOfTeachers");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //Teacher/Create/?
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Teachers.Add(teacher);

                    db.SaveChanges();
                    return RedirectToAction("ListOfTeachers");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            return View(teacher);
        }

        //Teacher/Details/?
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // Teacher/Edit/?
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "teacherId,teacherName,teacherPhone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListOfTeachers");
            }
            return View(teacher);
        }
    }
}