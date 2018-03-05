using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
        StatusService statusmanager { get; } = new StatusService();

        public ActionResult CreateStatus()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStatus([Bind(Include = "ID,Name,Description,Active,LastModified")] BusinessObjects.Status stat)
        {
            
            var response=statusmanager.Save(stat);
            if (!response.IsError)
            {
                return RedirectToAction("/StatusList");
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

        public ActionResult StatusList()
        {
            var statuslist = statusmanager.GetStatus();
            return View(statuslist);
        }

        public ActionResult EditStatus(int id)
        {

            var status = statusmanager.EditStatus(id);

            if (status != null)
            {
                return View(status);
            }

            else
            {
                return RedirectToAction("/StatusList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus([Bind(Include = "ID,Name,Description,Active,LastModified")] BusinessObjects.Status stats)
        {

           var response= statusmanager.SaveEdit(stats);
            if (!response.IsError)
            {
                return RedirectToAction("/StatusList");
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


        public ActionResult DeleteStatus(int id)
        {
            var status = statusmanager.DeleteStatus(id);


            if (status != null)
            {
                return View(status);
            }

            else
            {
                return RedirectToAction("/StatusList");
            }
        }


        [HttpPost, ActionName("DeleteStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var response=statusmanager.DeleteConfirmed(id);
            if (!response.IsError)
            {
                return RedirectToAction("/StatusList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var status = statusmanager.DeleteStatus(id);
                return View(status);
            }
        }



    }
}