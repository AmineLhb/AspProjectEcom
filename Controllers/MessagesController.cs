using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examen_ASP.Net.Data;
using Examen_ASP.Net.Models;

namespace Examen_ASP.Net.Controllers
{
    public class MessagesController : Controller
    {
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Buyer).Include(m => m.Seller);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.Buyer_id = new SelectList(db.Users, "Id", "Name");
            ViewBag.Seller_id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Messages/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Seller_id,Buyer_id")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Details","Products");
            }

            ViewBag.Buyer_id = new SelectList(db.Users, "Id", "Name", message.Buyer_id);
            ViewBag.Seller_id = new SelectList(db.Users, "Id", "Name", message.Seller_id);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.Buyer_id = new SelectList(db.Users, "Id", "Name", message.Buyer_id);
            ViewBag.Seller_id = new SelectList(db.Users, "Id", "Name", message.Seller_id);
            return View(message);
        }

        // POST: Messages/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Seller_id,Buyer_id")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Buyer_id = new SelectList(db.Users, "Id", "Name", message.Buyer_id);
            ViewBag.Seller_id = new SelectList(db.Users, "Id", "Name", message.Seller_id);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
