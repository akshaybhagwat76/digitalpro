using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalProduct.Models;
using DigitalProduct.ViewModel;
using AutoMapper;
using System.Data.Entity.Validation;
using Elmah;
using DigitalProduct.Helpers;

namespace DigitalProduct.Repository
{
    public class CategoryRepository
    {
        private DigitalProductEntities db = new DigitalProductEntities();

        public CategoryRepository()
        {

        }

        public CategoryVM AddOrUpdate(CategoryVM categoryVM)
        {
            try
            {
                var category = db.categories.FirstOrDefault(p => p.Id == categoryVM.Id);
                if (category == null)
                {
                    categoryVM.IsActive = true;
                    categoryVM.IsDeleted = false;
                    category = Mapper.Map<CategoryVM, category>(categoryVM);
                    db.categories.Add(category);
                    db.SaveChanges();
                    return Mapper.Map<category, CategoryVM>(category);
                }
                else
                {
                    categoryVM.IsActive = true;
                    categoryVM.IsDeleted = false;
                    db.Entry(category).CurrentValues.SetValues(categoryVM);
                    db.SaveChanges();
                    return Mapper.Map<category, CategoryVM>(category);
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

    

        public List<CategoryVM> GetAll()
        {
            List<category> category = new List<category>();
            try
            {
                category = db.categories.Where(i => i.IsActive && i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<List<category>, List<CategoryVM>>(category);
        }

        public CategoryVM Get(int id)
        {
            category category = new category();
            try
            {
                category = db.categories.FirstOrDefault(i => i.Id == id && i.IsActive);
                if (category == null)
                {
                    throw new Exception(Messages.BAD_DATA);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<category, CategoryVM>(category);
        }


        public bool Delete(int id)
        {
            var data = db.categories.FirstOrDefault(p => p.Id == id);
            if (data != null)
            {
                data.IsDeleted = true;
            }
            return db.SaveChanges() > 0;
        }
    }
}