using System;
namespace BankApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public Transaction? Transaction { get; set; }

    }
}
