using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobOpeningsDemo.Models;

namespace JobOpeningsDemo.Controllers
{
    public class JobsController : Controller
    {
        private DB_test_mainEntities db = new DB_test_mainEntities();

        // GET: Jobs
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        
        public ActionResult Create()
        {
            var job = new Job();
            job.Departments = db.Departments.Select(
                d => new SelectListItem
                {
                    Value = d.id.ToString(),
                    Text = d.title
                }).ToList();
            job.Locations = db.Locations.Select(
                d => new SelectListItem
                {
                    Value = d.id.ToString(),
                    Text = d.title
                }).ToList();

            return View(job);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,title,description,locationId,departmentId,closingDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            job.Departments = db.Departments.Select(d => new SelectListItem
            {
                Value = d.id.ToString(),
                Text = d.title
            }).ToList();
            job.Locations = db.Locations.Select(d => new SelectListItem
            {
                Value = d.id.ToString(),
                Text = d.title
            }).ToList();
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,title,description,locationId,departmentId,closingDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
