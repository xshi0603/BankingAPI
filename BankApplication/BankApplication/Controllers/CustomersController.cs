#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApplication.Models;

namespace BankApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BankDBContext _context;

        public CustomersController(BankDBContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<Response> GetCustomers()
        {
            Response response = new();

            // get customers
            List<Customer> customers = await _context.Customers.ToListAsync();

            // successful
            if (customers.Any())
            {
                // populate transaction fields
                List<Transaction> transactions = await _context.Transactions.ToListAsync();

                for (int i = 0; i < customers.Count; i++)
                {
                    foreach (Transaction transaction in transactions)
                    {
                        // transaction matches
                        if (transaction.CustomerId == customers[i].CustomerId)
                        {
                            customers[i].Transaction = transaction;
                        }
                    }
                }
                   
                // set successful response codes
                response.Customers= customers;
                response.StatusCode = 200;
                response.StatusDescription = "Call was successful. Returned existing customers.";
            }
            // empty list
            else
            {
                response.Customers = customers;
                response.StatusCode = 400;
                response.StatusDescription = "No existing customers. Please insert some first.";
            }

            return response;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<Response> GetCustomer(int id)
        {
            Response response = new();

            // get customer
            Customer customer = await _context.Customers.FindAsync(id);

            // successful
            if (customer != null)
            {
                // populate transaction fields
                List<Transaction> transactions = await _context.Transactions.ToListAsync();

                foreach (Transaction transaction in transactions)
                {
                    // transaction matches
                    if (transaction.CustomerId == customer.CustomerId)
                    {
                        customer.Transaction = transaction;
                    }
                }
                
                response.Customer = customer;
                response.StatusCode = 200;
                response.StatusDescription = "Call was successful. Returned customer with id " + id + ".";
            }
            // doesn't exist
            else
            {
                response.StatusCode = 404;
                response.StatusDescription = "No existing customers with this id. Please insert some first.";
            }

            return response;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Response> PostCustomer(Customer customer)
        {
            Response response = new();

            // customer is null
            if (customer == null)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Call was unsucessful. Input was null";
                return response;
            }

            // get customers
            List<Customer> customers = await _context.Customers.ToListAsync();

            bool emailExists = false;

            foreach (Customer tempCustomer in customers)
            {
                if (tempCustomer.Email == customer.Email)
                {
                    emailExists = true;
                }
            }
            
            // email already exists
            if (emailExists)
            {
                response.StatusCode = 500;
                response.StatusDescription = "Call was unsucessful. Email already exists.";
                return response;
            }

            // otherwise proceed with insertion
            try
            {
                _context.Customers.Add(customer);
                int success = await _context.SaveChangesAsync();

                // sucessful
                if (success > 0)
                {
                    response.StatusCode = 201;
                    response.StatusDescription = "Call was successful. Created new customer.";
                }
            }
            catch (Exception ex)
            {
                // misc error, likely with formatting
                response.StatusCode = 500;
                response.StatusDescription = "Call was unsucessful. Likely issue with customer formatting.";
            }

            return response;
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteCustomer(int id)
        {
            Response response = new();

            var customer = await _context.Customers.FindAsync(id);

            // customer doesn't actually exist
            if (customer == null)
            {
                response.StatusCode = 404;
                response.StatusDescription = "No existing customers with this id. Please insert some first.";
            }
            // customer exists, proceed with deletion
            else
            {
                // have to first remove the corresponding transaction
                List<Transaction> transactions = await _context.Transactions.ToListAsync();

                Transaction deleteTransaction;
                foreach (Transaction transaction in transactions)
                {
                    // transaction matches
                    if (transaction.CustomerId == customer.CustomerId)
                    {
                        deleteTransaction = transaction;

                        _context.Transactions.Remove(deleteTransaction);
                        await _context.SaveChangesAsync();
                    }
                }

                // proceed with deleting customer as well
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                response.StatusCode = 202;
                response.StatusDescription = "Call was successful. Deleted customer.";
            }

            return response;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
