using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mongoose.Models;

namespace mongoose.Areas.CourseSection.Controllers
{
    public class CoursController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: CourseSection/Cours
        public ActionResult Index()
        {
            ViewBag.Developer = "MB";
            return View(db.Courses.ToList());
        }

        // GET: CourseSection/Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(cours);
        }

        // GET: CourseSection/Cours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseSection/Cours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Name,Description,Department,Credits,Number")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Developer = "MB";
            return View(cours);
        }

        // GET: CourseSection/Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(cours);
        }

        // POST: CourseSection/Cours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Name,Description,Department,Credits,Number")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }

        // GET: CourseSection/Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(cours);
        }

        // POST: CourseSection/Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var stuCor = db.Student_Course.Where(i => i.CourseId == id);
            foreach (var m in stuCor) //deletes all student courses related to Course being deleted
            {
                Student_Course deleteStuCor = db.Student_Course.Find(m.StudentCourseid);
                db.Student_Course.Remove(deleteStuCor);
            }
           
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
