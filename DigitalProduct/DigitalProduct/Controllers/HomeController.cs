using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DigitalProduct.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                       .Select(c => c.Value).SingleOrDefault());
                string role_name = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).SingleOrDefault();
                ViewBag.roleName = role_name;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult setDataTableDetail(string SessionName, string PageLength, string Page)
        {
            bool retval = true;
            Session[SessionName + "PageLength"] = PageLength;
            Session[SessionName + "Page"] = Page;
            return Json(retval, JsonRequestBehavior.AllowGet);
        }

        public Tuple<int, int> getDataTableDetail(string SessionName, int? default_number)
        {
            int PageLength = System.Web.HttpContext.Current.Session[SessionName + "PageLength"] != null && System.Web.HttpContext.Current.Session[SessionName + "PageLength"].ToString() != "" ? Convert.ToInt16(System.Web.HttpContext.Current.Session[SessionName + "PageLength"]) : 0;
            int Page = System.Web.HttpContext.Current.Session[SessionName + "Page"] != null && System.Web.HttpContext.Current.Session[SessionName + "Page"].ToString() != "" ? Convert.ToInt16(System.Web.HttpContext.Current.Session[SessionName + "Page"]) : 0;
            int PageIndex = PageLength == 0 ? (default_number.HasValue ? default_number.Value : 25) : PageLength;

            System.Web.HttpContext.Current.Session[SessionName + "PageLength"] = null;
            System.Web.HttpContext.Current.Session[SessionName + "Page"] = null;

            return Tuple.Create<int, int>(Page, PageIndex);
        }
    }
}