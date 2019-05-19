using ASP_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ASP_MVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult index()
        {
            using (var user = new UserModelContext())
            {
                //List<tblUsers> users = user.tblUsers.Where(a => a.idUser >= 1).ToList();
                return View(user.tblUsers.Where(a => a.bActive == true).ToList());
            }
        } 
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(tblUsers user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new UserModelContext())
                {
                    db.tblUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error al agregar el alumno",ex);
                return View();
            }
        }
        [HttpGet]
        public ActionResult Editar(int idUser)
        {
            using(var db = new UserModelContext())
            {
                //tblUsers user = db.tblUsers.Where(a => a.idUser == idUser).FirstOrDefault();
                tblUsers user = db.tblUsers.Find(idUser);
                return View(user);
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(tblUsers u)
        {
            using(var db = new UserModelContext())
            {
                tblUsers user = db.tblUsers.Find(u.idUser);
                user.vName      = u.vName;
                user.vLastName  = u.vLastName;
                user.bActive    = u.bActive;

                db.SaveChanges();
                return RedirectToAction("index");
            }
        }
        public ActionResult Detalles(int idUser)
        {
            using(var db = new UserModelContext())
            {
                tblUsers user = db.tblUsers.Find(idUser);
                return View(user);
            }
        }

        public ActionResult Eliminar(int idUser)
        {
            using(var db = new UserModelContext())
            {
                var user = db.tblUsers.Find(idUser);
                user.bActive = false;
                //db.tblUsers.Remove(user);
                db.SaveChanges();
                return RedirectToAction("index");

            }
        }
    }
}
