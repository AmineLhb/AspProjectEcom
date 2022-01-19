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
    public class ComplaintsController : Controller
    {
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        // GET: Complaints
        public ActionResult Index()
        {
            var complaints = db.Complaints.Include(c => c.User);
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                ViewBag.Role = user.Role;
            }
            return View(complaints.ToList());
        }

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name");
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }

        // POST: Complaints/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Question)
        {
            Complaint complaint = new Complaint();
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (ModelState.IsValid)
            {
                User user;
                user = (User)Session["user"];
                complaint.User_id = user.Id;
                complaint.Question = Question;
                db.Complaints.Add(complaint);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name", complaint.User_id);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? Id,string Answer)
        {
            Complaint complaint = db.Complaints.Find(Id);
            if (ModelState.IsValid)
            {
                complaint.Answer = Answer;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.User_id = new SelectList(db.Users, "Id", "Name", complaint.User_id);
            return View("Index");
        }

        // GET: Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
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
