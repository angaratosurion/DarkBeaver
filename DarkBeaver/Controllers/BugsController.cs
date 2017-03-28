using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using DarkBeaver.Managers;
using DarkBeaver.Data.ViewModels;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Controllers
{
    [System.ComponentModel.Composition.Export("Bugs", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BugsController : Controller
    {
        private ApplicationDbContext db =Statics.db;
        BugManager bugmngr = Statics.bugmngr;
        // GET: Bugs
        public ActionResult Index(int? projectid)
        {
            try
            {
                var bugs = bugmngr.BugsByProjectId(projectid);
                List<ViewBugs> vbs = new List<ViewBugs>();
                foreach( var b in bugs)
                {
                    ViewBugs vb = new ViewBugs();
                    vb.ImportFromModel(b);
                    vbs.Add(vb);
                }



            return View(vbs);
        }
             catch (Exception)
            {

                throw;
            }
        }

        // GET: Bugs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bugs bugs = this.bugmngr.BugById(id);
            if (bugs == null)
            {
                return HttpNotFound();
            }
            ViewBugs vb = new ViewBugs();
            vb.ImportFromModel(bugs);
            return View(vb);
        }

        // GET: Bugs/Create
        [Authorize]
        public ActionResult Create(int? projectid)
        {
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ReporedAt,EditedAt")] ViewBugs vbugs, int projectid)
        {
            vbugs.EditedAt = DateTime.Now;
            vbugs.EditedBy = Statics.usrmng.GetUser(this.User.Identity.Name);
            vbugs.ReportedBy = vbugs.EditedBy;
            vbugs.ReporedAt = vbugs.EditedAt;
            vbugs.Project = Statics.mngr.GetProjectById(projectid);
           
           // if (ModelState.IsValid)
            {
                Bugs bugs = vbugs.ExportTomodel();
                this.bugmngr.Create(bugs);
                return RedirectToAction("Index");
            }

            return View(vbugs);
        }

        // GET: Bugs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bugs bugs = this.bugmngr.BugById(id);
            if (bugs == null)
            {
                return HttpNotFound();
            }
            ViewBugs vb = new ViewBugs();
            vb.ImportFromModel(bugs);
            return View(vb);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ReporedAt,EditedAt,RowVersion")] ViewBugs vbugs)
        {
            if (ModelState.IsValid)
            {
                Bugs bugs = vbugs.ExportTomodel();
                this.bugmngr.Edit(bugs);
                return RedirectToAction("Index");
            }
            return View(vbugs);
        }

        // GET: Bugs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bugs bugs = this.bugmngr.BugById(id);
            if (bugs == null)
            {
                return HttpNotFound();
            }
            ViewBugs vbugs = new ViewBugs();
            vbugs.ImportFromModel(bugs);
            return View(vbugs);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bugs bugs = this.bugmngr.BugById(id);
            this.bugmngr.Delete(bugs);
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
