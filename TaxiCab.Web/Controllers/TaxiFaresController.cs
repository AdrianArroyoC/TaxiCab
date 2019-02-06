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
            return View(db.TaxiFares.ToList());
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
            return View(taxiFare);
        }

        // GET: TaxiFares/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxiFares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time,StartAt,EndAt,MilesLess6mph,TimeLess6mph,TimeInNoMotion,TimeMore6mph,NewYorkTax,Total")] TaxiFare taxiFare)
        {
            if (ModelState.IsValid)
            {
                taxiFare.Total = Total(taxiFare);
                db.TaxiFares.Add(taxiFare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxiFare);
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
            return View(taxiFare);
        }

        // POST: TaxiFares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time,StartAt,EndAt,MilesLess6mph,TimeLess6mph,TimeInNoMotion,TimeMore6mph,NewYorkTax,Total")] TaxiFare taxiFare)
        {
            if (ModelState.IsValid)
            {
                taxiFare.Total = Total(taxiFare);
                db.Entry(taxiFare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxiFare);
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
            return View(taxiFare);
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

        private float Total(TaxiFare taxiFare)
        {
            float total = 3.0f;
            int timeCovered = taxiFare.TimeInNoMotion + taxiFare.TimeMore6mph;
            TimeSpan startTime = taxiFare.Time;
            TimeSpan endTime = EndTime(startTime, +taxiFare.TimeLess6mph + timeCovered);
            if (!taxiFare.Date.DayOfWeek.Equals(DayOfWeek.Saturday) && !taxiFare.Date.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                if (IntoHours(startTime, endTime, new TimeSpan(16, 0, 0), new TimeSpan(20, 0, 0)))
                {
                    total += 1.0f;
                }
            }
            if (IntoHours(startTime, endTime, new TimeSpan(20, 0, 0), new TimeSpan(23, 59, 0)))
            {
                total += 0.5f;
            }
            else if (IntoHours(startTime, endTime, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)))
            {
                total += 0.5f;
            }
            total += (taxiFare.MilesLess6mph * 5 * 0.35f);
            total += (timeCovered * 0.35f);
            if (taxiFare.NewYorkTax)
            {
                total += 0.5f;
            }
            return total;
        }

        private TimeSpan EndTime(TimeSpan startTime, int time)
        {
            int hours = startTime.Hours;
            int minutes = 0;
            if (time > 60)
            {
                hours += (time / 60);
                minutes = time % 60;
            }
            else
            {
                minutes = time;
            }
            minutes += startTime.Minutes;
            if (minutes > 60)
            {
                hours++;
                minutes %= 60;
            }
            if (hours > 23)
            {
                hours -= 24;
            }
            return new TimeSpan(hours, minutes, 0);
        }

        private bool IntoHours(TimeSpan startTime, TimeSpan endTime, TimeSpan startLimit, TimeSpan endLimit)
        {
            bool into = false;
            if (startTime >= startLimit && startTime <= endLimit)
            {
                into = true;
            }
            if (endTime >= startLimit && endTime <= endLimit)
            {
                into = true;
            }
            return into;
        }
    }
}
