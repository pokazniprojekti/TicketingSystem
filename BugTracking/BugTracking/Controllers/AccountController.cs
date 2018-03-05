using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BugTracking.Models;
using Common.Logging;
using BugTracking.Business;

namespace BugTracking.Controllers
{



    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {

        OrganizationService OrganizationService { get; } = new OrganizationService();
        RoleService rolemanager { get; } = new RoleService();
        UserService usermanager { get; } = new UserService();

        protected static readonly ILogger Logger = LoggerFactory.GetLogger(LoggingComponent.View);
        private static readonly string ClassName = typeof(AccountController).FullName;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }



        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Logger.TraceMethodStart(ClassName);
            ViewBag.ReturnUrl = returnUrl;
            Logger.TraceMethodEnd(ClassName);
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            Logger.TraceMethodStart(ClassName);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    Logger.TraceMethodEnd(ClassName);
                    ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);

                    if ((UserManager.IsInRole(user.Id, "Admin")))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    if ((UserManager.IsInRole(user.Id, "Technician")))
                    {
                        return RedirectToAction("Index", "Technician");
                    }

                    if ((UserManager.IsInRole(user.Id, "User")))
                    {
                        return RedirectToAction("Index", "RegularUser");
                    }

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    Logger.TraceMethodEnd(ClassName);
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    Logger.TraceMethodEnd(ClassName);
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    Logger.TraceMethodEnd(ClassName);
                    return View(model);
            }
        }

       

        //
        // GET: /Account/Register
        [Authorize]
        public ActionResult Register()
        {
            ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name");
            ViewBag.RoleId = new SelectList(rolemanager.DropDownRole(), "Id", "Name");

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Active=true};
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    usermanager.SaveUser(model);

                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");



                    return RedirectToAction("UserList", "User");
                  
                }
                AddErrors(result);
            }

            ViewBag.OrganizationID = new SelectList(OrganizationService.DropDownOrganisation(), "Id", "Name");
            ViewBag.RoleId = new SelectList(rolemanager.DropDownRole(), "Id", "Name");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

      
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}