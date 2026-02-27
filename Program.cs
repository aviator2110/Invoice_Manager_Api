using FluentValidation;
using FluentValidation.AspNetCore;
using Invoice_Manager_API.Data;
using Invoice_Manager_API.Mapping;
using Invoice_Manager_API.Middlewares;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
                        .Configuration
                        .GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InvoiceManagerApiDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InvoiceManagerApiDbContext>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRowService, InvoiceRowService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
