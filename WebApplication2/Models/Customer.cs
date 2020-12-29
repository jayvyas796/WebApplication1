using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(15, ErrorMessage = "Name should be less than or equal to Fifteen characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Address")]
        [StringLength(50, ErrorMessage = "Name should be less than or equal to fifty characters.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Your Gender")]
        [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter Your City")]
        [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter Your Country")]
        [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Your must provide a Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter Your Mail ID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }


    public class CustomerRepository
    {
        string connectionString = @"Data Source=DESKTOP-6DJRB3G;Initial Catalog=HRMS;Integrated Security=True";
        //To View all Customers details      
        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> lstCustomer = new List<Customer>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Customer Customer = new Customer();

                    Customer.ID = Convert.ToInt32(sdr["CustomerID"]);
                    Customer.Name = sdr["Name"].ToString();
                    Customer.Address = sdr["Address"].ToString();
                    Customer.Gender = sdr["Gender"].ToString();
                    Customer.Country = sdr["Country"].ToString();
                    Customer.City = sdr["City"].ToString();
                    Customer.Mobile = sdr["MobileNo"].ToString();
                    Customer.Email = sdr["MailId"].ToString();
                    lstCustomer.Add(Customer);
                }
                con.Close();
            }
            return lstCustomer;
        }
        //To Add new Customer record      
        public void AddCustomer(Customer Customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", Customer.Name);
                cmd.Parameters.AddWithValue("@Address", Customer.Address);
                cmd.Parameters.AddWithValue("@Gender", Customer.Gender);
                cmd.Parameters.AddWithValue("@Country", Customer.Country);
                cmd.Parameters.AddWithValue("@City", Customer.City);
                cmd.Parameters.AddWithValue("@MobileNo", Customer.Mobile);
                cmd.Parameters.AddWithValue("@MailId", Customer.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar Customer    
        public void UpdateCustomer(Customer Customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", Customer.ID);
                cmd.Parameters.AddWithValue("@Name", Customer.Name);
                cmd.Parameters.AddWithValue("@Address", Customer.Address);
                cmd.Parameters.AddWithValue("@Gender", Customer.Gender);
                cmd.Parameters.AddWithValue("@Country", Customer.Country);
                cmd.Parameters.AddWithValue("@City", Customer.City);
                cmd.Parameters.AddWithValue("@MobileNo", Customer.Mobile);
                cmd.Parameters.AddWithValue("@MailId", Customer.Email);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular Customer    
        public Customer GetCustomerData(int? id)
        {
            Customer Customer = new Customer();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("sp_GetCustomerByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Customer.ID = Convert.ToInt32(sdr["CustomerID"]);
                    Customer.Name = sdr["Name"].ToString();
                    Customer.Address = sdr["Address"].ToString();
                    Customer.Gender = sdr["Gender"].ToString();
                    Customer.Country = sdr["Country"].ToString();
                    Customer.City = sdr["City"].ToString();
                    Customer.Mobile = sdr["MobileNo"].ToString();
                    Customer.Email = sdr["MailId"].ToString();
                }
            }
            return Customer;
        }

        //To Delete the record on a particular Customer    
        public void DeleteCustomer(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}