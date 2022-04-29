using System;
namespace BankApplication.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? TransactionName { get; set; }
        public float Amount { get; set; }
        public int? CustomerId { get; set; }

    }
}
