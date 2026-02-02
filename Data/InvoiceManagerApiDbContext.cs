using Invoice_Manager_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Invoice_Manager_API.Data;

public class InvoiceManagerApiDbContext : DbContext
{
    public InvoiceManagerApiDbContext(DbContextOptions<InvoiceManagerApiDbContext> options) : base(options)
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
                customer.HasKey(c => c.Id);
                customer.Property(c => c.Name)
                    .IsRequired();
                customer.Property(c => c.Address);
                customer.Property(c => c.Email)
                    .IsRequired();
                customer.Property(c => c.PhoneNumber);
                customer.Property(c => c.CreatedAt)
                    .IsRequired();
                customer.Property(c => c.UpdatedAt)
                    .IsRequired();
                customer.Property(c => c.DeletedAt);
            }
        );

        modelBuilder.Entity<Invoice>(
            invoice =>
            {
                invoice.HasKey(i => i.Id);
                invoice.Property(i => i.StartDate)
                    .IsRequired();
                invoice.Property(i => i.EndDate)
                    .IsRequired();
                invoice.Property(i => i.TotalSum)
                    .IsRequired();
                invoice.Property(i => i.Comment);
                invoice.Property(i => i.Status)
                    .IsRequired();
                invoice.Property(i => i.CreatedAt)
                    .IsRequired();
                invoice.Property(i => i.UpdatedAt)
                    .IsRequired();
                invoice.Property(i => i.DeletedAt);

                invoice.HasOne(i => i.Customer)
                    .WithMany(c => c.Invoices)
                    .HasForeignKey(i => i.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                invoice.HasMany(i => i.Rows)
                    .WithOne(r => r.Invoice)
                    .HasForeignKey(r => r.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        );

        modelBuilder.Entity<InvoiceRow>(
            invoiceRow =>
            {
                invoiceRow.HasKey(ir => ir.Id);
                invoiceRow.Property(ir => ir.Service)
                    .IsRequired();
                invoiceRow.Property(ir => ir.Quantity)
                    .IsRequired();
                invoiceRow.Property(ir => ir.Rate)
                    .IsRequired();
                invoiceRow.Property(ir => ir.Sum)
                    .IsRequired();
            }
        );
    }
}