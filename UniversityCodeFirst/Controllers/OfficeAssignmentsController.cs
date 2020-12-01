using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCodeFirst;
using UniversityCodeFirst.Models;

namespace UniversityCodeFirst.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: OfficeAssignments
        public ActionResult Index()
        {
            var officeAssignments = db.OfficeAssignments.Include(o => o.Instructor);
            return View(officeAssignments.ToList().Where(x=>x.Enabled==true));
        }

        // GET: OfficeAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName");
            return View();
        }

        // POST: OfficeAssignments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,Location")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.OfficeAssignments.Add(officeAssignment);
                officeAssignment.Enabled = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,Location")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            officeAssignment.Enabled = false;
            /*db.OfficeAssignments.Remove(officeAssignment);*/
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
