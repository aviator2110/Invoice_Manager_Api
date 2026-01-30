using Invoice_Manager_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Manager_API.Data;

public class InvoiceManagerApiDbContext : DbContext
{
    public InvoiceManagerApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(
            customer =>
            {

            }
        );
    }
}
