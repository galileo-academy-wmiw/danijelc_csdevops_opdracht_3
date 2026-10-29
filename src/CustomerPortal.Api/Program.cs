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
        var response = customerService.GetCustomer(id);

        if (response.Status == Status.NotFound)
        {
            return Results.NotFound();
        }
        return Results.Json(response.Value);
    }
);

app.MapGet(
    "/customers",
    (CustomerService customerService) =>
    {
        var response = customerService.GetAll();
        return Results.Json(response.Value);
    }
);

app.MapPost(
    "/customers",
    (CreateCustomerRequest request, CustomerService customerService) =>
    {
        var response = customerService.CreateCustomer(request);

        if (response.Status == Status.ValidationFailed)
        {
            return Results.BadRequest(response.Error);
        }

        if (response.Status == Status.AlreadyExists)
        {
            return Results.Conflict(response.Error);
        }

        return Results.Created($"/customers/{response.Value?.Id}", response.Value);
    }
);

app.Run();
