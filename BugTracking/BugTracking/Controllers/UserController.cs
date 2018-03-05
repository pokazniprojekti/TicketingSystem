using System.Web.Mvc;
using BugTracking.Business;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        UserService usermanager { get; } = new UserService();
        RoleService rolemanager { get; } = new RoleService();
        OrganizationService orgmanager { get; } = new OrganizationService();

        // GET: User
        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var users = usermanager.GetUserList();
            return View(users);
        }

        public ActionResult RoleList()
        {
            var roles = usermanager.GetRoleList();
            return View(roles);
        }



        public ActionResult AssignRole()
        {

            ViewBag.UserId = new SelectList(usermanager.DropDownUser(), "UserId", "Email");
            ViewBag.RoleId = new SelectList(rolemanager.DropDownRole(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole([Bind(Include = "UserId,RoleId,Active,LastModified")] BusinessObjects.AspNetUserRole aspNetUserRole)
        {

            usermanager.SaveUserRole(aspNetUserRole);
            return RedirectToAction("/RoleList");

        }


        //public ActionResult AddInformation()
        //{
        //    ViewBag.OrganizationID = new SelectList(orgmanager.DropDownOrganisation(), "Id", "Name");
        //    ViewBag.Username = new SelectList(usermanager.DropDownAddInfo(), "Username", "Username");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddInformation([Bind(Include = "ID,FirstName,LastName,Email,UserName, Active, LastModified, OrganizationID, TelephoneNumber, UserId")] BusinessObjects.AddInformation addinfo)
        //{

        //    usermanager.SaveAddInfo(addinfo);
        //    return RedirectToAction("/UserList");

        //}

        public ActionResult EditUser(int Id)
        {
            var user = usermanager.EditUser(Id);

            if (user != null)
            {
                ViewBag.OrganizationID = new SelectList(orgmanager.DropDownOrganisation(), "ID", "Name", user.OrganizationID);


                return View(user);
            }

            else
            {
                return RedirectToAction("/UserList");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "ID,FirstName,LastName,Email,UserName, Active, LastModified, OrganizationID, TelephoneNumber, UserId")] BusinessObjects.User user)
        {

            usermanager.SaveEditUser(user);
            return RedirectToAction("/UserList");

        }

        public ActionResult DeleteUser(int Id)
        {
            var user = usermanager.DeleteUser(Id);

            if (user != null)
            {
                ViewBag.OrganizationID = new SelectList(orgmanager.DropDownOrganisation(), "Id", "Name", user.OrganizationID);


                return View(user);
            }

            else
            {
                return RedirectToAction("/UserList");
            }
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            usermanager.DeleteUserConfirmed(id);
            return RedirectToAction("/UserList");
        }


        public ActionResult InactiveUserList()
        {
            var users = usermanager.GetInactiveUserList();
            return View(users);

        }

        public ActionResult ActivateUser(int Id)
        {
            usermanager.ActivateExistingUser(Id);


            return RedirectToAction("/UserList");


        }
    }
}