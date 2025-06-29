using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Ansej.Data.Core
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query, params Expression<Func<T,object>>[] navigationPropertiesToLoad) where T:class
        {
            foreach (var item in navigationPropertiesToLoad)
            {
                query = query.Include(item);
            }

            return query;
        }
    }
}
