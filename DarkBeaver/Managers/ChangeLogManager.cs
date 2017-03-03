using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
 
using DarkBeaver.Models;

namespace DarkBeaver.Managers
{
    public class ChangeLogManager
    {
        private   ApplicationDbContext db =Statics.db;
        public List<ChangeLog> GetAllChangeLog()
        {
            try
            {
                List<ChangeLog> ap = null;
                // var changeLogs = db.ChangeLogs.Include(c => c.Releases);
                //   ap = changeLogs.ToList();


                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);


                return null;
            }
        }

        public List<ChangeLog> GetChangeLogsByReelaseId(int? id)
        {
            try
            {
                List<ChangeLog> ap = null;
                var changeLogs = db.ChangeLogs.Where(c => c.Releases.Id == id).ToList();
                ap = changeLogs;


                return ap;

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); return null; }
        }
        public List<ChangeLog> GetChangeLogsByProjectId(int? id)
        {
            try
            {
                List<ChangeLog> ap = null;
               
                ap = db.ChangeLogs.Where(x => x.Project.Id == id).ToList();
               return ap;

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); return null; }
        }
        public ChangeLog GetChangeLogsById(int? id)
        {
            try
            {
                List<ChangeLog> ap = null;
                var changeLogs = db.ChangeLogs.First(c => c.Id == id);
                return changeLogs;//.ToList(); ;


                //return ap;

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); return null; }
        }
        public void Create(ChangeLog changelog)
        {
            try
            {
                if (changelog != null)
                {
                    db.ChangeLogs.Add(changelog);
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }
        public void Edit(ChangeLog changelog)
        {
            try
            {
                if (changelog != null)
                {
                    db.Entry(changelog).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }
        public void Delete(int id)
        {
            try
            {
                ChangeLog changeLog = this.GetChangeLogsById(id);
                if (changeLog != null)
                {
                    db.ChangeLogs.Remove(changeLog);
                    db.SaveChanges();
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }
        public void DeleteByProjectId(int? id)
        {
            try
            {
                List<ChangeLog> changeLog = GetChangeLogsByProjectId(id);
                if (changeLog != null)
                {
                    foreach( var c in changeLog)
                    {
                        this.Delete(c.Id);
                    }
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }

    }
}
