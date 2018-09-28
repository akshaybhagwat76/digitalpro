using AutoMapper;
using DigitalProduct.Helpers;
using DigitalProduct.Models;
using DigitalProduct.ViewModel;
using Elmah;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalProduct.Repository
{
    public class ProductRepository : IFilterRepository
    {
        private DigitalProductEntities db = new DigitalProductEntities();
        public ProductRepository()
        {

        }

        public ProductVM AddOrUpdate(ProductVM productVM)
        {
            try
            {
                var product = db.products.FirstOrDefault(p => p.Id == productVM.Id);
                if (product == null)
                {
                    productVM.IsActive = true;
                    productVM.IsDeleted = false;
                    product = Mapper.Map<ProductVM, product>(productVM);
                    db.products.Add(product);
                    db.SaveChanges();
                    return Mapper.Map<product, ProductVM>(product);
                }
                else
                {
                    productVM.IsActive = true;
                    productVM.IsDeleted = false;
                    db.Entry(product).CurrentValues.SetValues(productVM);
                    db.SaveChanges();
                    return Mapper.Map<product, ProductVM>(product);
                }
            }
            catch (DbEntityValidationException dve)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(dve);
                throw new Exception(string.Join("\n", dve.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(y => y.ErrorMessage)));
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        public Reply Filter(DataTableFilter filter = null)
        {
            var ret = new Reply { draw = filter.draw };
            var data = db.products.AsQueryable();

            var iq_data = data.Select(d => new
            {
                id = d.Id,
                productname = d.ProductName,
                title = d.Title,
                description = d.Description,
                price = d.Price,
                status = d.IsActive == true ? "Active" : "Deactive"
            });
            if (!string.IsNullOrWhiteSpace(filter.search.value))
            {
                string search = filter.search.value.ToLower();
                iq_data = iq_data.Where(i =>
                        (!String.IsNullOrEmpty(i.productname) && i.productname.ToLower().Contains(search)) ||
                        (!String.IsNullOrEmpty(i.title) && i.title.ToLower().Contains(search)) ||
                        (!String.IsNullOrEmpty(i.description) && i.description.ToLower().Contains(search)) ||
                        (!String.IsNullOrEmpty(i.status) && i.status.ToLower().Contains(search))
                    );
            }

            if (filter.order != null && filter.order.Count() > 0)
            {
                iq_data = iq_data.ApplyOrder(filter.columns[filter.order[0].column].data, filter.order[0].dir == "asc" ? "OrderBy" : "OrderByDescending");
            }
            ret.prepareReply(iq_data, filter.start, filter.length);
            return ret;
        }

        public List<ProductVM> GetAll()
        {
            List<product> product = new List<product>();
            try
            {
                product = db.products.Where(i => i.IsActive && i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<List<product>, List<ProductVM>>(product);
        }

        public ProductVM Get(int id)
        {
            product product = new product();
            try
            {
                product = db.products.FirstOrDefault(i => i.Id == id && i.IsActive);
                if (product == null)
                {
                    throw new Exception(Messages.BAD_DATA);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<product, ProductVM>(product);
        }


        public bool Delete(int id)
        {
            var data = db.products.FirstOrDefault(p => p.Id == id);
            if (data != null)
            {
                data.IsActive = false;
                data.IsDeleted = true;
            }
            return db.SaveChanges() > 0;
        }
    }
}