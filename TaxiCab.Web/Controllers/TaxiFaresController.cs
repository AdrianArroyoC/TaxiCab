using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiCab.Web.Models;

namespace TaxiCab.Web.Controllers
{
    public class TaxiFaresController : Controller
    {
        private TaxiCabWebContext db = new TaxiCabWebContext();

        // GET: TaxiFares
        public ActionResult Index()
        {
            return View("Index", db.TaxiFares.ToList());
        }

        // GET: TaxiFares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiFare taxiFare = db.TaxiFares.Find(id);
            if (taxiFare == null)
            {
                return HttpNotFound();
            }
            return View("Details", taxiFare);
        }

        // GET: TaxiFares/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: TaxiFares/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time,StartAt,EndAt,MilesLess6mph,TimeLess6mph,TimeInNoMotion,TimeMore6mph,NewYorkTax,Total")] TaxiFare taxiFare)
        {
            if (ModelState.IsValid)
            {
                db.TaxiFares.Add(taxiFare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", taxiFare);
        }

        // GET: TaxiFares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiFare taxiFare = db.TaxiFares.Find(id);
            if (taxiFare == null)
            {
                return HttpNotFound();
            }
            return View("Edit", taxiFare);
        }

        // POST: TaxiFares/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time,StartAt,EndAt,MilesLess6mph,TimeLess6mph,TimeInNoMotion,TimeMore6mph,NewYorkTax,Total")] TaxiFare taxiFare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxiFare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", taxiFare);
        }

        // GET: TaxiFares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxiFare taxiFare = db.TaxiFares.Find(id);
            if (taxiFare == null)
            {
                return HttpNotFound();
            }
            return View("Delete", taxiFare);
        }

        // POST: TaxiFares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxiFare taxiFare = db.TaxiFares.Find(id);
            db.TaxiFares.Remove(taxiFare);
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
