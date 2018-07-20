using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.Controllers
{
    public class CountyController : Controller
    {
        private readonly DemoDbContext db = DemoDbContext.Create();

        // GET: County
        public ActionResult Index()
        {
            var counties = db?.Counties
                             ?.Include(c => c.Country)
                             .ToArray();
            return View(counties);
        }

        // GET: County/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        // GET: County/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "Code");
            return View();
        }

        // POST: County/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountyId,CountryId")] County county)
        {
            if (ModelState.IsValid)
            {
                county.CountyId = Guid.NewGuid();
                db.Counties.Add(county);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "Code", county.CountryId);
            return View(county);
        }

        // GET: County/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "Code", county.CountryId);
            return View(county);
        }

        // POST: County/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountyId,CountryId")] County county)
        {
            if (ModelState.IsValid)
            {
                db.Entry(county).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Country, "CountryId", "Code", county.CountryId);
            return View(county);
        }

        // GET: County/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        // POST: County/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            County county = db.Counties.Find(id);
            db.Counties.Remove(county);
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
