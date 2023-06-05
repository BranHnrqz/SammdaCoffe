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
    public class RecipesController : Controller
    {
        private SammdasCoffeeEntities db = new SammdasCoffeeEntities();

        // GET: Recipes
        public ActionResult Index()
        {
            var recipe = db.Recipe.Include(r => r.Ingredient).Include(r => r.Product);
            return View(recipe.ToList());
        }

        //GET: Listar Recipes
        public async Task<ActionResult> ListRecipes(int? id)
        {
            try
            {
                if (id == 0)
                {
                    return View("error");
                }
                var query = string.Format("Select Ingredient.ingredientName, Product.productName, Recipe.recipeID, Recipe.ingredientID, Recipe.productID from Recipe inner join Ingredient on Recipe.ingredientID = Ingredient.ingredientID inner join Product on Recipe.productID = Product.productID where Recipe.productID = '" + id + "'");
                var recipe = db.Recipe.SqlQuery(query);

                if (recipe == null)
                {
                    return HttpNotFound();
                }
                return View(model: await recipe.ToListAsync());
            }

            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.ingredientID = new SelectList(db.Ingredient, "ingredientID", "ingredientName");
            ViewBag.productID = new SelectList(db.Product, "productID", "productName");
            return View();
        }

        // POST: Recipes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recipeId,ingredientID,productID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipe.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ingredientID = new SelectList(db.Ingredient, "ingredientID", "ingredientName", recipe.ingredientID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", recipe.productID);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ingredientID = new SelectList(db.Ingredient, "ingredientID", "ingredientName", recipe.ingredientID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", recipe.productID);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recipeId,ingredientID,productID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ingredientID = new SelectList(db.Ingredient, "ingredientID", "ingredientName", recipe.ingredientID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", recipe.productID);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipe.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipe.Find(id);
            db.Recipe.Remove(recipe);
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
