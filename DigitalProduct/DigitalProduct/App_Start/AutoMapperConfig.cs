using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalProduct.Models;
using DigitalProduct.ViewModel;

namespace DigitalProduct.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserVM, userprofile>();
                cfg.CreateMap<userprofile, UserVM>();

                cfg.CreateMap<RoleVM, user_role>();
                cfg.CreateMap<user_role, RoleVM>();

                cfg.CreateMap<ProductVM, product>();
                cfg.CreateMap<product, ProductVM>();

                cfg.CreateMap<CategoryVM, category>();
                cfg.CreateMap<category, CategoryVM>();

                cfg.CreateMap<CommentVM, comment>();
                cfg.CreateMap<comment, CommentVM>();
            });
        }
    }
}