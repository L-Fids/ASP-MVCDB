using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RoofDBApp.Models;

namespace RoofDBApp.Tests
{
    public class ExtensionMethodsTests
    {
        // Test the extension method quote total function.
        [Fact]
        public void TotalQuoteCalculation()
        {
            // Arrange
            List<CustomerViewModel> customerModels = new List<CustomerViewModel>();
            
            CustomerViewModel customer1 = new CustomerViewModel { Quote = 12000 };
            CustomerViewModel customer2 = new CustomerViewModel { Quote = 15000 };
            CustomerViewModel customer3 = new CustomerViewModel { Quote = null };

            customerModels.Add(customer1);
            customerModels.Add(customer2);
            customerModels.Add(customer3);

            decimal? expectedQuoteTotal = customer1.Quote + customer2.Quote;

            // Act
            decimal? actualQuoteTotal = customerModels.QuoteTotal();

            // Assert
            Assert.Equal(expectedQuoteTotal, actualQuoteTotal);
        }
    }
}
