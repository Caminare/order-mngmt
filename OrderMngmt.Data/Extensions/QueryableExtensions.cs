using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OrderMngmt.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return queryable;
            }

            return queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string? prop, string? order)
        {
            var capitalizedProp = prop?.First().ToString().ToUpper() + prop?.Substring(1);
            if (string.IsNullOrEmpty(prop) || string.IsNullOrEmpty(order) || !IsValidProperty<T>(capitalizedProp))
            {
                return queryable;
            }

            var isAscending = order.ToLower() == "asc";
            return isAscending
                ? queryable.OrderBy(capitalizedProp)
                : queryable.OrderByDescending(capitalizedProp);
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string prop)
        {
            return queryable.OrderBy(GetExpression<T>(prop));
        }

        private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> queryable, string prop)
        {
            return queryable.OrderByDescending(GetExpression<T>(prop));
        }

        private static Expression<Func<T, object>> GetExpression<T>(string prop)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, prop);
            var propAsObject = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        private static bool IsValidProperty<T>(string prop)
        {
            return typeof(T).GetProperty(prop) != null;
        }
    }
}