using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace DigitalProduct.Helpers
{
    public interface IFilterRepository
    {
        Reply Filter(DataTableFilter filter);
    }

    public class DataTableFilter
    {
        public class Search
        {
            public string value { get; set; }
            public bool regex { get; set; }
        }
        public class OrderInfo
        {
            public int column { get; set; }
            public string dir { get; set; }
        }
        public class ColumnInfo
        {
            public string data { get; set; }
            public string name { get; set; }
            public bool searchable { get; set; }
            public bool orderable { get; set; }
            public Search search { get; set; }
        }
        public string draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
        public List<OrderInfo> order { get; set; }
        public List<ColumnInfo> columns { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> client_id { get; set; }
        public Nullable<int> project_id { get; set; }
        public string status { get; set; }
        public string role_name { get; set; }
        public string Type { get; set; }
    }

    public class Reply
    {
        public IEnumerable<dynamic> data { get; set; }
        public dynamic obj { get; set; }
        public string error { get; set; }
        public string draw { get; set; }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }

        public void prepareReply(IQueryable<dynamic> data, int start, int length, long CustomTotal = 0)
        {

            this.recordsTotal = CustomTotal == 0 ? data != null ? data.Count() : 0 : CustomTotal;
            this.data = data != null ? data.Skip(start).Take(length) : null;
            this.recordsFiltered = this.recordsTotal; // this.data != null ? this.data.Count() : 0;
        }
    }

    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }

    public class DT_RowAttr
    {

    }
}