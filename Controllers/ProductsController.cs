using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examen_ASP.Net.Data;
using Examen_ASP.Net.Models;

namespace Examen_ASP.Net.Controllers
{


    public class ProductsController : Controller
    {
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.User);
            return View(products.ToList());
        }
        /*
        // Save images 
        */


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            ViewModel model = new ViewModel();
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                ViewBag.authId = user.Id;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Product = db.Products.Find(id);
            if (model.Product == null)
            {
                return HttpNotFound();
            }
            model.images = db.Images.Where(elt => elt.Product_id == id);
            model.Categories = db.Categories.ToList();
            model.User = db.Users.Find(model.Product.User_id);

            return View(model);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name");
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.

        //public ActionResult Create([Bind(Include = "Id,Title,Description,Discount,Price,Quantity,Address,Status,Category_id,User_id")] Product product)
        //{
        //    string filename;
        //    string path;
        //    Image image = new Image();

        //    if (product.Price < product.Discount) {
        //        ViewBag.Message = "Discount price is beger than Price";

        //        return View("Create");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        User user;
        //        user = (User)Session["user"];
        //        product.User_id = 1;
        //        db.Products.Add(product);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");




        //        //foreach (var file in obj.files)
        //        //{
        //        //    if (file != null)
        //        //    {
        //        //        filename = Path.GetFileName(file.FileName);
        //        //        path = Path.Combine(Server.MapPath("~/uploads/"), filename);
        //        //        file.SaveAs(path);
        //        //        image.Path = path;
        //        //        image.Product_id = product.Id;

        //        //        db.Images.Add(image);
        //        //        db.SaveChanges();
        //        //    }
        //        //}

        //    }

        //    ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name", product.Category_id);
        //    ViewBag.User_id = new SelectList(db.Users, "Id", "Name", product.User_id);
        //    return View(product);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Title, string Description, double Discount, double Price, int Quantity, string Address, bool Status, int Category_id, ImageFile obj)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index");
            }
            string filename;
            string path;
            Image image = new Image();
            Product product = new Product();

            if (Price < Discount)
            {
                ViewBag.Message = "Discount price is beger than Price";

                return View("Create");
            }

            if (ModelState.IsValid)
            {
                User user;
                user = (User)Session["user"];
                product.Title = Title;
                product.Discount = Discount;
                product.Description = Description;
                product.Address = Address;
                product.Status = Status;
                product.Category_id = Category_id;
                product.Price = Price;
                product.Quantity = Quantity;
                product.User_id = user.Id;
                // product.User_id = 1;
                db.Products.Add(product);
                db.SaveChanges();
                int id = db.Products.Count();
                // int id = db.Products.Last().Id;
                var a = db.Products.Where(elt => elt.Title == Title && elt.Description == Description && elt.Address == Address && elt.Status == Status).FirstOrDefault();
                ViewBag.id = a.Id;

                double k = 1;
                foreach (var file in obj.files)
                {
                    DateTime time = DateTime.Now;

                    filename = Path.GetFileName(time.ToString() + k + file.FileName);
                    path = Path.Combine(Server.MapPath("/uploads/"), filename);
                    file.SaveAs(path);
                    image.Path = filename;
                    image.Product = product;
                    db.Images.Add(image);
                    db.SaveChanges();
                    k++;

                }
                return RedirectToAction("Index");

            }

            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name", product.Category_id);
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name", product.User_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name", product.Category_id);
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name", product.User_id);

            return View(product);
        }

        // POST: Products/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string Title, string Description, double Discount, double Price, int Quantity, string Address, bool Status, int Category_id, ImageFile obj)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Index");
            }
            string filename;
            string path;
            Image image = new Image();
            Product item = db.Products.Find(Id);
            Product product = new Product();

            if (Price < Discount)
            {
                ViewBag.Message = "Discount price is beger than Price";

                return View("Create");
            }

            if (ModelState.IsValid)
            {
                product.Title = Title;
                product.Discount = Discount;
                product.Description = Description;
                product.Address = Address;
                product.Status = Status;
                product.Category_id = Category_id;
                product.Price = Price;
                product.Quantity = Quantity;
                if (TryUpdateModel(item, "", (IValueProvider)product))
                {
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                    }
                };

                var a = db.Images.Where(elt => elt.Product_id == product.Id);
                foreach (var img in a)
                {
                    string chemin = Server.MapPath("uploads" + img.Path);
                    FileInfo file = new FileInfo(chemin);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }

                    db.Images.Remove(img);

                }
                db.SaveChanges();


                foreach (var file in obj.files)
                {

                    filename = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("/uploads/"), filename);

                    file.SaveAs(path);
                    image.Path = "/uploads/" + filename;
                    image.Product = product;
                    db.Images.Add(image);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");

            }

            ViewBag.Category_id = new SelectList(db.Categories, "Id", "Name", product.Category_id);
            ViewBag.User_id = new SelectList(db.Users, "Id", "Name", product.User_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();
            var a = db.Images.Where(elt => elt.Product_id == product.Id);
            foreach (var img in a)
            {
                string chemin = Server.MapPath("uploads" + img.Path);
                FileInfo file = new FileInfo(chemin);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }

                db.Images.Remove(img);

            }
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
    public class ImageFile
    {
        public List<HttpPostedFileBase> files { get; set; }
    }
}