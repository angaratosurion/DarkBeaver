using System;
using System.Linq;
using BlackCogs.Data.Models;
using BlackCogs.Managers;
using Microsoft.AspNet.Identity.EntityFramework;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Managers
{
    public class ProjectUserManager:BlackCogsUserManager
    {

        public Boolean UserHasAccessToProject(ApplicationUser user, Project wk, Boolean isDelete)
        {
            try
            {
                Boolean ap = false;
                if (user != null && UserExists(user.UserName.ToString()) == true)
                {
                    if (isDelete == true)
                    {
                        IdentityRole role = this.GetRole(AdminRoles);
                        if (role != null)
                        {
                            if (role.Users.First(x => x.UserId == user.Id) != null && wk.Admininstrator== user.Id)
                            {
                                ap = true;
                            }

                        }
                    }
                    else
                    {
                        if (wk.Admininstrator == user.Id)
                        {
                            ap = true;
                        }
                    }
                }


                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return false;
            }
        }
    }
}
