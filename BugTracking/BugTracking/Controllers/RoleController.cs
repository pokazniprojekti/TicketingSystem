using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        RoleService rolemanager { get; } = new RoleService();

        // GET: Role
        public ActionResult CreateRole()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole([Bind(Include = "Id,Name,Description,Active,LastModified")] BusinessObjects.AspNetRole role)
        {

            var response = this.rolemanager.Save(role);

            if (!response.IsError)
            {
                return RedirectToAction("/RoleList");
            }

            else
            {
                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                return View();
            }

        }


        public ActionResult RoleList()
        {
            var rolelist=rolemanager.GetRoles();
            return View(rolelist);
        }


        public ActionResult EditRole(string id)
        {
           
            var role = rolemanager.EditRole(id);

            if (role != null)
            {
                return View(role);
            }

            else
            {
                return RedirectToAction("/RoleList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "Id,Name,Description,Active,LastModified")] BusinessObjects.AspNetRole role)
        {
            var response=rolemanager.SaveEdit(role);
            if (!response.IsError)
            {
                return RedirectToAction("/RoleList");
            }

            else
            {
                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return View();
            }
        }


        public ActionResult DeleteRole(string id)
        {
            var role=rolemanager.DeleteRole(id);

            if (role != null)
            {
                return View(role);
            }

            else
            {
                return RedirectToAction("/RoleList");
            }
        }


        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var response=rolemanager.DeleteConfirmed(id);

            if (!response.IsError)
            {
                return RedirectToAction("/RoleList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var role = rolemanager.DeleteRole(id);
                return View(role);
            }
        }

    }
}