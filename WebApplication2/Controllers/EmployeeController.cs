using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        CustomerRepository objCustomer = new CustomerRepository();
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            customers = objCustomer.GetAllCustomers().ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create_customer()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create_customer([Bind] Customer cust)
        {
            if (ModelState.IsValid)
            {
                objCustomer.AddCustomer(cust);
                return RedirectToAction("Index");
            }
            return View(objCustomer);
        }


        public ActionResult Edit_customer(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Customer customer = objCustomer.GetCustomerData(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_customer(int id, [Bind] Customer cust)
        {
            if (id != cust.ID)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                objCustomer.UpdateCustomer(cust);
                return RedirectToAction("Index");
            }
            return View(objCustomer);
        }


        [HttpGet]
        public ActionResult customer_Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Customer objcustomer = objCustomer.GetCustomerData(id);

            if (objcustomer == null)
            {
                return HttpNotFound();
            }
            return View(objcustomer);
        }


        [HttpGet]
        public ActionResult Delete_customer(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Customer objcustomer = objCustomer.GetCustomerData(id);

            if (objcustomer == null)
            {
                return HttpNotFound();
            }
            return View(objcustomer);
        }

        [HttpPost, ActionName("Delete_customer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            objCustomer.DeleteCustomer(id);
            return RedirectToAction("Index");
        }


    }
}