using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mongoose.Models;

namespace mongoose.Areas.MajorSection
{
    public class MajorsController : Controller
    {
        private InternshipEntities db = new InternshipEntities();

        // GET: MajorSection/Majors
        public ActionResult Index()
        {
            return View(db.Majors.ToList());
            ViewBag.Developer = "MB";
        }

        // GET: MajorSection/Majors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(major);
        }

        // GET: MajorSection/Majors/Create
        public ActionResult Create()
        {
            ViewBag.Developer = "MB";
            return View();
        }

        // POST: MajorSection/Majors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorId,Name,Description,RequiredCredits")] Major major)
        {
            if (ModelState.IsValid)
            {
                db.Majors.Add(major);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(major);
        }

        // GET: MajorSection/Majors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(major);
        }

        // POST: MajorSection/Majors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorId,Name,Description,RequiredCredits")] Major major)
        {
            if (ModelState.IsValid)
            {
                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(major);
        }

        // GET: MajorSection/Majors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.Developer = "MB";
            return View(major);
        }

        // POST: MajorSection/Majors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var intMaj = db.Internship_Major.Where(i => i.MajorId == id);
            var stuMaj = db.Student_Major.Where(s => s.MajorId == id);
            foreach (var m in intMaj) //deletes all internship majors related to Major being deleted mb 
            {
                Internship_Major deleteIntMaj = db.Internship_Major.Find(m.InternshipMajorId);
                db.Internship_Major.Remove(deleteIntMaj);
            }
            foreach (var m in stuMaj) //deletes all student majors related to Major being deleted mb
            {
                Student_Major deleteStuMaj = db.Student_Major.Find(m.StudentMajorId);
                db.Student_Major.Remove(deleteStuMaj);
            }

            Major major = db.Majors.Find(id);
            db.Majors.Remove(major);
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
