using RoofDBApp.DataLibrary.BusinessLogic;
using RoofDBApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RoofDBApp.Controllers
{
    public class CustomerController : Controller
    {
        // READ: list the customers
        public ActionResult Index()
        {
            var data = CustomerProcessor.LoadCustomers();
            //var data = CustomerProcessor.LoadMultiMap();
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
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                }) ;
            }

            return View(customers);
        }

        // READ: get details for a customer
        public ActionResult Details(int id) // the int ID is coming from the path of the controller
        {
            // pull in all customers into a CustomerModel list (based on data access model)
            var data = CustomerProcessor.LoadMultiMap(id);

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
                    PostalCode = row.PostalCode,
                    PhoneNumber = row.PhoneNumber,
                    Email = row.Email,
                    LeadSource = row.LeadSource,
                    Status = row.Status,
                    Notes = row.Notes,
                    Quote = row.Quote,
                    FinalPrice = row.FinalPrice,
                    Commission = row.Commission
                });
            }
             
            var customer = customers.Where(x => x.CustomerID == id).First();

            return View(customer);
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
        public ActionResult Create([Bind(Exclude = "CustomerID")]CustomerViewModel customerModel)
        {
            if (ModelState.IsValid) // if valid, post, then return to the customer list
            {
                try
                {
                    int recordsCreated = CustomerProcessor.CreateCustomer(customerModel.FirstName, customerModel.LastName, customerModel.Address,
                    customerModel.City, customerModel.PostalCode, customerModel.PhoneNumber, customerModel.Email, customerModel.LeadSource,
                    customerModel.Status, customerModel.Notes, customerModel.Quote, customerModel.FinalPrice, customerModel.Commission);

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
            // Get the data from the DB via the Data Model
            var data = CustomerProcessor.LoadSingleCustomer(id);

            // Create CustomerModel list from the MVC Model
            CustomerViewModel customerEdit;

            // Translate data model to the MVC model
            customerEdit = new CustomerViewModel
                {
                    CustomerID = data.CustomerID,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Address = data.Address,
                    City = data.City,
                    PostalCode = data.PostalCode,
                    PhoneNumber = data.PhoneNumber,
                    Email = data.Email,
                    LeadSource = data.LeadSource,
                    Status = data.Status,
                    Notes = data.Notes
                };

            //var customerEdit = customer.Where(x => x.CustomerID == id).First();
            
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
                    int recordsEdited = CustomerProcessor.UpdateCustomer(model.CustomerID, model.FirstName, model.LastName, model.Address,
                    model.City, model.PostalCode, model.PhoneNumber, model.Email, model.LeadSource,
                    model.Status, model.Notes);
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
    }
}
