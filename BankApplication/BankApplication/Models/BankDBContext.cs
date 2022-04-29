using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using BankApplication.Models;

namespace BankApplication.Models
{
    public class BankDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public BankDBContext(DbContextOptions<BankDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("BankDataService");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
    }
}
