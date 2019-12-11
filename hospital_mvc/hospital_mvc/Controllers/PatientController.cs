using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hospital_mvc.Models;

namespace hospital_mvc.Controllers
{
    public class PatientController : Controller
    {
        private Hospital hb = new Hospital();

        [Authorize(Roles = "Patient")]
        // GET: Case/Create
        public ActionResult OpenNewCase()
        {
            int patientId = Convert.ToInt32(Session["userId"]);
            int count = hb.Cases.Where(j => j.patientId == patientId && j.status == "open").Count();
            if (count < 1)
            {
                Case ca = new Case();
                ca.patientId = patientId;
                ca.opendate = DateTime.Today;
                hb.Cases.Add(ca);
                hb.SaveChanges();
            }
            else if(count > 1)
            {
                ViewBag.Error = "You have already one case opened.";
               
            }

            return RedirectToAction("ShowPatientCase");
        }

        [Authorize(Roles = "Patient")]
        // GET: Case
        public ActionResult ShowPatientCase()
        {
            int patientId = Convert.ToInt32(Session["userId"]);
            var cases= hb.Cases.Where(j => j.patientId == patientId).ToList();
            ViewBag.patientId = patientId;
            return View(cases);
        }

        [Authorize(Roles = "Patient")]
        // GET: Case/Details/5
        public ActionResult BookAppointment(int? id, int patientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var doctor = hb.Staffs.Where(j => j.dept == "Doctor").OrderBy(j => j.type).ToList();
            List<SelectListItem> staffId = new List<SelectListItem>();
            foreach (var d in doctor)
            {
                staffId.Add(new SelectListItem
                {
                    Text = " (" + d.type + ") Dr." + d.name,
                    Value = d.staffId.ToString()
                });
            }
            ViewBag.staff_id = staffId;
            ViewBag.patient = patientId;
            ViewBag.todaydate = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        [Authorize(Roles = "Patient")]
        // POST: Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment([Bind(Include = "date")] Appointment app, int caseId, string staff_Id , int patientId)
        {
            try
            {
                
                int staffId = Convert.ToInt32(staff_Id);
                Staff staff = hb.Staffs.Find(staffId);
                Case cases = hb.Cases.Find(caseId);
                Patient patient = hb.Patients.Find(patientId);

                AddedCharge c = new AddedCharge();
                c.type = "Appointment ( Dr." + staff.name + " - " + staff.type + " )";
                c.amount = staff.appcharge;
                c.caseId = caseId;

                cases.bill += staff.appcharge;
                cases.due += staff.appcharge;

                Appointment book = new Appointment();
                book.date = app.date;
                book.caseId = caseId;
                book.staffId = staffId;
                book.patientname = patient.name;

                hb.Entry(cases).State = EntityState.Modified;
                hb.AddedCharges.Add(c);
                hb.Appointments.Add(book);
                hb.SaveChanges();
                ViewBag.Error = "Appointment Booked.";
                return RedirectToAction("ShowPatientCase");

            }
            catch
            {
                ViewBag.Error = "Error While Creating Appointment , Try Again !!!";
                return RedirectToAction("ShowPatientCase");

            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ViewAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var apps = hb.Appointments.Include("Staff").Where(j => j.caseId == id).OrderBy(j => j.date).ToList();
            return View(apps);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ViewAppointmentDetails(int? id , int caseId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment patientApp = hb.Appointments.Find(id);

            if (patientApp == null)
            {
                return HttpNotFound();
            }
            ViewBag.app = id;
            ViewBag.cases = caseId;
            return View(patientApp);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult DeleteAppointment(int? id , int caseId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment patientApp = hb.Appointments.Find(id);

            if (patientApp == null)
            {
                return HttpNotFound();
            }
            hb.Appointments.Remove(patientApp);
            hb.SaveChanges();

            ViewBag.Error = "Appointment Deleted";
            return RedirectToAction("ViewAppointment", new { id = caseId });
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ShowPatientBill()
        {
            int patientId = Convert.ToInt32(Session["userId"]);
            var cases = hb.Cases.Where(j => j.patientId == patientId).ToList();

            return View(cases);

        }

        [Authorize(Roles = "Patient")]
        // GET: Receiptionist/Create
        public ActionResult ShowAddedCharges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var charges = hb.AddedCharges.Where(j => j.caseId == id).ToList();
            return View(charges);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ShowCases()
        {
            int id = Convert.ToInt32(Session["userId"]);
            var cases = hb.Cases.Where(j => j.patientId == id).ToList();
            ViewBag.patient = id;
            return View(cases);

        }

        [Authorize(Roles = "Patient")]
        // GET: Doctor/Details/5
        public ActionResult ViewReports(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int patientId = Convert.ToInt32(Session["userId"]);
            var reports = hb.Reports.Include("Case").Where(j => j.caseId == id && j.Case.patientId == patientId).ToList();
            ViewBag.caseId = id;
            return View(reports);
        }

        [Authorize(Roles = "Patient")]
        // GET: Reports/Details/5
        public ActionResult DownloadReport(int? id, int caseId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = hb.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            Case cases = hb.Cases.Find(caseId);
            string rootpath = "~/App_Data/Reports/" + cases.patientId;
            rootpath = Server.MapPath(rootpath);
            string path = Path.Combine(rootpath, report.filePath);
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, "application/pdf");

        }

        [Authorize(Roles = "Patient")]
        // GET: Staffs/Details/5
        public ActionResult ViewProfile()
        {
            int id = Convert.ToInt32(Session["userId"]);
            Patient staff = hb.Patients.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        /* public ViewResult Index(string caseId ,string searchString)
        {
            ViewBag.caseId = caseId;
            var doctors = from s in hb.Staffs
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.type.Contains(searchString));
            }
            return View(doctors.ToList()); 
        } */




        /*private static List<SelectListItem> PopulateDoctors()
        {

            Hospital hb = new Hospital();
            var doctor = hb.Staffs.Where(j => j.dept.Equals("Medical")).OrderBy(j => j.type).ToList();
            List<SelectListItem> staffId = new List<SelectListItem>();
            foreach (var d in doctor)
            {
                staffId.Add(new SelectListItem
                {
                    Text = " (" + d.type + ") Dr." + d.name,
                    Value = d.staffId.ToString()
                });
            }

            return staffId;
        }*/


    }
}
