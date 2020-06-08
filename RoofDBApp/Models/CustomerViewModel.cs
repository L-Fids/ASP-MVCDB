using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoofDBApp.Models
{
    public class CustomerViewModel
    {
        // UI MODEL

        // Customer Model Properties
        [HiddenInput(DisplayValue=false)]
        public int CustomerID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter a first name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a last name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Lead Source")]
        public string LeadSource { get; set; }
        
        public string Status { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        // Financial Model Properties
        [HiddenInput(DisplayValue = false)]
        public int? FinancialID { get; set; }
        
        [Display(Name = "Quotes")]
        public decimal? Quote { get; set; }

        [Display(Name = "Final Price")]
        public decimal? FinalPrice { get; set; }

        [Display(Name = "Commission")]
        public decimal? Commission { get; set; }

    }
}