using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrganizationController : Controller
    {

        OrganizationService orgmanager { get; } = new OrganizationService();


        // GET: Organization
        public ActionResult CreateOrganization()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrganization([Bind(Include = "Id,Name,Location,Email,TelephoneNumber, LastModified, Active")] BusinessObjects.Organization org)
        {

            var response = orgmanager.Save(org);

            if (!response.IsError)
            {
                return RedirectToAction("/OrganizationList");
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

        public ActionResult OrganizationList()
        {
            var orglist = orgmanager.GetOrganizations();
            return View(orglist);
        }


        public ActionResult EditOrganization(long id)
        {

            var org = orgmanager.EditOrganization(id);

            if (org != null)
            {
                return View(org);
            }

            else
            {
                return RedirectToAction("/OrganizationList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrganization([Bind(Include = "Id,Name,Location,Email,TelephoneNumber, LastModified, Active")] BusinessObjects.Organization org)
        {
            var response=orgmanager.SaveEdit(org);

            if (!response.IsError)
            {
                return RedirectToAction("/OrganizationList");
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



        public ActionResult DeleteOrganization(long id)
        {
            var org = orgmanager.DeleteOrganization(id);

            if (org != null)
            {
                return View(org);
            }

            else
            {
                return RedirectToAction("/OrganizationList");
            }
        }


        [HttpPost, ActionName("DeleteOrganization")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
           
            var response = orgmanager.DeleteConfirmed(id);
            

            if (!response.IsError)
            {
                return RedirectToAction("/OrganizationList");
            }

            else

            {
             

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var org = orgmanager.DeleteOrganization(id);
                return View(org);
            }
        }
    }
}