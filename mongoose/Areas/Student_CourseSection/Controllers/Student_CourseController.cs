using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mongoose.Models;
using Microsoft.AspNet.Identity;

namespace mongoose.Areas.Student_CourseSection.Controllers
{
    public class Student_CourseController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: Student_CourseSection/Student_Course
        public ActionResult Index()
        {
            var student_Course = db.Student_Course.Include(s => s.Cours).Include(s => s.Student);
            ViewBag.Developer = "MB";
            return View(student_Course.ToList());
        }

        // GET: Student_CourseSection/Student_Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Course student_Course = db.Student_Course.Find(id);
            if (student_Course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(student_Course);
        }

        // GET: Student_CourseSection/Student_Course/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName");
            ViewBag.Developer = "MB";
            return View();
        }

        // POST: Student_CourseSection/Student_Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentCourseid,CourseId,StudentId,SemesterCompleted,Grade,Term")] Student_Course student_Course)
        {
            if (ModelState.IsValid)
            {
                db.Student_Course.Add(student_Course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", student_Course.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", student_Course.StudentId);
            return View(student_Course);
        }

        // GET: Student_CourseSection/Student_Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Course student_Course = db.Student_Course.Find(id);
            if (student_Course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", student_Course.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", student_Course.StudentId);
            ViewBag.Developer = "MB";
            return View(student_Course);
        }

        // POST: Student_CourseSection/Student_Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentCourseid,CourseId,StudentId,SemesterCompleted,Grade,Term")] Student_Course student_Course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", student_Course.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", student_Course.StudentId);
            return View(student_Course);
        }

        // GET: Student_CourseSection/Student_Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Course student_Course = db.Student_Course.Find(id);
            if (student_Course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(student_Course);
        }

        // POST: Student_CourseSection/Student_Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Course student_Course = db.Student_Course.Find(id);
            db.Student_Course.Remove(student_Course);
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
