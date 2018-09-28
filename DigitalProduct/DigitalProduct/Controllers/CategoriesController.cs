using DigitalProduct.Helpers;
using DigitalProduct.Repository;
using DigitalProduct.ViewModel;
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
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());
            var objuser = new UserRepository().GetProfile(userId);
            ViewBag.lstCategories = new CategoryRepository().GetAll();
            var DataTableDetail = new HomeController().getDataTableDetail("Categories", objuser.default_number);
            ViewBag.Page = DataTableDetail.Item1;
            ViewBag.PageIndex = DataTableDetail.Item2;
            return View();
        }

        public ActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM();
            ViewBag.PageType = "Create";
            return View("ManageCategory", categoryVM);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                CategoryVM categoryVM = new CategoryRepository().Get(id);
                ViewBag.PageType = "Edit";
                return View("ManageCategory", categoryVM);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateCategories(CategoryVM categoryVM)
        {
            try
            {
                var category = new CategoryRepository().AddOrUpdate(categoryVM);
                return new ReplyFormat().Success(Messages.SUCCESS, category);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool retval = true;
            try
            {
                retval = new CategoryRepository().Delete(id);
                return new ReplyFormat().Success(string.Format(Messages.DELETE_MESSAGE, "category"), null);
            }
            catch (Exception ex)
            {

                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }
    }
}