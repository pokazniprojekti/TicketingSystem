using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        ProductService productmanager { get; } = new ProductService();
        OrganizationService OrganizationService { get; } = new OrganizationService();
        UserService usermanager { get; } = new UserService();

        // GET: Product
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ID,Name_Prod,Description_Prod,Active,LastModified")] BusinessObjects.Product prod)
        {

            var response = productmanager.Save(prod);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductList");
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


        public ActionResult ProductList()
        {
            var productlist = productmanager.GetProduct();
            return View(productlist);
        }

        public ActionResult EditProduct(long id)
        {

            var prod = productmanager.EditProduct(id);
            if (prod != null)
            {
                return View(prod);
            }

            else
            {
                return RedirectToAction("/ProductList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ID,Name_Prod,Description_Prod,Active,LastModified")] BusinessObjects.Product prod)
        {
            var response = productmanager.SaveEdit(prod);
            if (!response.IsError)
            {
                return RedirectToAction("/ProductList");
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


        public ActionResult DeleteProduct(long id)
        {
            var prod = productmanager.DeleteProduct(id);

            if (prod != null)
            {
                return View(prod);
            }

            else
            {
                return RedirectToAction("/ProductList");
            }
        }


        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var response = productmanager.DeleteConfirmed(id);
            if (!response.IsError)
            {
                return RedirectToAction("/ProductList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var prod = productmanager.DeleteProduct(id);
                return View(prod);
            }
        }

        public ActionResult AssignResponsible()
        {

            ViewBag.ID_User = new SelectList(usermanager.DropDownUser(), "ID", "Email");
            ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignResponsible([Bind(Include = "ID, ID_Product,ID_User,Active,LastModified")] BusinessObjects.Responsible productresponsible)
        {

           var response= productmanager.SaveProductResponsible(productresponsible);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductResponsibleList");
            }

            else
            {

                ViewBag.ID_User = new SelectList(usermanager.DropDownUser(), "ID", "Email");
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod");

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                return View();
            }


        }

        public ActionResult AssignOrganisation()
        {

            ViewBag.ID_Organisation = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name");
            ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignOrganisation([Bind(Include = "ID, ID_product,ID_Organisation,Active,LastModified")] BusinessObjects.ProductOrganisation productorganisation)
        {

            var response = productmanager.SaveProductOrganisation(productorganisation);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductOrganizationList");
            }

            else
            {

                ViewBag.ID_Organisation = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name");
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod");

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                return View();

            }
        }

        public ActionResult ProductResponsibleList()
        {
            var productreslist = productmanager.GetProductResponsibleList();
            return View(productreslist);
        }

        public ActionResult ProductOrganizationList()
        {
            var productorglist = productmanager.GetProductOrganizationList();
            return View(productorglist);
        }

        public ActionResult EditResponsible(int Id)
        {
            var responsible = productmanager.EditResponsible(Id);
            if (responsible != null)
            {
                ViewBag.ID_User = new SelectList(usermanager.DropDownUser(), "ID", "Email", responsible.ID_User);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", responsible.ID_Product);

                return View(responsible);
            }

            else
            {
                return RedirectToAction("/ProductResponsibleList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResponsible([Bind(Include = "ID, ID_Product,ID_User,Active,LastModified")] BusinessObjects.Responsible responsible)
        {

            var response=productmanager.SaveEditResponsible(responsible);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductResponsibleList");
            }

            else
            {

                ViewBag.ID_User = new SelectList(usermanager.DropDownUser(), "ID", "Email", responsible.ID_User);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", responsible.ID_Product);

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return View();
            }


        }

        public ActionResult DeleteResponsible(int id)
        {
            var resp = productmanager.DeleteResponsible(id);
            var responsible = productmanager.EditResponsible(id);

            if (resp != null && responsible != null)
            {
                ViewBag.ID_User = new SelectList(usermanager.DropDownUser(), "ID", "Email", responsible.ID_User);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", responsible.ID_Product);

                return View(resp);
            }

            else
            {
                return RedirectToAction("/ProductResponsibleList");
            }
        }


        [HttpPost, ActionName("DeleteResponsible")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResponsibleConfirmed(int id)
        {
            var response=productmanager.DeleteResponsibleConfirmed(id);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductResponsibleList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var resp = productmanager.DeleteResponsible(id);
                return View(resp);
            }


        }


        public ActionResult EditOrganization(int Id)
        {
            var prodorg = productmanager.EditOrganization(Id);

            if (prodorg != null)
            {
                ViewBag.ID_Organisation = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name", prodorg.ID_Organisation);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", prodorg.ID_Product);

                return View(prodorg);
            }

            else
            {
                return RedirectToAction("/ProductOrganizationList");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrganization([Bind(Include = "ID, ID_Product,ID_Organisation,Active,LastModified")] BusinessObjects.ProductOrganisation prodorg)
        {

            var response=productmanager.SaveEditOrganization(prodorg);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductOrganizationList");
            }

            else
            {
                ViewBag.ID_Organisation = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name", prodorg.ID_Organisation);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", prodorg.ID_Product);

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return View();
            
        }


        }

        public ActionResult DeleteOrganization(int Id)
        {
            var prodorg = productmanager.DeleteOrganization(Id);

            if (prodorg != null)
            {
                ViewBag.ID_Organisation = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name", prodorg.ID_Organisation);
                ViewBag.ID_Product = new SelectList(productmanager.DropDownProduct(), "Id", "Name_Prod", prodorg.ID_Product);

                return View(prodorg);
            }

            else
            {
                return RedirectToAction("/ProductOrganizationList");
            }
        }

        [HttpPost, ActionName("DeleteOrganization")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrganizationConfirmed(int id)
        {
           var response= productmanager.DeleteOrganizationConfirmed(id);

            if (!response.IsError)
            {
                return RedirectToAction("/ProductOrganizationList");
            }

            else

            {


                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var org = productmanager.DeleteOrganizationConfirmed(id);
                return View(org);
            }


        }

    }
}