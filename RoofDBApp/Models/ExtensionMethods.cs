using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoofDBApp.Models
{
    public static class ExtensionMethods
    {
        public static decimal? QuoteTotal(this IEnumerable<CustomerViewModel> customerEnum)
        {
            decimal? total = 0;
            foreach (CustomerViewModel cust in customerEnum)
            {
                if (cust.Quote != null)
                {
                    total += cust.Quote;
                }
            }
            return total;
        }
    }
}