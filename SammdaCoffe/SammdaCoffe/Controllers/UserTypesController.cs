using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SammdaCoffe.Models;

namespace SammdaCoffe.Controllers
{
    public class UserTypesController : Controller
    {
        private SammdasCoffeeEntities db = new SammdasCoffeeEntities();

        // GET: UserTypes
        public ActionResult Index()
        {
            var userType = db.UserType.Include(u => u.Rol);
            return View(userType.ToList());
        }

        // GET: UserTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            return View(userType);
        }

        // GET: UserTypes/Create
        public ActionResult Create()
        {
            ViewBag.rolID = new SelectList(db.Rol, "rolID", "rolName");
            return View();
        }

        // POST: UserTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userName,userMail,userPassword,rolID")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                db.UserType.Add(userType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rolID = new SelectList(db.Rol, "rolID", "rolName", userType.rolID);
            return View(userType);
        }

        // GET: UserTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolID = new SelectList(db.Rol, "rolID", "rolName", userType.rolID);
            return View(userType);
        }

        // POST: UserTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userName,userMail,userPassword,rolID")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rolID = new SelectList(db.Rol, "rolID", "rolName", userType.rolID);
            return View(userType);
        }

        // GET: UserTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            return View(userType);
        }

        // POST: UserTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserType userType = db.UserType.Find(id);
            db.UserType.Remove(userType);
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
