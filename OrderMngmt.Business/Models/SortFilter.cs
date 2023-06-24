using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMngmt.Business.Models
{
    public class SortFilter
    {
        // should capitalize the first letter of the property name
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }

        public Boolean IsValid(Type type)
        {
            return type.GetProperties().Any(p => p.Name.Equals(SortBy, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}