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

namespace DigitalProduct.Repository
{
    public class CommentRepository
    {
        private DigitalProductEntities db = new DigitalProductEntities();

        public CommentVM AddOrUpdate(CommentVM commentVM)
        {
            try
            {
                var comment = db.comments.FirstOrDefault(p => p.Id == commentVM.Id);
                if (comment == null)
                {
                    comment = Mapper.Map<CommentVM, comment>(commentVM);
                    db.comments.Add(comment);
                    db.SaveChanges();
                    return Mapper.Map<comment, CommentVM>(comment);
                }
                else
                {
                    db.Entry(comment).CurrentValues.SetValues(commentVM);
                    db.SaveChanges();
                    return Mapper.Map<comment, CommentVM>(comment);
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


        public List<CommentVM> GetAll()
        {
            List<comment> comments = new List<comment>();
            try
            {
                comments = db.comments.Where(i => i.Publish).ToList();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<List<comment>, List<CommentVM>>(comments);
        }

        public List<CommentVM> CommentsForProduct(int product_id)
        {
            List<comment> comments = new List<comment>();

            try
            {
                comments = db.sp_getComments1(product_id).ToList();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<List<comment>, List<CommentVM>>(comments);
        }

        public CommentVM Get(int id)
        {
            comment comment = new comment();
            try
            {
                comment = db.comments.FirstOrDefault(c => c.Publish);
                if (comment == null)
                {
                    throw new Exception(Messages.BAD_DATA);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<comment, CommentVM>(comment);
        }


        public bool Delete(int id)
        {
            var data = db.comments.FirstOrDefault(p => p.Id == id);
            db.comments.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}