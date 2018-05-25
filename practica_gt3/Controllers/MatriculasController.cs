using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using practica_gt3.Models;

namespace practica_gt3.Controllers
{
    [Authorize(Roles = "profesor")]
    public class MatriculasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private int getGrupoClase()
        {
            string currentUserId = User.Identity.GetUserId();
            var grupos = db.AsignacionDocentes.Where(p => p.UsuarioId == currentUserId).ToList();
            if (grupos.Count == 0)
                return -1;
            else return grupos.First().Grupo.Id;
        }

        // GET: Matriculas
        public ActionResult Index()
        {
            int grupo = getGrupoClase();
            var matriculas = db.Matriculas.Include(m => m.Grupo).Include(m => m.Usuario).Where(p => p.GrupoId == grupo).ToList();
            return View(matriculas.ToList());
        }

        // GET: Matriculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matriculas = db.Matriculas.Find(id);
            if (matriculas == null)
            {
                return HttpNotFound();
            }
            return View(matriculas);
        }

        // GET: Matriculas/Create
        public ActionResult Create()
        {
            ViewBag.GrupoId = new SelectList(db.GrupoClases, "Id", "Grupo_Nombre");
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GrupoId,UsuarioId")] Matriculas matriculas)
        {
            if (ModelState.IsValid)
            {
                db.Matriculas.Add(matriculas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoId = new SelectList(db.GrupoClases, "Id", "Grupo_Nombre", matriculas.GrupoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Name", matriculas.UsuarioId);
            return View(matriculas);
        }

        // GET: Matriculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matriculas = db.Matriculas.Find(id);
            if (matriculas == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoId = new SelectList(db.GrupoClases, "Id", "Grupo_Nombre", matriculas.GrupoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Name", matriculas.UsuarioId);
            return View(matriculas);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GrupoId,UsuarioId")] Matriculas matriculas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matriculas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoId = new SelectList(db.GrupoClases, "Id", "Grupo_Nombre", matriculas.GrupoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Name", matriculas.UsuarioId);
            return View(matriculas);
        }

        // GET: Matriculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculas matriculas = db.Matriculas.Find(id);
            if (matriculas == null)
            {
                return HttpNotFound();
            }
            return View(matriculas);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matriculas matriculas = db.Matriculas.Find(id);
            db.Matriculas.Remove(matriculas);
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
