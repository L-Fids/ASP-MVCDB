using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofDBApp.DataLibrary.Models
{
    public class FinancialDataModel
    {
        public int FinancialID { get; set; }
        public int CustomerID { get; set; }
        public decimal? Quote { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal? Commission { get; set; }
    }
}
