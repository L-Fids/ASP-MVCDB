using RoofDBApp.DataLibrary.BusinessLogic;
using RoofDBApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RoofDBApp.Controllers
{
    public class CustomerController : Controller
    {
        // READ: list the customers
        public ActionResult Index(string status)
        {
            // 1. Get the data and put it into a list
            var data = CustomerProcessor.LoadCustomers(); // load all customers into var
            List<CustomerViewModel> customers = new List<CustomerViewModel>(); // create CustomerViewModel type list

            foreach (var row in data) // pass the data model list to the view model list.
            {
                customers.Add(new CustomerViewModel
                {
                    CustomerID = row.CustomerID,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Address = row.Address,
                    City = row.City,
                    Province = row.Province,
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                });
            }

            // 2. Create the status filter list and send to View Bag
            List<string> statusList = new List<String>();

            foreach (var row in customers)
            {
                if (row.Status != null)
                {
                    statusList.Add(row.Status.ToString());
                }
            }

            var filteredStatusList = statusList.Distinct();

            ViewBag.status = new SelectList(filteredStatusList);

            return View(status);
        }
    
            

        public ActionResult GetCustomerData(string status)
        {
            // 1. Get the data and put it into a list
            var data = CustomerProcessor.LoadCustomers(); // load all customers into var
            List<CustomerViewModel> customers = new List<CustomerViewModel>(); // create CustomerViewModel type list

            foreach (var row in data) // pass the data model list to the view model list.
            {
                customers.Add(new CustomerViewModel
                {
                    CustomerID = row.CustomerID,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Address = row.Address,
                    City = row.City,
                    Province = row.Province,
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                });
            }

            // 2. Create the status filter list and send to View Bag
            List<string> statusList = new List<String>();

            foreach (var row in customers)
            {
                if (row.Status != null)
                {
                    statusList.Add(row.Status.ToString());
                }
            }

            var filteredStatusList = statusList.Distinct();

            ViewBag.status = new SelectList(filteredStatusList);
            
            // 3. Filter the list if needed
            if (!String.IsNullOrEmpty(status))
            {
                customers = customers.Where(s => s.Status == status).ToList();
            }
           
            // 4a. If this is an AJAX request, format the data, and return a JSON result object (for processing with JavaScript)
            if (Request.IsAjaxRequest())
            {
                var formattedData = customers.Select(c => new
                {
                    CustomerID = c.CustomerID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostalCode = c.PostalCode,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    LeadSource = c.LeadSource,
                    Status = c.Status,
                    Notes = c.Notes
                });
                return Json(formattedData, JsonRequestBehavior.AllowGet);
            }
            // 4b. If no filtering is needed, return full customer list.
            {
                return PartialView(customers);
            }
        }


        // READ: get details for a customer
        public ActionResult Details(int id) // the int ID is coming from the path of the controller
        {
            // pull in one customer into a CustomerModel object (based on data access model)
            var data = CustomerProcessor.LoadMultiMap(id);

            // create a new object of CustomerModel type (based on the UI model)
            CustomerViewModel customerDetails; 

            customerDetails = new CustomerViewModel
            {
                CustomerID = data.CustomerID,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                City = data.City,
                Province = data.Province,
                PostalCode = data.PostalCode,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
                LeadSource = data.LeadSource,
                Status = data.Status,
                Notes = data.Notes,
                FinancialID = data.FinancialData.FinancialID,
                Quote = data.FinancialData.Quote,
                FinalPrice = data.FinancialData.FinalPrice,
                Commission = data.FinancialData.Commission
            };
            
            return View(customerDetails);
        }

        // READ: get the create form.
        public ActionResult Create()
        {
            ViewBag.Message = "Add Customer";

            return View();
        }

        // CREATE: add a new customer to the database
        [HttpPost]
        [ValidateAntiForgeryToken] // we check this on both the front and back-end 
        public ActionResult Create(CustomerViewModel customerModel)
        {
            if (ModelState.IsValid) // if valid, post, then return to the customer list
            {
                try
                {
                    int recordsCreated = CustomerProcessor.CreateCustomer(customerModel.FirstName, customerModel.LastName, customerModel.Address,
                    customerModel.City, customerModel.Province, customerModel.PostalCode, customerModel.PhoneNumber, customerModel.Email, customerModel.LeadSource,
                    customerModel.Status, customerModel.Notes);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // READ: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            // pull in all customers into a CustomerModel list (based on data access model)
            var data = CustomerProcessor.LoadMultiMap(id);

            // create a new list of CustomerModel type (based on the UI model)
            CustomerViewModel customerEdit;

            customerEdit = new CustomerViewModel
            {
                CustomerID = data.CustomerID,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                City = data.City,
                Province = data.Province,
                PostalCode = data.PostalCode,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
                LeadSource = data.LeadSource,
                Status = data.Status,
                Notes = data.Notes,
                FinancialID = data.FinancialData.FinancialID,
                Quote = data.FinancialData.Quote,
                FinalPrice = data.FinancialData.FinalPrice,
                Commission = data.FinancialData.Commission
            };
            
            return View(customerEdit);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.FinancialID == null)
                    {
                        int recordsEdited = CustomerProcessor.UpdateCustomer(id, model.FirstName, model.LastName, model.Address,
                        model.City, model.Province, model.PostalCode, model.PhoneNumber, model.Email, model.LeadSource,
                        model.Status, model.Notes);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        int customerEdited = CustomerProcessor.UpdateCustomer(id, model.FirstName, model.LastName, model.Address,
                        model.City, model.Province, model.PostalCode, model.PhoneNumber, model.Email, model.LeadSource,
                        model.Status, model.Notes);

                        int financialEdited = FinancialProcessor.ModifyFinancial(id, model.Quote, model.FinalPrice, model.Commission);
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        //GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Message = "Delete a Customer";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = CustomerProcessor.LoadSingleCustomer(id);

            CustomerViewModel customerDelete;

            customerDelete = new CustomerViewModel
            {
                CustomerID = data.CustomerID,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                City = data.City,
                Province = data.Province,
                PostalCode = data.PostalCode,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
                LeadSource = data.LeadSource,
                Status = data.Status,
                Notes = data.Notes
            };

            if (customerDelete == null)
            {
                return HttpNotFound();
            }

            return View(customerDelete);
        }

        //POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int recordsCreated = CustomerProcessor.DeleteCustomer(id);

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Filter(string status) // ajax options
        {
            var data = CustomerProcessor.LoadCustomers();
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            foreach (var row in data)
            {
                customers.Add(new CustomerViewModel
                {
                    CustomerID = row.CustomerID,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Address = row.Address,
                    City = row.City,
                    Province = row.Province,
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                });
            }

            // Process the status list and assign to Viewbag
            List<string> statusList = new List<String>();
            foreach (var row in customers)
            {
                if (row.Status != null)
                {
                    statusList.Add(row.Status.ToString());
                }
            }

            ViewBag.status = new SelectList(statusList);

            if (!String.IsNullOrEmpty(status))
            {
                customers = customers.Where(s => s.Status == status).ToList();
            }
            
            return PartialView("_Customers", customers);
        }

        public ActionResult Quote()
        {
            // pull in all customers into a CustomerModel list (based on data access model)
            var data = CustomerProcessor.MultipleMultiMap();

            // create a new list of CustomerModel type (based on the UI model)
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            foreach (var row in data) // take the data access model and map it to the UI model.
            {
                customers.Add(new CustomerViewModel
                {
                    CustomerID = row.CustomerID,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Address = row.Address,
                    City = row.City,
                    Province = row.Province,
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                    Quote = row.FinancialData.Quote,
                    FinalPrice = row.FinancialData.FinalPrice,
                    Commission = row.FinancialData.Commission
                });
            }

            decimal? total = customers.QuoteTotal();
            ViewBag.Quote = total;
           
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
