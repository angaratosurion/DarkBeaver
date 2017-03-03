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
    [Export("ProjectNews", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectNewsController : Controller
    {
        ProjectNewsManager mngr = Statics.newMngr;

        // GET: ProjectNews
        public ActionResult Index()
        {
            return View(mngr.List());
        }

        // GET: ProjectNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNews projectNews = mngr.Details(id);
            if (projectNews == null)
            {
                return HttpNotFound();
            }
            return View(projectNews);
        }
        public ActionResult GetNewsByDarkBeaverId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectNews = mngr.ListByProjectId(id);
           
            return View(projectNews.ToList());
        }

        // GET: ProjectNews/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Published,content")] ProjectNews projectNews)
        {
            if (ModelState.IsValid)
            { //this.User.Identity
                
                this.mngr.Create(projectNews, this.User.Identity.Name);

                return RedirectToAction("Index");
            }

            return View(projectNews);
        }

        // GET: ProjectNews/Edit/5
           [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNews projectNews = mngr.Details(id);
            if (projectNews == null)
            {
                return HttpNotFound();
            }
            return View(projectNews);
        }

        // POST: ProjectNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Published,content,RowVersion")] ProjectNews projectNews)
        {
            if (ModelState.IsValid)
            {
                projectNews = mngr.Edit(projectNews);
                return RedirectToAction("Index");
            }
            return View(projectNews);
        }

        // GET: ProjectNews/Delete/5
           [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNews projectNews = mngr.Details(id);
            if (projectNews == null)
            {
                return HttpNotFound();
            }
            return View(projectNews);
        }

        // POST: ProjectNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mngr.Delete(id);
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
