using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracking.Business;
using BugTracking.Business.BusinessServices;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "User")]
    public class RegularUserController : Controller
    {

        ProductService productmanager { get; } = new ProductService();
        OrganizationService OrganizationService { get; } = new OrganizationService();
        UserService usermanager { get; } = new UserService();
        CategoryService categorymanager { get; } = new CategoryService();
        PriorityService prioritymanager { get; } = new PriorityService();
        StatusService statusmanager { get; } = new StatusService();
        TicketService ticketmanager { get; } = new TicketService();

        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TicketList()
        {
            ViewData["ProductID"] = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod");
            ViewData["OrganizationID"] = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name");
            ViewData["CategoryID"] = new SelectList(categorymanager.DropDownCategory(), "ID", "Name");
            ViewData["PriorityID"] = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name");
            ViewData["StatusID"] = new SelectList(statusmanager.DropDownStatus(), "ID", "Name");
            var ticketList = ticketmanager.GetRegularUserList();
            return View(ticketList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult TicketList([Bind(Include = "ID,UserID,CategoryID,PriorityID,StatusID,DateCreated,Deadline,Subject,Active,LastModified,AssigneeID,ProductID,Body,OrganizationID")] BusinessObjects.Ticket search)
        {
            ViewData["ProductID"] = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod");
            ViewData["OrganizationID"] = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name");
            ViewData["CategoryID"] = new SelectList(categorymanager.DropDownCategory(), "ID", "Name");
            ViewData["PriorityID"] = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name");
            ViewData["StatusID"] = new SelectList(statusmanager.DropDownStatus(), "ID", "Name");
            var ticketList = ticketmanager.GetFilteredRegularUserTicketList(search);
            return View(ticketList);
        }


        public ActionResult NewTicket()
        {
            ViewBag.AssigneeID = new SelectList(usermanager.DropDownUser(), "ID", "Email");
            ViewBag.ProductID = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod");
            ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(categorymanager.DropDownCategory(), "ID", "Name");
            ViewBag.PriorityID = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name");
            ViewBag.StatusID = new SelectList(statusmanager.DropDownStatus(), "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewTicket([Bind(Include = "UserID,CategoryID,PriorityID,StatusID,DateCreated,Deadline,Subject,Active,LastModified,AssigneeID,ProductID,Body,OrganizationID")] BusinessObjects.Ticket ticket)
        {

            var response = ticketmanager.Save(ticket);

            if (!response.IsError)
            {
                return RedirectToAction("/TicketList");
            }

            else
            {
                ViewBag.AssigneeID = new SelectList(usermanager.DropDownUser(), "ID", "Email");
                ViewBag.ProductID = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod");
                ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name");
                ViewBag.CategoryID = new SelectList(categorymanager.DropDownCategory(), "ID", "Name");
                ViewBag.PriorityID = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name");
                ViewBag.StatusID = new SelectList(statusmanager.DropDownStatus(), "ID", "Name");

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                return View();
            }
        }

        public ActionResult EditTicket(long Id)
        {
            var ticket = ticketmanager.EditTicket(Id);
            if (ticket != null)
            {
                ViewBag.AssigneeID = new SelectList(usermanager.DropDownUser(), "ID", "Email", ticket.AssigneeID);
                ViewBag.ProductID = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod", ticket.ProductID);
                ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name", ticket.OrganizationID);
                ViewBag.CategoryID = new SelectList(categorymanager.DropDownCategory(), "ID", "Name", ticket.CategoryID);
                ViewBag.PriorityID = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name", ticket.PriorityID);
                ViewBag.StatusID = new SelectList(statusmanager.DropDownStatus(), "ID", "Name", ticket.StatusID);

                return View(ticket);
            }

            else
            {
                return RedirectToAction("/TicketList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicket([Bind(Include = "ID,UserID,CategoryID,PriorityID,StatusID,DateCreated,Deadline,Subject,Active,LastModified,AssigneeID,ProductID,Body,OrganizationID")] BusinessObjects.Ticket ticket)
        {

            var response = ticketmanager.SaveEdit(ticket);

            if (!response.IsError)
            {
                return RedirectToAction("/TicketList");
            }

            else
            {
                ViewBag.AssigneeID = new SelectList(usermanager.DropDownUser(), "ID", "Email", ticket.AssigneeID);
                ViewBag.ProductID = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod", ticket.ProductID);
                ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name", ticket.OrganizationID);
                ViewBag.CategoryID = new SelectList(categorymanager.DropDownCategory(), "ID", "Name", ticket.CategoryID);
                ViewBag.PriorityID = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name", ticket.PriorityID);
                ViewBag.StatusID = new SelectList(statusmanager.DropDownStatus(), "ID", "Name", ticket.StatusID);

                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return View();
            }


        }

        public ActionResult DeleteTicket(long id)
        {
            var ticket = ticketmanager.DeleteTicket(id);

            if (ticket != null)
            {
                ViewBag.AssigneeID = new SelectList(usermanager.DropDownUser(), "ID", "Email", ticket.AssigneeID);
                ViewBag.ProductID = new SelectList(productmanager.DropDownProduct(), "ID", "Name_Prod", ticket.ProductID);
                ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "ID", "Name", ticket.OrganizationID);
                ViewBag.CategoryID = new SelectList(categorymanager.DropDownCategory(), "ID", "Name", ticket.CategoryID);
                ViewBag.PriorityID = new SelectList(prioritymanager.DropDownPriority(), "ID", "Name", ticket.PriorityID);
                ViewBag.StatusID = new SelectList(statusmanager.DropDownStatus(), "ID", "Name", ticket.StatusID);
                return View(ticket);
            }

            else
            {
                return RedirectToAction("/TicketList");
            }
        }

        [HttpPost, ActionName("DeleteTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTicketConfirmed(long id)
        {
            var response = ticketmanager.DeleteTicketConfirmed(id);
            if (!response.IsError)
            {
                return RedirectToAction("/TicketList");
            }

            else

            {
                foreach (var failer in response.ValidationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                var ticket = ticketmanager.DeleteTicket(id);
                return View(ticket);
            }
        }

    }
}