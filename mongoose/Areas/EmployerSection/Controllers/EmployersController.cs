﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mongoose.Models;
using Microsoft.AspNet.Identity;

namespace mongoose.Areas.EmployerSection.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployersController : Controller
    {
        private InternshipAppEntities db = new InternshipAppEntities();

        // GET: EmployerSection/Employers

        public ActionResult Index()
        {
            var employers = db.Employers.Include(e => e.AspNetUser);
            return View(employers.ToList());
        }

        // GET: EmployerSection/Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // GET: EmployerSection/Employers/Create
        public ActionResult Create()
        {
            new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: EmployerSection/Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployerId,Name,ContactName,Phone,Email,Address1,Address2,City,State,Zipcode,Id")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                var UserId = User.Identity.GetUserId();
                employer.Id = UserId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", employer.Id);
            return View(employer);
        }

        // GET: EmployerSection/Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", employer.Id);
            return View(employer);
        }

        // POST: EmployerSection/Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployerId,Name,ContactName,Phone,Email,Address1,Address2,City,State,Zipcode,Id")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", employer.Id);
            return View(employer);
        }

        // GET: EmployerSection/Employers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // POST: EmployerSection/Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employer employer = db.Employers.Find(id);
            db.Employers.Remove(employer);
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
