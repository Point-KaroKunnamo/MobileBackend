using mobileBackend.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimesheetMobile3.Models;

namespace mobileBackend.Controllers
{
    public class WorkAssignmentController : ApiController
    {
        public string[] GetAll()
        {
            string[] assignmentNames = null;
            TimesheetEntities entities = new TimesheetEntities();
            try
            {
                assignmentNames = (from wa in entities.WorkAssignments
                                   where (wa.Active == true)
                                   select wa.Title).ToArray();
            }
            finally
            {
                entities.Dispose();
            }

            return assignmentNames;
        }

        [HttpPost]
        public bool PostStatus(WorkAssignmentOperationModel input)
        {
            TimesheetEntities entities = new TimesheetEntities();
            try
            {
                WorkAssignment assignment = (from wa in entities.WorkAssignments
                                             where (wa.Active == true) &&
                                             (wa.Title == input.AssignmentTitle)
                                             select wa).FirstOrDefault();

                if (assignment == null)
                {
                    return false;
                }
                //string value;
                //decimal number;
                NumberStyles style;
                CultureInfo provider;
                provider = new CultureInfo("fi-FI");
                string valueString = input.Latitude.ToString("R", CultureInfo.InvariantCulture);
                style = NumberStyles.AllowDecimalPoint;
                if (input.Operation == "Start")
                {
                    int assignmentId = assignment.id_WorkAssignment;

                    Timesheet newEntry = new Timesheet()
                    {
                        id_WorkAssignment = assignmentId,
                        StartTime = DateTime.Now,
                        WorkComplete = false,
                        Active = true,
                        CreatedAt = DateTime.Now,
                        Latitude = Convert.ToDecimal(input.Latitude),
                        Longitude = Convert.ToDecimal(input.Longitude)
                   };
                    entities.Timesheets.Add(newEntry);
                }
                else if (input.Operation == "Stop")
                {
                    int assignmentId = assignment.id_WorkAssignment;
                    Timesheet existing = (from ts in entities.Timesheets
                                          where (ts.id_WorkAssignment == assignmentId) &&
                                          (ts.Active == true) && (ts.WorkComplete == false)
                                          orderby ts.StartTime descending
                                          select ts).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.StopTime = DateTime.Now;
                        existing.WorkComplete = true;
                        existing.LastModifiedAt = DateTime.Now;
                    }
                    else
                    {
                        return false;
                    }
                }

                entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            finally
            {
                entities.Dispose();
            }

            return true;
        }
    }
}
