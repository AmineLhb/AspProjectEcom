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
    public class FavoritesController : Controller
    {
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        // GET: Favorites
        public ActionResult Index()
        {
            var favorites = db.Favorites.Include(f => f.User);
            return View(favorites.ToList());
        }

        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Favorites/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsFavorite,UserId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", favorite.UserId);
            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", favorite.UserId);
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsFavorite,UserId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", favorite.UserId);
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
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