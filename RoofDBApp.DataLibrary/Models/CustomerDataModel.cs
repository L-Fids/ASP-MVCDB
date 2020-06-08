using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofDBApp.DataLibrary.Models
{
    public class CustomerDataModel
    {
        // DATA ACCESS MODEL

        // Customer Table Properties
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LeadSource { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        // Financial Model Object
        public FinancialDataModel FinancialData { get; set; }
    }
}
