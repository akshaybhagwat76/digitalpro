using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalProduct.Helpers
{
    public class FilterHelper
    {
        public ContentResult Filter(IFilterRepository repo, DataTableFilter filter = null)
        {
            filter = filter ?? new DataTableFilter
            {
                start = 0,
                length = 10
            };
            var data = repo.Filter(filter);
            var list = Newtonsoft.Json.JsonConvert.SerializeObject(data,
                Newtonsoft.Json.Formatting.None,
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return new ContentResult
            {
                Content = list,
                ContentType = "application/json"
            };
        }

    }
}