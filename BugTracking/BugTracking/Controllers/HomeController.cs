using System.Web.Mvc;
using Common.Logging;

namespace BugTracking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        protected static readonly ILogger Logger = LoggerFactory.GetLogger(LoggingComponent.View);
        private static readonly string ClassName = typeof(HomeController).FullName;

        
        public ActionResult Index()
        {

            Logger.TraceMethodStart(ClassName);
            Logger.TraceMethodEnd(ClassName);
            return View();
        }

       
       

    }
}