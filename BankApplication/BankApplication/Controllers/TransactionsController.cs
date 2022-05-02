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
    public class TransactionsController : ControllerBase
    {
        private readonly BankDBContext _context;

        public TransactionsController(BankDBContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<Response> GetTransactions()
        {
            Response response = new();

            // get transactions
            List<Transaction> transactions = await _context.Transactions.ToListAsync();

            // successful
            if (transactions.Any())
            {
                // set successful response codes
                response.Transactions = transactions;
                response.StatusCode = 200;
                response.StatusDescription = "Call was successful. Returned existing transactions.";
            }
            // empty list
            else
            {
                response.StatusCode = 400;
                response.StatusDescription = "No existing transactions. Please insert some first.";
            }

            return response;
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
