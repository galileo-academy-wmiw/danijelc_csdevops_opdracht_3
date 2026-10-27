using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet(
    "/customers/{id:int}",
    (int id) =>
    {
        var customerService = new CustomerService(new InMemoryCustomerRepository());
        var customerResponse = customerService.GetCustomer(id);
        return customerResponse;
    }
);

app.Run();
