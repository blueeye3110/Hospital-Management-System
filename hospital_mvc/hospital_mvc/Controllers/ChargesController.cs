using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace hospital_mvc.Models
{
    public class ChargesController : Controller
    {
        private Hospital db = new Hospital();

        [Authorize(Roles = "Admin")]
        // GET: Charges
        public ActionResult Index()
        {
            return View(db.Charges.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Charges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        [Authorize(Roles = "Admin")]
        // GET: Charges/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Charges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chargeID,type,amount")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Charges.Add(charge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(charge);
        }

        [Authorize(Roles = "Admin")]
        // GET: Charges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        [Authorize(Roles = "Admin")]
        // POST: Charges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chargeID,type,amount")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(charge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(charge);
        }

        [Authorize(Roles = "Admin")]
        // GET: Charges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            db.Charges.Remove(charge);
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
