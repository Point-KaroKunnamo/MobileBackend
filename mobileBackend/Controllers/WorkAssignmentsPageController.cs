using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mobileBackend.DataAccess;

namespace mobileBackend.Controllers
{
    public class WorkAssignmentsPageController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: WorkAssignmentsPage
        public ActionResult Index()
        {
            var workAssignments = db.WorkAssignments.Include(w => w.Customer);
            return View(workAssignments.ToList());
        }

        // GET: WorkAssignmentsPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignment workAssignment = db.WorkAssignments.Find(id);
            if (workAssignment == null)
            {
                return HttpNotFound();
            }
            return View(workAssignment);
        }

        // GET: WorkAssignmentsPage/Create
        public ActionResult Create()
        {
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName");
            return View();
        }

        // POST: WorkAssignmentsPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_WorkAssignment,id_Customer,Title,Description,Deadline,InProsess,InProgressAt,Completed,CompletedAt,CreatedAt,LastModifiedAt,DeletedAt,Active")] WorkAssignment workAssignment)
        {
            if (ModelState.IsValid)
            {
                db.WorkAssignments.Add(workAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", workAssignment.id_Customer);
            return View(workAssignment);
        }

        // GET: WorkAssignmentsPage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignment workAssignment = db.WorkAssignments.Find(id);
            if (workAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", workAssignment.id_Customer);
            return View(workAssignment);
        }

        // POST: WorkAssignmentsPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_WorkAssignment,id_Customer,Title,Description,Deadline,InProsess,InProgressAt,Completed,CompletedAt,CreatedAt,LastModifiedAt,DeletedAt,Active")] WorkAssignment workAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", workAssignment.id_Customer);
            return View(workAssignment);
        }

        // GET: WorkAssignmentsPage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignment workAssignment = db.WorkAssignments.Find(id);
            if (workAssignment == null)
            {
                return HttpNotFound();
            }
            return View(workAssignment);
        }

        // POST: WorkAssignmentsPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkAssignment workAssignment = db.WorkAssignments.Find(id);
            db.WorkAssignments.Remove(workAssignment);
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
