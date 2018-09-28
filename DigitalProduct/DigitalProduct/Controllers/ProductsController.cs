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
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());
            var objuser = new UserRepository().GetProfile(userId);
            ViewBag.lstProducts = new ProductRepository().GetAll();
            var DataTableDetail = new HomeController().getDataTableDetail("Products", objuser.default_number);
            ViewBag.Page = DataTableDetail.Item1;
            ViewBag.PageIndex = DataTableDetail.Item2;
            return View();
        }

        public ActionResult Create()
        {
            ProductVM productVM = new ProductVM();
            ViewBag.PageType = "Create";
            try
            {
                ViewBag.lstCategory = new CategoryRepository().GetAll().ToList().Select(d => new SelectListItem { Text = d.CategoryName, Value = d.Id.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View("Create", productVM);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductVM productVM = new ProductRepository().Get(id);
                ViewBag.PageType = "Edit";
                try
                {
                    ViewBag.lstCategory = new CategoryRepository().GetAll().ToList().Select(d => new SelectListItem { Text = d.CategoryName, Value = d.Id.ToString() }).ToList();
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                return View("Create", productVM);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateProducts(ProductVM productVM)
        {
            try
            {
                var product = new ProductRepository().AddOrUpdate(productVM);
                return new ReplyFormat().Success(Messages.SUCCESS, product);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }

        public JsonResult GetProducts(DataTablesParam param)

        {
            List<ProductVM> list = new List<ProductVM>();
            list = new ProductRepository().GetAll();
            return Json(new
            {
                aaData = list,
                sEcho = param.sEcho,
                iTotalDisplayRecords = list.Count(),
                iTotalRecords = list.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool retval = true;
            try
            {
                retval = new ProductRepository().Delete(id);
                return new ReplyFormat().Success(string.Format(Messages.DELETE_MESSAGE, "product"), null);
            }
            catch (Exception ex)
            {

                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }
    }
}