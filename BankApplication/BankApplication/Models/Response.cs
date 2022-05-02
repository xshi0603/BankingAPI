namespace BankApplication.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? StatusDescription { get; set; }
        public Customer? Customer { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
