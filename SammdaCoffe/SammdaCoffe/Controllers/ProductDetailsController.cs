using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SammdaCoffe.Models;

namespace SammdaCoffe.Controllers
{
    public class ProductDetailsController : Controller
    {
        private SammdasCoffeeEntities db = new SammdasCoffeeEntities();

        // GET: ProductDetails
        public ActionResult Index()
        {
            var productDetail = db.ProductDetail.Include(p => p.Product);
            return View(productDetail.ToList());
        }

        public ActionResult Login()
        {
            Response.Redirect("~/Login/login");
            return View();
        }

        //GET: Listar ProductDetails
        public async Task<ActionResult> ListDProducts()
        {
            try
            {
                var query = string.Format("Select Product.productName, Product.productID, ProductDetail.size, ProductDetail.productPrice, ProductDetail.descriptionProduct, ProductDetail.detailID, ProductDetail.productID from ProductDetail inner join Product on ProductDetail.productID = Product.productID");
                var product = db.ProductDetail.SqlQuery(query);

                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(model: await product.ToListAsync());
            }

            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: ProductDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetail.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // GET: ProductDetails/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.Product, "productID", "productName");
            return View();
        }

        // POST: ProductDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "detailID,productID,size,productPrice,descriptionProduct")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.ProductDetail.Add(productDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.Product, "productID", "productName", productDetail.productID);
            return View(productDetail);
        }

        // GET: ProductDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetail.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", productDetail.productID);
            return View(productDetail);
        }

        // POST: ProductDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "detailID,productID,size,productPrice,descriptionProduct")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", productDetail.productID);
            return View(productDetail);
        }

        // GET: ProductDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetail.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // POST: ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductDetail productDetail = db.ProductDetail.Find(id);
            db.ProductDetail.Remove(productDetail);
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
