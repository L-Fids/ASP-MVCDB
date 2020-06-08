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
        }

        // CREATE a new customer in the DB, 
        public static int CreateCustomer(string firstName, string lastName,
            string address, string city, string province, string postalCode, string phoneNumber, string email, string leadSource, string status, 
            string notes)
        {
            CustomerDataModel data = new CustomerDataModel // mapping the UI model to the DataLibrary model
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                Province = province,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                Email = email,
                LeadSource = leadSource,
                Status = status,
                Notes = notes,
            };
            
            string sql = @"INSERT INTO Customer
                           (FirstName, LastName, Address, City, Province, PostalCode, PhoneNumber, Email, LeadSource, Status, Notes)
                            VALUES 
                           (@FirstName, @LastName, @Address, @City, @Province, @PostalCode, @PhoneNumber, @Email, @LeadSource, @Status, @Notes)";
            
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
            string address, string city, string province, string postalCode, string phoneNumber, string email, string leadSource, string status,
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
                Province = province,
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
                           Province = @Province,
                           PostalCode = @PostalCode,
                           PhoneNumber = @PhoneNumber,
                           Email = @Email,
                           LeadSource = @LeadSource,
                           Status = @Status,
                           Notes = @Notes
                           WHERE CustomerID = @CustomerID";

            return SqlDataAccess.SaveData(sql, data);
        }
        
        // MULTI-MAPPING QUERIES

        // New and improved multi-map query that uses the sql access layer's generic multi map function
        public static CustomerDataModel LoadMultiMap(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("CustomerID", id);

            string sql = @"SELECT cu.*, fi.*
                           FROM Customer cu
                           LEFT JOIN Financials fi
                               ON cu.CustomerID = fi.CustomerID
                            WHERE cu.CustomerID = @CustomerID";

            // What column should dapper use to differentiate the objects.
            string splitCategory = "FinancialID";

            // return the customer object with a financial object attached.
            return SqlDataAccess.LoadDataSingleMultiMap<CustomerDataModel, FinancialDataModel, CustomerDataModel>(
                sql,
                MapResults,
                new { @CustomerID = id },
                splitCategory);
        }

        public static List<CustomerDataModel> MultipleMultiMap()
        {
            // Left Join the Customer and Financial Tables
            string sql = @"SELECT cu.*, fi.*
                           FROM Customer cu
                           LEFT JOIN Financials fi
                               ON cu.CustomerID = fi.CustomerID";

            string splitCategory = "FinancialID";

            return SqlDataAccess.LoadDataMultiMap<CustomerDataModel, FinancialDataModel, CustomerDataModel>(
                    sql,
                    MapResults,
                    splitCategory);
        }

        // Mapping function for multiple table queries.
        private static CustomerDataModel MapResults (CustomerDataModel customer, FinancialDataModel financial) 
        {
            if (financial != null)
            {
                customer.FinancialData = financial;
                return customer;
            }
            else
            {
                FinancialDataModel generateModel = new FinancialDataModel();
                customer.FinancialData = generateModel;
                return customer;
            }
        }


        // CREATE a new customer in the DB, 
        public static int CreateCustomerWithFinancial(string firstName, string lastName,
            string address, string city, string province, string postalCode, string phoneNumber, string email, string leadSource, string status,
            string notes)
        {
            CustomerDataModel data = new CustomerDataModel // mapping the UI model to the DataLibrary model
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                Province = province,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                Email = email,
                LeadSource = leadSource,
                Status = status,
                Notes = notes,
            };

            string sql = @"INSERT INTO Customer
                           (FirstName, LastName, Address, City, Province, PostalCode, PhoneNumber, Email, LeadSource, Status, Notes)
                            VALUES 
                           (@FirstName, @LastName, @Address, @City, @Province, @PostalCode, @PhoneNumber, @Email, @LeadSource, @Status, @Notes);
                           SELECT CAST(SCOPE_IDENTITY() as int)";

            return SqlDataAccess.SaveData(sql, data);
        }


        // Map the View Model to the Data Model (want to implement this for testability purposes)

        //public static CustomerDataModel ViewModelToDataModel(string firstName, string lastName,
        //    string address, string city, string postalCode, string phoneNumber, string email, string leadSource, string status,
        //    string notes)
        //{
        //    CustomerDataModel dataModel = new CustomerDataModel 
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Address = address,
        //        City = city,
        //        PostalCode = postalCode,
        //        PhoneNumber = phoneNumber,
        //        Email = email,
        //        LeadSource = leadSource,
        //        Status = status,
        //        Notes = notes,
        //    };

        //    return dataModel;
        //}
    }
}
