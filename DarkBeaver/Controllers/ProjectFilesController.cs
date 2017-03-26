using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DarkBeaver.Managers;
using DarkBeaver.Models;

namespace DarkBeaver.Controllers
{
    [Export("ProjectFile", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectFilesController : Controller
    {
       // private ApplicationDbContext db = new ApplicationDbContext();
        ProjectFileManager filmngr=Statics.projfilmngr;
        public ProjectFilesController()
        {
           // filmngr = new ProjectFileManager();
           
        }
       

        // GET: ProjectFiles
        public ActionResult Index(int ?releaseid)
        {
            if (releaseid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(this.filmngr.DetailsByReleaseId(releaseid));
        }
        public ActionResult GetFileByProjectId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(this.filmngr.DetailsByProjectId(id));
        }

        // GET: ProjectFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectFiles projectFiles = this.filmngr.DetailsById(id);
            if (projectFiles == null)
            {
                return HttpNotFound();
            }
            return View(projectFiles);
        }

        // GET: ProjectFiles/Create
        [Authorize]
        public ActionResult Create(int? releaseid)
        {
            return View();
        }

        // POST: ProjectFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReleaseId,FileName,Path")] ProjectFiles projectFiles,HttpPostedFileBase file, int? releaseid)
        {
            if (ModelState.IsValid && file!=null)
            {

                this.filmngr.Create(projectFiles, file);
                return RedirectToAction("Index");
            }

            return View(projectFiles);
        }

        // GET: ProjectFiles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectFiles projectFiles = this.filmngr.DetailsById(id);
            if (projectFiles == null)
            {
                return HttpNotFound();
            }
            return View(projectFiles);
        }

        // POST: ProjectFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReleaseId,FileName,Path,RowVersion")] ProjectFiles projectFiles)
        {
            if (ModelState.IsValid)
            {
                this.filmngr.Edit(projectFiles);
                return RedirectToAction("Index");
            }
            return View(projectFiles);
        }

        // GET: ProjectFiles/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectFiles projectFiles = this.filmngr.DetailsById(id);
            if (projectFiles == null)
            {
                return HttpNotFound();
            }
            return View(projectFiles);
        }

        // POST: ProjectFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectFiles projectFiles = this.filmngr.DetailsById(id);
            this.filmngr.Delete(projectFiles);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
