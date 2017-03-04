using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DarkBeaver.Managers;
using DarkBeaver.Models;
using DarkBeaver.ViewModels;

namespace DarkBeaver.Controllers
{
    [Export("Projects", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectsController : Controller
    {
        // private ApplicationDbContext db = new ApplicationDbContext();
        DarkBeaverManager mngr = Statics.mngr;

        // GET: DarkBeaver
        public ActionResult Index()
        {
            var prolst = mngr.List();
            List<ViewProject> vproj = new List<ViewProject>();
            foreach( var p in prolst)
            {
                ViewProject vp = new ViewProject();
                vp.ImportFromModel(p);
                vproj.Add(vp);
            }
            return View(vproj);
        }

        // GET: DarkBeaver/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = this.mngr.GetProjectById(Convert.ToInt32(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewProject vproject = new ViewProject();
            vproject.ImportFromModel(project);
            return View(vproject);
        }

        // GET: DarkBeaver/Create
        [Authorize]
        public ActionResult Create()
        {
            //Project proj= new Project();
            return View();
        }

        // POST: DarkBeaver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ViewProject vproject)
        {
            //if (ModelState.IsValid)
            {  //project.Admininstrator = Statics.usrmng.GetUser(this.User.Identity.Name);
                Project project = vproject.ExportToModel();
                this.mngr.Create(project, this.User.Identity.Name);
               // RouteValueDictionary vals = new RouteValueDictionary();
               // vals.Add("newwikiname", project.Name);
               //return RedirectToAction("Create", "HomeWiki",vals);
                return RedirectToAction("Index");
            }

            //return View(project);
        }

        // GET: DarkBeaver/Edit/5
        [Authorize]
        public ActionResult EditBasicInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = this.mngr.GetProjectById(Convert.ToInt32(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewProject vproject = new ViewProject();
            vproject.ImportFromModel(project);
            return View(vproject);
        }
        [Authorize]
        public ActionResult EditProject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = this.mngr.GetProjectById(Convert.ToInt32(id));
            //if (project == null)
            //{
            //    return HttpNotFound();
            //}
            ViewProject vproject = new ViewProject();
            vproject.ImportFromModel(project);
            return View(vproject);
        }
        // POST: DarkBeaver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBasicInfo([Bind(Include = "Id,Name,Description,RowVersion")] ViewProject vproject)
        {
            if (ModelState.IsValid)
            {
                Project project = vproject.ExportToModel();
                this.mngr.Edit(project);
                return RedirectToAction("Index");
            }
            return View(vproject);
        }

        // GET: DarkBeaver/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = this.mngr.GetProjectById(Convert.ToInt32(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewProject vproject = new ViewProject();
            vproject.ImportFromModel(project);
            return View(vproject);
        }

        // POST: DarkBeaver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
                this.mngr.Delete(id, this.User.Identity.Name);
            
            return RedirectToAction("Index");
        }
        public ActionResult GetProjectUsers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = this.mngr.GetProjectById(Convert.ToInt32(id));
            //if (project == null)
            //{
            //    return HttpNotFound();
            //}
            ViewProject vproject = new ViewProject();
            vproject.ImportFromModel(project);
            ViewProjectUsers vpusr = new ViewProjectUsers();
            vpusr.Administrator = vproject.Admininstrator;
            vpusr.Members = vproject.Members;
            vpusr.Project = vproject;
            return View(vpusr);
        }


    }
}
