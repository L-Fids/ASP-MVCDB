using Dapper;
using RoofDBApp.DataLibrary.DataAccess;
using RoofDBApp.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofDBApp.DataLibrary.BusinessLogic
{
    // Not a true 3-tier app here. We have the business logic and data access in the same physical tier, though they have some logical separation.
    // TODO: Work on adding dependency inversion.
    public static class CustomerProcessor
    {
        // READ customer list from the DB
        public static List<CustomerDataModel> LoadCustomers()
        {
            string sql = @"SELECT * FROM Customer";

            return SqlDataAccess.LoadData<CustomerDataModel>(sql);
        }

        // READ customer list by ID from the DB
        public static CustomerDataModel LoadSingleCustomer(int? id) // takes id parameter from the controller.
        {
            var parameter = new DynamicParameters();
            parameter.Add("CustomerID", id); 

            string sql = @"SELECT * FROM Customer WHERE CustomerID = @CustomerID";

            return SqlDataAccess.LoadSingle<CustomerDataModel>(sql, parameter);

            //return SqlDataAccess.LoadSingleData<CustomerDataModel>(sql, parameter);
        }

        // CREATE a new customer in the DB, 
        public static int CreateCustomer(string firstName, string lastName,
            string address, string city, string postalCode, string phoneNumber, string email, string leadSource, string status, 
            string notes, decimal? quote, decimal? finalPrice, decimal? commission)
        {
            CustomerDataModel data = new CustomerDataModel // mapping the UI model to the DataLibrary model
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                Email = email,
                LeadSource = leadSource,
                Status = status,
                Notes = notes,
                Quote = quote,
                FinalPrice = finalPrice,
                Commission = commission
            };


            string sql = @"INSERT INTO Customer
                           (FirstName, LastName, Address, City, PostalCode, PhoneNumber, Email, LeadSource, Status, Notes)
                            VALUES 
                           (@FirstName, @LastName, @Address, @City, @PostalCode, @PhoneNumber, @Email, @LeadSource, @Status, @Notes)";
            
            return SqlDataAccess.SaveData(sql, data);
        }

        //DELETE a record from the DB
        public static int DeleteCustomer(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("CustomerID", id);

            string sql = @"DELETE FROM Customer
                           WHERE CustomerID = @customerID";

            return SqlDataAccess.DeleteData(sql, parameter);
        }

        //UPDATE a record from the DB
        public static int UpdateCustomer(int id, string firstName, string lastName,
            string address, string city, string postalCode, string phoneNumber, string email, string leadSource, string status,
            string notes)
        {
            var parameter = new DynamicParameters();
            parameter.Add("CustomerID", id);

            CustomerDataModel data = new CustomerDataModel
            {
                CustomerID = id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                Email = email,
                LeadSource = leadSource,
                Status = status,
                Notes = notes
            };

            string sql = @"UPDATE Customer
                           SET 
                           FirstName = @FirstName,
                           LastName = @LastName,
                           Address = @Address,
                           City = @City,
                           PostalCode = @PostalCode,
                           PhoneNumber = @PhoneNumber,
                           Email = @Email,
                           LeadSource = @LeadSource,
                           Status = @Status,
                           Notes = @Notes
                           WHERE CustomerID = @CustomerID";

            return SqlDataAccess.SaveData(sql, data);
        }
        
        // multi mapping queries take 4 arguments: 1) SQL Query 2) Mapping Function 
        // 3) Command Parameter(id in this case) 4) spliton string (where to split the model)
        public static List<CustomerDataModel> LoadMultiMap(int id)
        {
                        
            // Left Join the Customer and Financial Tables
            string sql = @"SELECT cu.*, fi.*
                           FROM Customer cu
                           LEFT JOIN Financials fi
                               ON cu.CustomerID = fi.CustomerID";

            // Grab the data and map the Financial class data onto the FiancialDataModel property of the Customer class
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<CustomerDataModel, FinancialDataModel, CustomerDataModel>(
                    sql,
                    MapResults,
                    new { @CustomerID = id },
                    splitOn: "FinancialID").ToList();
            }

            //new { @CustomerID = id },    
            //return SqlDataAccess.LoadData<CustomerDataModel>(sql);
        }

        // Mapping function - hopefully will not need this once I can figure out how to properly setup a generic multi mapping query.
        private static CustomerDataModel MapResults (CustomerDataModel customer, FinancialDataModel financial) 
        {
            customer.Quote = financial.Quote;
            customer.FinalPrice = financial.FinalPrice;
            customer.Commission = financial.Commission;
            return customer;
        }
        
        // Need this until I can figure out how to properly setup a generic multi mapping query in my data access class.
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["RoofDBConnection"].ConnectionString;
        }
    }
}
