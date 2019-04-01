using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mobileBackend.DataAccess;

namespace mobileBackend.Controllers
{
    public class TimesheetPageController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: TimesheetPage
        public async Task<ActionResult> Index()
        {
            var timesheets = db.Timesheets.Include(t => t.Contractor).Include(t => t.Customer).Include(t => t.Employee).Include(t => t.WorkAssignment);
            return View(await timesheets.ToListAsync());
        }

        // GET: TimesheetPage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: TimesheetPage/Create
        public ActionResult Create()
        {
            ViewBag.id_Contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName");
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName");
            ViewBag.id_Employee = new SelectList(db.Employees, "id_Employee", "FirstName");
            ViewBag.id_WorkAssignment = new SelectList(db.WorkAssignments, "id_WorkAssignment", "Title");
            return View();
        }

        // POST: TimesheetPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_Timesheet,id_Customer,id_Contractor,id_Employee,id_WorkAssignment,StartTime,StopTime,Comments,CreatedAt,WorkComplete,LastModifiedAt,DeletedAt,Active,Latitude,Longitude")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_Contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", timesheet.id_Contractor);
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", timesheet.id_Customer);
            ViewBag.id_Employee = new SelectList(db.Employees, "id_Employee", "FirstName", timesheet.id_Employee);
            ViewBag.id_WorkAssignment = new SelectList(db.WorkAssignments, "id_WorkAssignment", "Title", timesheet.id_WorkAssignment);
            return View(timesheet);
        }

        // GET: TimesheetPage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", timesheet.id_Contractor);
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", timesheet.id_Customer);
            ViewBag.id_Employee = new SelectList(db.Employees, "id_Employee", "FirstName", timesheet.id_Employee);
            ViewBag.id_WorkAssignment = new SelectList(db.WorkAssignments, "id_WorkAssignment", "Title", timesheet.id_WorkAssignment);
            return View(timesheet);
        }

        // POST: TimesheetPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_Timesheet,id_Customer,id_Contractor,id_Employee,id_WorkAssignment,StartTime,StopTime,Comments,CreatedAt,WorkComplete,LastModifiedAt,DeletedAt,Active,Latitude,Longitude")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_Contractor = new SelectList(db.Contractors, "id_Contractor", "CompanyName", timesheet.id_Contractor);
            ViewBag.id_Customer = new SelectList(db.Customers, "id_Customer", "CustomerName", timesheet.id_Customer);
            ViewBag.id_Employee = new SelectList(db.Employees, "id_Employee", "FirstName", timesheet.id_Employee);
            ViewBag.id_WorkAssignment = new SelectList(db.WorkAssignments, "id_WorkAssignment", "Title", timesheet.id_WorkAssignment);
            return View(timesheet);
        }

        // GET: TimesheetPage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: TimesheetPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            db.Timesheets.Remove(timesheet);
            await db.SaveChangesAsync();
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
