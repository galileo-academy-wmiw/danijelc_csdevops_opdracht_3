using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet(
    "/customers/{id:int}",
    (int id, ICustomerRepository repository) =>
    {
        var customerService = new CustomerService(repository);
        var customerResponse = customerService.GetCustomer(id);
        if (customerResponse.Status is Status.NotFound)
        {
            return Results.NotFound();
        }
        return Results.Json(customerResponse.Customers);
    }
);

app.MapGet(
    "/customers",
    (ICustomerRepository repository) =>
    {
        var customerService = new CustomerService(repository);
        var customerResponse = customerService.GetAll();
        return Results.Json(customerResponse.Customers);
    }
);
app.MapPost(
    "/customers",
    (CreateCustomerRequest request, ICustomerRepository repository) =>
    {
        var customerService = new CustomerService(repository);
        var customerResponse = customerService.CreateCustomer(request);
        if (customerResponse.Status is Status.ValidationFailed)
        {
            return Results.BadRequest(customerResponse.Error);
        }
        return Results.Created(
            $"/customers/{customerResponse.Customers[0].Id}",
            customerResponse.Customers
        );
    }
);

app.Run();
