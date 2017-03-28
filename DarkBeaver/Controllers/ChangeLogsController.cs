using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using DarkBeaver.Managers;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Controllers
{
    [Export("ChangeLogs", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChangeLogsController : Controller
    {
        private ApplicationDbContext db =Statics.db;
        ChangeLogManager chlmngr = Statics.chgMngr;

        // GET: ChangeLogs
        public ActionResult Index(int? releaseid)
        {
          //  var changeLogs = db.ChangeLogs.Include(c => c.Releases);
           // return View(changeLogs.ToList());
            List<ChangeLog> ap = this.chlmngr.GetChangeLogsByReelaseId(releaseid);
            return View(ap);
        }

        // GET: ChangeLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeLog changeLog = this.chlmngr.GetChangeLogsById(id);
            if (changeLog == null)
            {
                return HttpNotFound();
            }
            return View(changeLog);
        }

        // GET: ChangeLogs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.FileReleases, "Id", "Tittle");
            return View();
        }

        // POST: ChangeLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Published")] ChangeLog changeLog)
        {
            if (ModelState.IsValid)
            {
                this.chlmngr.Create(changeLog);
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.FileReleases, "Id", "Tittle", changeLog.Id);
            return View(changeLog);
        }

        // GET: ChangeLogs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeLog changeLog = this.chlmngr.GetChangeLogsById(id);
            if (changeLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.FileReleases, "Id", "Tittle", changeLog.Id);
            return View(changeLog);
        }

        // POST: ChangeLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Published,RowVersion")] ChangeLog changeLog)
        {
            if (ModelState.IsValid)
            {
                this.chlmngr.Edit(changeLog);
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.FileReleases, "Id", "Tittle", changeLog.Id);
            return View(changeLog);
        }

        // GET: ChangeLogs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeLog changeLog = this.chlmngr.GetChangeLogsById(id);
            if (changeLog == null)
            {
                return HttpNotFound();
            }
            return View(changeLog);
        }

        // POST: ChangeLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.chlmngr.Delete(id);
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
