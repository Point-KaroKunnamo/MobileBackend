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
    public class EmployeesPageController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: EmployeesPage
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Contractor);
            return View(employees.ToList());
        }

        // GET: EmployeesPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: EmployeesPage/Create
        public ActionResult Create()
        {
            ViewBag.id_contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName");
            return View();
        }

        // POST: EmployeesPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Employee,id_contractor,FirstName,LastName,PhoneNumber,EmailAdress,CreatedAt,LastModifiedAt,DeletedAt,Active")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", employee.id_contractor);
            return View(employee);
        }

        // GET: EmployeesPage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", employee.id_contractor);
            return View(employee);
        }

        // POST: EmployeesPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Employee,id_contractor,FirstName,LastName,PhoneNumber,EmailAdress,CreatedAt,LastModifiedAt,DeletedAt,Active")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", employee.id_contractor);
            return View(employee);
        }

        // GET: EmployeesPage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeesPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
