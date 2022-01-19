using Examen_ASP.Net.Data;
using Examen_ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Examen_ASP.Net.Controllers
{
    public class UsersController : Controller
    {
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
 
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Password,Phone,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                // if(isEmailExiste(user.Email))
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["user"] = user;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Email already Exist";
                    return View("Create");
                }
            }

            return View(user);

            //db.Users.Add(user);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            // return View(user);
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("login");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password,Phone,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        /*
        // login 
        */
        public ActionResult login()
        {

            return View("Login");
        }
        /*
        // Auth 
        */
        public ActionResult auth(string Email, string Password)
        {
            var user = db.Users.Where(elt => elt.Email == Email && elt.Password == Password).FirstOrDefault();
            if (user == null)
            {
                ViewBag.errorMessage = "Enter a valide user";
                // ViewData["errorMessage"] = "Email not Founded";
                return View("Login");
            }

            Session["user"] = user;
            ViewBag.password = user.Password;
            if (user.Role == "admin")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        /*
        // isAuth() 
        */
        public bool isAuth()
        {
            if (Session["user"] == null)
            {
                return false;
            }
            return true;
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        public ActionResult profile()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("login");
            }
            User user = (User)Session["user"];

            var product = db.Products.Where(elt => elt.User_id == user.Id);
            var msg = db.Messages.Where(elt => elt.Seller_id == user.Id);
            ViewModel model = new ViewModel();
            model.User = user;
            List<Product> prd = new List<Product>();
            List<ProfileInfo> prf = new List<ProfileInfo>();

            foreach (var item in product)
            {
                prd.Add(item);
            }
            foreach (var itm in msg)
            {
                ProfileInfo pr = new ProfileInfo();
                //if (itm.IsRepliyed)
                //{
                //    pr.Text = itm.Text;
                //    var photo = db.Images.First(elt => elt.Product_id == itm.Product_id);
                //    pr.Path = photo.Path;
                //    pr.Buyer_id= (int)itm.Buyer_id;
                //    pr.Id=itm.Id;
                //    var usr = db.Users.Where(elt => elt.Id == itm.Buyer_id).FirstOrDefault();
                //    pr.UserName = usr.Name;
                //    prf.Add(pr);
                //}
                if (itm.Answer == null)
                {
                    pr.Text = itm.Text;
                    var photo = db.Images.First(elt => elt.Product_id == itm.Product_id);
                    pr.Path = photo.Path;
                    pr.Buyer_id = (int)itm.Buyer_id;
                    pr.Id = itm.Id;
                    var usr = db.Users.Where(elt => elt.Id == itm.Buyer_id).FirstOrDefault();
                    pr.UserName = usr.Name;
                    prf.Add(pr);
                }

            }
            model.ProfileInfos = prf;
            model.Products = prd;
            ViewBag.products = product.Count();
            return View(model);

        }
    }
}