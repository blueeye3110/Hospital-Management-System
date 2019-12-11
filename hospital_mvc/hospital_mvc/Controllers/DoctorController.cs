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
    public class DoctorController : Controller
    {
        private Hospital hb = new Hospital();

        [Authorize(Roles = "Doctor")]
        // GET: Doctor
        public ActionResult ShowAppointments()
        {
            int doctorId = Convert.ToInt32(Session["userId"]);
            var app = hb.Appointments.Include("Staff").Include("Case").Where(j => j.staffId == doctorId).OrderByDescending(p => p.date).ToList();
            ViewBag.staffId = doctorId;
            return View(app);
        }

        [Authorize(Roles = "Doctor")]
        // GET: Doctor/Edit/5
        public ActionResult EditAppointmentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment app = hb.Appointments.Find(id);

            return View(app);
        }

        [Authorize(Roles = "Doctor")]
        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult EditAppointmentDetails(Appointment app)
        {
            try
            {
                Appointment appo = hb.Appointments.Find(app.appID);
                appo.details = app.details;
                appo.pres = app.pres;

                hb.Entry(appo).State = EntityState.Modified;
                hb.SaveChanges();

                return RedirectToAction("ShowAppointments");
            }
            catch
            {
                ViewBag.Error = "Error while editing appointment details , try again !!!";
                return RedirectToAction("ShowAppointments");
            }
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ShowAppointmentDetails(int? id)
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
            return View(patientApp);
        }

        [Authorize(Roles = "Doctor")]
        // GET: Doctor/Delete/5
        public ActionResult CloseAppointment(int? id)
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
            patientApp.status = "Conducted";

            hb.Entry(patientApp).State = EntityState.Modified;
            hb.SaveChanges();

            return RedirectToAction("ShowAppointments");
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ShowPatients()
        {
            return View(hb.Patients.ToList());
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ShowPatientCase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cases = hb.Cases.Where(j => j.patientId == id).ToList();
            ViewBag.patient = id;
            return View(cases);

        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ViewAppointments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var apps = hb.Appointments.Include("Staff").Where(j => j.caseId == id).OrderBy(j => j.date).ToList();
            return View(apps);
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult ViewAppointmentDetails(int? id, int caseId)
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

        [Authorize(Roles = "Doctor")]
        public ActionResult ShowCases()
        {
            var cases = hb.Cases.Include("Patient").ToList();
            return View(cases);
        }

        [Authorize(Roles = "Doctor")]
        // GET: Reports/Create
        public ActionResult UploadReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            return View( );
        }

        [Authorize(Roles = "Doctor")]
        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadReport(Report report, HttpPostedFileBase files,int caseId)
        {
            if (ModelState.IsValid)
            {
                String FileExt = Path.GetExtension(files.FileName).ToUpper();

                if (FileExt == ".PDF")
                {
                    Case cases = hb.Cases.Find(caseId);
                    string dirpath = @"G:\MVC PROJECT 2019\hospital_mvc\hospital_mvc\App_Data\Reports\"+cases.patientId;
                    if (!Directory.Exists(dirpath))
                    {
                        Directory.CreateDirectory(dirpath);
                    }
                    string path = "~/App_Data/Reports/" + cases.patientId;
                    path = Server.MapPath(path);
                    string fileName = Path.GetFileName(files.FileName);

                    string fullPath = Path.Combine(path, fileName);
                    files.SaveAs(fullPath);

                   
                    report.filePath = fileName;
                    report.date = DateTime.Today;
                    report.caseId = caseId;
                    report.staffId = Convert.ToInt32(Session["userId"]);
                    Staff staff = hb.Staffs.Find(Convert.ToInt32(Session["userId"]));
                    report.uploadedBy = staff.name;


                    hb.Reports.Add(report);
                    hb.SaveChanges();
                    return RedirectToAction("ShowCases");
                }
                else
                {

                    ViewBag.Error = "Please upload report in PDF format";
                    ViewBag.caseId = caseId;
                    return View();
                }

            }

            return View(report);
        }


        [Authorize(Roles = "Doctor")]
        // GET: Doctor/Details/5
        public ActionResult ViewReports(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reports = hb.Reports.Include("Case").Where(j => j.caseId == id).ToList();
            ViewBag.caseId = id;
            return View(reports);
        }

        [Authorize(Roles = "Doctor")]
        // GET: Reports/Details/5
        public ActionResult DownloadReport(int? id , int caseId)
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

        
    }
}
