using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {

        CategoryService categorymanager { get; } = new CategoryService();

        public ActionResult CreateCategory()
        {
            ViewBag.Section = categorymanager.SectionList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "Id,Name,Description,Active, LastModified, Section")] BusinessObjects.Category category)
        {

            var response = categorymanager.Save(category);

            if (!response.IsError)
            {
                return RedirectToAction("/CategoryList");
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


        public ActionResult CategoryList()
        {
            var categorylist = categorymanager.GetCategories();
            
            return View(categorylist);
        }


        public ActionResult EditCategory(int Id)
        {
            
            var category = categorymanager.EditCategory(Id);
            ViewBag.Section = new SelectList(categorymanager.SectionList(), "Value", "Text", category.Section);

            if (category != null)
            {
                return View(category);
            }

            else
            {
                return RedirectToAction("/CategoryList");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "Id,Name,Description,Active, LastModified, Section")] BusinessObjects.Category category)
        {
            var response=categorymanager.SaveEdit(category);
            if (!response.IsError)
            {
                return RedirectToAction("/CategoryList");
            }

            else
            {
                ViewBag.Section = new SelectList(categorymanager.SectionList(), "Value", "Text", category.Section);

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return View();
            }
        }



        public ActionResult DeleteCategory(long id)
        {
            var category = categorymanager.DeleteCategory(id);
            ViewBag.Section = new SelectList(categorymanager.SectionList(), "Value", "Text", category.Section);

            if (category != null)
            {
                return View(category);
            }

            else
            {
                return RedirectToAction("/CategoryList");
            }
        }


        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var response = categorymanager.DeleteConfirmed(id);


            if (!response.IsError)
            {
                return RedirectToAction("/CategoryList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var category = categorymanager.DeleteCategory(id);
                ViewBag.Section = new SelectList(categorymanager.SectionList(), "Value", "Text", category.Section);

                return View(category);
            }
        }



    }
}