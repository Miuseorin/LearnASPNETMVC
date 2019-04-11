using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Expenditure.Models
{
    public class ExpenditureController : Controller
    {
        private ExpenditureEntities db = new ExpenditureEntities();

        // GET: /Expenditure/
        public ActionResult Index()
        {
            var model = db.B.ToList();
            return View(model);
        }

        // GET: /Expenditure/Details/5
        public ActionResult Details(int id)
        {
            var model = db.B.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            B b = db.B.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Expenditure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Expenditure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(B model)
        {
            ValidateExpenditure(model);
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Today;
                db.B.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        private void ValidateExpenditure(B model)
        {
            if (model.Amount <= 0)
                ModelState.AddModelError("Amount", "Số tiền quá ít!");
        }
        // GET: /Expenditure/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            B b = db.B.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        // POST: /Expenditure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Date,Amount,Note")] B b)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }

        // GET: /Expenditure/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            B b = db.B.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        // POST: /Expenditure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            B b = db.B.Find(id);
            db.B.Remove(b);
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
