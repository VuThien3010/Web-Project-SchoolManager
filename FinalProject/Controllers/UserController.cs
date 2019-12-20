using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        SchoolManagementEntities1 db = new SchoolManagementEntities1();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            if (username != null || password != null)
            {
                User user = db.Users.FirstOrDefault(u => u.userName == username &&
                u.userPassword == password);
                if (user != null)
                {
                    int type = (int) user.userType;
                    
                    Session["userName"] = user.userName;
                    Session["userType"] = type;
                    return Json(new { Result = type });
                }
                else
                {
                    return Json(new { Result = false });
                }
            }
            return Json(new { Result = false });
        }

        public ActionResult Logout()
        {
            Session["userName"] = null;
            Session["userType"] = null;
            Session["current-courseId"] = null;
            Session["current-subjectName"] = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            string username = Session["userName"].ToString();
            if (username.Equals(""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(string oldPass, string confirmPass)
        {
            if (oldPass.Equals("") || confirmPass.Equals(""))
            {
                return Json(new { Result = false, Message="Do not leave empty !" });
            }
            string username = Session["userName"].ToString();
            int type = int.Parse(Session["userType"].ToString());
            var user = db.Users
                .Where( u => u.userName.Equals(username)
                && u.userPassword.Equals(oldPass) && u.userType == type).FirstOrDefault();
            if(user == null)
            {
                return Json(new { Result = false, Message = "Password can not be found !" });
            }
            try
            {
                db.Users.Attach(user);
                user.userPassword = confirmPass;
                db.SaveChanges();
                Session["userName"] = null;
                Session["userType"] = null;
                return Json(new { Result = true });
            }
            catch (Exception)
            {
                return Json(new { Result = false, Message = "Error !" });
            }

        }

        // Users/Edit/?
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userName,userPassword,userType")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //User/Details/?
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //Get: USer/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(u => u.userName == id).Select(u => u).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Message = string.Format("Please check whenever you have REMOVE Admin/Teacher/Student account in THOSE PAGE or NOT");
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            User user = db.Users.Where(u => u.userName == id).Select(u => u).FirstOrDefault();
            if (user.Student != null)
            {
                ViewBag.Message = string.Format("Please! Delete Student account first");
                return View("Delete", user);

            }
            if (user.Teacher != null)
            {
                ViewBag.Message = string.Format("Please! Delete Teacher account first");
                return View("Delete", user);
            }
            if (user.Admin != null)
            {
                ViewBag.Message = string.Format("Please! Delete Admin account first");
                return View("Delete", user);
            }

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //User/Create/?
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                    catch (Exception)
                {
                    return View("Error");
                }
        }
            return View(user);
        }
    }


}