using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PriorityController : Controller
    {
        PriorityService prioritymanager { get; } = new PriorityService();
        public ActionResult CreatePriority()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePriority([Bind(Include = "ID,Name,Description,Active,LastModified,TimeIncrementInMinutes")] BusinessObjects.Priority prio)
        {
           var response= prioritymanager.Save(prio);
            if (!response.IsError)
            {
                return RedirectToAction("/PrioritiesList");
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

        public ActionResult PrioritiesList()
        {
            var priolist = prioritymanager.GetPriorities();
            return View(priolist);
        }

        public ActionResult EditPriority(int id)
        {

            var prio = prioritymanager.EditPriority(id);
            if (prio != null)
            {
                return View(prio);
            }

            else
            {
                return RedirectToAction("/PrioritiesList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPriority([Bind(Include = "Id,Name,Description,Active,LastModified,TimeIncrementInMinutes")] BusinessObjects.Priority prio)
        {
           var response= prioritymanager.SaveEdit(prio);
            if (!response.IsError)
            {
                return RedirectToAction("/PrioritiesList");
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

        public ActionResult DeletePriority(int id)
        {
            var prior = prioritymanager.DeletePriority(id);

            if (prior != null)
            {
                return View(prior);
            }

            else
            {
                return RedirectToAction("/PrioritiesList");
            }
        }


        [HttpPost, ActionName("DeletePriority")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var response=prioritymanager.DeleteConfirmed(id);
            if (!response.IsError)
            {
                return RedirectToAction("/PrioritiesList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var prior = prioritymanager.DeletePriority(id);
                return View(prior);
            }
        }

    }
}