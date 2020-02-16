using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofDBApp.Models
{
    public class FinancialViewModel
    {
        public int FinancialID { get; set; }
        public int CustomerID { get; set; }
        
        [Display(Name = "Quote")]
        public decimal? Quote { get; set; }
       
        [Display(Name = "Final Price")]
        public decimal? FinalPrice { get; set; }
        
        [Display(Name = "Commission")]
        public decimal? Commission { get; set; }
    }
}