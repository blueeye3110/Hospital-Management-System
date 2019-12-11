using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hospital_mvc.Models;


namespace hospital_mvc.Controllers
{
    public class ReceiptionistController : Controller
    {
        private Hospital hb = new Hospital();

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist
        public ActionResult ShowPatients()
        {
            return View(hb.Patients.ToList());
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Details/5
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

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Details/5
        public ActionResult ShowPatientBill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cases = hb.Cases.Where(j => j.patientId == id).ToList();
            ViewBag.patient = id;
            return View(cases);

        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Create
        public ActionResult AddCharge(int? id , int patientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var charges = hb.Charges.ToList();
            List<SelectListItem> chargeId = new List<SelectListItem>();
            foreach (var c in charges)
            {
                chargeId.Add(new SelectListItem
                {
                    Text = c.type + " ( Rs." + c.amount + " )",
                    Value = c.chargeID.ToString()
                });
            }
            ViewBag.charge_Id = chargeId;
            ViewBag.patientId = patientId;
            return View();
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Create
        public ActionResult ShowAddedCharges(int? id, int patientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var charges = hb.AddedCharges.Where(j => j.caseId == id).ToList();
            return View(charges);
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Create
        public ActionResult PayBill(int? id, int patientId , int due)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            ViewBag.patientId = patientId;
            ViewBag.due = due;
            return View();
        }

        [Authorize(Roles = "Reception")]
        // POST: Receiptionist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Create
        public ActionResult AllotWard(int? id , int patientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.caseId = id;
            var rooms = hb.Rooms.ToList();
            List<SelectListItem> roomId = new List<SelectListItem>();
            foreach (var r in rooms)
            {
                if (r.status == "Available")
                {
                    roomId.Add(new SelectListItem
                    {
                        Text = r.type + " ( Rs." + r.charge + " )",
                        Value = r.roomId.ToString()
                    });
                }
            }
            ViewBag.room_Id = roomId;
            ViewBag.patientId = patientId;

            return View();
        }

        [Authorize(Roles = "Reception")]
        // POST: Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCharge(int patientId, int caseId, string charge_Id)
        {
            try
            {
                int chargeId = Convert.ToInt32(charge_Id);
                Charge c = hb.Charges.Find(chargeId);
                Patient patient = hb.Patients.Find(patientId);
                Case cases = hb.Cases.Find(caseId);

                AddedCharge charge = new AddedCharge();
                charge.type = c.type;
                charge.amount = c.amount;
                charge.caseId = caseId;

                cases.bill += c.amount;
                cases.due += c.amount;
                

                hb.AddedCharges.Add(charge);
                
                hb.Entry(cases).State = EntityState.Modified;

                hb.SaveChanges();
                ViewBag.Error = "Case-Id("+caseId+"): "+c.type+" - charge added.";
                return RedirectToAction("ShowPatientBill", new { id = patientId });

            }
            catch
            {
                ViewBag.Error = "Error While Creating Appointment , Try Again !!!";
                return RedirectToAction("AllotWard", new { id = caseId, patientId = patientId });

            }
        }

        [Authorize(Roles = "Reception")]
        // POST: Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayBill(int patientId, int caseId , int amount)
        {
            try
            {
                Patient patient = hb.Patients.Find(patientId);
                Case cases = hb.Cases.Find(caseId);

                AddedCharge charge = new AddedCharge();
                charge.type = "User-Payment";
                charge.amount = amount;
                charge.caseId = caseId;

                cases.paid += amount;
                cases.due -= amount;


                hb.AddedCharges.Add(charge);

                hb.Entry(cases).State = EntityState.Modified;

                hb.SaveChanges();
                ViewBag.Error = "Case-Id(" + caseId + "): Rs." + amount + " Paid.";
                return RedirectToAction("ShowPatientBill", new { id = patientId });

            }
            catch
            {
                ViewBag.Error = "Error While Creating Appointment , Try Again !!!";
                return RedirectToAction("AllotWard", new { id = caseId, patientId = patientId });

            }
        }

        [Authorize(Roles = "Reception")]
        // POST: Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllotWard(int patientId , int caseId, string room_Id)
        {
            try
            {
                int roomId = Convert.ToInt32(room_Id);
                Room room = hb.Rooms.Find(roomId);
                Patient patient = hb.Patients.Find(patientId);
                Case cases = hb.Cases.Find(caseId);

                room.status = "Occupied";
                room.patientName = patient.name;
                room.caseId = caseId.ToString();

                AddedCharge charge = new AddedCharge();
                charge.type = "Room Booking ( " + room.type + " )" ;
                charge.amount = room.charge;
                charge.caseId = caseId;

                cases.bill += room.charge;
                cases.due += room.charge;
                cases.roombooking = "yes";

                hb.AddedCharges.Add(charge);
                hb.Entry(room).State = EntityState.Modified;
                hb.Entry(cases).State = EntityState.Modified;

                hb.SaveChanges();
                ViewBag.Error = "Room Alloted";
                return RedirectToAction("ShowWards");

            }
            catch
            {
                ViewBag.Error = "Error While Creating Appointment , Try Again !!!";
                return RedirectToAction("AllotWard", new { id = caseId , patientId = patientId});

            }
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist
        public ActionResult ShowWards()
        {
            return View(hb.Rooms.ToList());
        }

        [Authorize(Roles = "Reception")]
        public ActionResult ModifyWardStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = hb.Rooms.Find(id);
            room.status = "Available";

            id = Convert.ToInt32(room.caseId);
            Case cases = hb.Cases.Find(id);
            cases.roombooking = "no";

            hb.Entry(room).State = EntityState.Modified;
            hb.Entry(cases).State = EntityState.Modified;

            hb.SaveChanges();
            return RedirectToAction("ShowWards");
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Create
        public ActionResult CloseCase(int? id, int patientId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case cases = hb.Cases.Find(id);
            cases.closedate = DateTime.Today;
            cases.status = "closed";
            hb.Entry(cases).State = EntityState.Modified;
            hb.SaveChanges();
            return RedirectToAction("ShowPatientCase",  new { id = patientId});
        }

        [Authorize(Roles = "Reception")]
        public ActionResult ShowCharges()
        {
            return View(hb.Charges.ToList());
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize(Roles = "Reception")]
        // POST: Receiptionist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Reception")]
        // GET: Receiptionist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "Reception")]
        // POST: Receiptionist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
