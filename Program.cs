using Invoice_Manager_API.Data;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Invoice_Manager_API.Services;
using Invoice_Manager_API.Mapping;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
                        .Configuration
                        .GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InvoiceManagerApiDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
