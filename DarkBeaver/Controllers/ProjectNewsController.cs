using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using DarkBeaver.Managers;
using DarkBeaver.Data.ViewModels;
using DarkBeaver.Data.Models;

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

            List<ViewProjectNews> vlist = new List<ViewProjectNews>();
            List<ProjectNews> list = mngr.List();
            foreach( var v in list)
            {
                ViewProjectNews vl = new ViewProjectNews();
                vl.ImportFromModel(v);
                vlist.Add(vl);

            }
            return View(vlist);
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
            ViewProjectNews vl = new ViewProjectNews();
            vl.ImportFromModel(projectNews);
          
            return View(vl);
        }
        public ActionResult GetNewsByProjectId(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projectNews = mngr.ListByProjectId(id);
            List<ViewProjectNews> vlist = new List<ViewProjectNews>();
            List<ProjectNews> list = projectNews;
            if (list != null)
            {

            
            foreach (var v in list)
            {
                ViewProjectNews vl = new ViewProjectNews();
                vl.ImportFromModel(v);
                vlist.Add(vl);

            }

        }
            return View(vlist);
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
        public ActionResult Create([Bind(Include = "Id,Title,Published,content")] ViewProjectNews projectNews)
        {
            if (ModelState.IsValid)
            { //this.User.Identity
                
                this.mngr.Create(projectNews.ExportToModel(), this.User.Identity.Name);

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
            ViewProjectNews vl = new ViewProjectNews();
            vl.ImportFromModel(projectNews);

            return View(vl);
        }

        // POST: ProjectNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Published,content,RowVersion")] ViewProjectNews projectNews)
        {
            if (ModelState.IsValid)
            {
                projectNews.ImportFromModel(mngr.Edit(projectNews.ExportToModel()));
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
            ViewProjectNews vl = new ViewProjectNews();
            vl.ImportFromModel(projectNews);

            return View(vl);
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
