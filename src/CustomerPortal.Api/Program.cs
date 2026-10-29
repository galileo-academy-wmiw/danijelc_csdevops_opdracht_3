using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddScoped<CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.MapGet(
    "/customers/{id:int}",
    (int id, CustomerService customerService) =>
    {
        var customerResponse = customerService.GetCustomer(id);

        if (customerResponse.Status is Status.NotFound)
        {
            return Results.NotFound();
        }
        return Results.Json(customerResponse.Customers[0]);
    }
);

app.MapGet(
    "/customers",
    (CustomerService customerService) =>
    {
        var customerResponse = customerService.GetAll();
        return Results.Json(customerResponse.Customers);
    }
);
app.MapPost(
    "/customers",
    (CreateCustomerRequest request, CustomerService customerService) =>
    {
        var customerResponse = customerService.CreateCustomer(request);

        if (customerResponse.Status is Status.ValidationFailed)
        {
            return Results.BadRequest(customerResponse.Error);
        }

        if (customerResponse.Status == Status.SaveFailed)
        {
            return Results.Conflict(customerResponse.Error);
        }

        return Results.Created(
            $"/customers/{customerResponse.Customers[0].Id}",
            customerResponse.Customers[0]
        );
    }
);

app.Run();
