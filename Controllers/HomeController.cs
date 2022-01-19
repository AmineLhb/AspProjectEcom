using Examen_ASP.Net.Data;
using Examen_ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen_ASP.Net.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        private Examen_ASPNetContext db = new Examen_ASPNetContext();

        public ActionResult Index(string search)
        {
            ViewModel model = new ViewModel();
            List<BestOffer> BestOff = new List<BestOffer>();

            var prds = db.Products.OrderByDescending(product => product.Price - product.Discount).Take(3);
            foreach (var prd in prds)
            {
                BestOffer bestOffer = new BestOffer();
                var img = db.Images.Where(elt => elt.Product_id == prd.Id).First();
                bestOffer.Product = prd;

                bestOffer.Images = img;
                BestOff.Add(bestOffer);
            }
            model.BestOffers = BestOff;
            if (!String.IsNullOrEmpty(search))
            {
                model.Products = db.Products.Where(s => s.Title.Contains(search));
            }
            else
            {
                model.Products = db.Products.ToList();
            }
            List<HomeInfo> homes = new List<HomeInfo>();
            var products = db.Products.ToList();
            foreach (var item in products)
            {
                List<Image> imgs = new List<Image>();
                HomeInfo home = new HomeInfo();
                home.Product = item;
                var images = db.Images.Where(s => s.Product_id == item.Id);
                foreach (var t in images)
                {
                    imgs.Add(t);
                }
                home.Images = imgs;

                homes.Add(home);
            }
            model.Categories = db.Categories.ToList();
            model.HomeInfos = homes;
            return View(model);
        }
        /*
        // 
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Seller_id,Buyer_id,Product_id,IsRepliyed,Answer")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Buyer_id = new SelectList(db.Users, "Id", "Name", message.Buyer_id);
            ViewBag.Product_id = new SelectList(db.Products, "Id", "Title", message.Product_id);
            ViewBag.Seller_id = new SelectList(db.Users, "Id", "Name", message.Seller_id);
            return View(message);
        }
        public ActionResult CategorieProducts(int? id)
        {
            ViewModel model = new ViewModel();
            List<HomeInfo> homes = new List<HomeInfo>();
            var products = db.Products.Where(product => product.Category_id == id).ToList();
            foreach (var item in products)
            {
                List<Image> imgs = new List<Image>();
                HomeInfo home = new HomeInfo();
                home.Product = item;
                var images = db.Images.Where(s => s.Product_id == item.Id);
                foreach (var t in images)
                {
                    imgs.Add(t);
                }
                home.Images = imgs;

                homes.Add(home);
            }
            //model.Products = db.Products.Where(product => product.Category_id == id).ToList();
            model.Categories = db.Categories.ToList();
            model.HomeInfos = homes;
            Category category = db.Categories.Find(id);
            ViewBag.category = category.Name;
            //model.Products = db.Products.ToList();


            return View(model);
        }


    }
}