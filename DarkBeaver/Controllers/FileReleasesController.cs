using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DarkBeaver.Models;
using DarkBeaver.Managers;
using DarkBeaver.ViewModels;

namespace DarkBeaver.Controllers
{

    [Export("FileReleases", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileReleasesController : Controller
    {
        private ApplicationDbContext db =Statics.db;
        private ReleasesManager relmngr = Statics.relmngr;

        // GET: FileReleases
        public ActionResult Index( int ? projectid)
        {
            //var fileReleases = db.FileReleases.Include(f => f.ChangeLog);

            var fileReleases = this.relmngr.GetAllReleasesByProjectId(projectid);
            List<ViewFileReleases> vrels = new List<ViewFileReleases>();
            foreach( var rel in fileReleases)
            {
                ViewFileReleases vrel = new ViewFileReleases();
                vrel.ImportFromModel(rel);
                vrels.Add(vrel);
            }
            
            return View(vrels);
        }
        public ActionResult GetFileReleasesByProjectId(int? id)
        {
            //var fileReleases = db.FileReleases.Include(f => f.ChangeLog);
            
            var fileReleases = this.relmngr.GetAllReleasesByProjectId(id);
            List<ViewFileReleases> vrels = new List<ViewFileReleases>();
            foreach (var rel in fileReleases)
            {
                ViewFileReleases vrel = new ViewFileReleases();
                vrel.ImportFromModel(rel);
                vrels.Add(vrel);
            }

            return View(vrels);
        }

        // GET: FileReleases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileReleases fileReleases = this.relmngr.GetDetailsById(id);
            if (fileReleases == null)
            {
                return HttpNotFound();
            }
            ViewFileReleases vrel = new ViewFileReleases();
            vrel.ImportFromModel(fileReleases);
            return View(vrel);
        }

        // GET: FileReleases/Create
        [Authorize]
        public ActionResult Create(int projectid)
        {
            ViewBag.Id = new SelectList(db.ChangeLogs, "Id", "Title");
            return View();
        }

        // POST: FileReleases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tittle,Version,Published,content")] ViewFileReleases vfileReleases,int projectid)
        {
            vfileReleases.Project = Statics.mngr.GetProjectById(projectid);
            vfileReleases.Published = DateTime.Now;
            vfileReleases.UploadedBy = Statics.usrmng.GetUser(this.User.Identity.Name);
            
            //if (ModelState.IsValid)
            {
                FileReleases fileReleases = vfileReleases.ExportTomodel();
                this.relmngr.CreateNew(fileReleases);
               
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.ChangeLogs, "Id", "Title", vfileReleases.Id);
            return View(vfileReleases);
        }

        // GET: FileReleases/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileReleases fileReleases = this.relmngr.GetDetailsById(id);
            if (fileReleases == null)
            {
                return HttpNotFound();
            }
            ViewFileReleases vrel = new ViewFileReleases();
            vrel.ImportFromModel(fileReleases);
            ViewBag.Id = new SelectList(db.ChangeLogs, "Id", "Title", vrel.Id);
            return View(vrel);
        }

        // POST: FileReleases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tittle,Version,Published,content,RowVersion")] ViewFileReleases vfileReleases)
        {
            if (ModelState.IsValid)
            {
                FileReleases fileReleases = vfileReleases.ExportTomodel();
                this.relmngr.Edit(fileReleases);
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.ChangeLogs, "Id", "Title", vfileReleases.Id);
            return View(vfileReleases);
        }

        // GET: FileReleases/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileReleases fileReleases = this.relmngr.GetDetailsById(id);
            if (fileReleases == null)
            {
                return HttpNotFound();
            }
            ViewFileReleases vrel = new ViewFileReleases();
            vrel.ImportFromModel(fileReleases);
            return View(vrel);
        }

        // POST: FileReleases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileReleases fileReleases = this.relmngr.GetDetailsById(id);

            this.relmngr.Delete(fileReleases);
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
