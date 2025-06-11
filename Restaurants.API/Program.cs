using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);



var app = builder.Build();

// Step 1: Open the toolbox (create a scope)
var scope = app.Services.CreateScope();

// Step 2: Grab the seeder tool from the toolbox
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

// Step 3: Use the seeder tool to add data to the database
await seeder.Seed();


// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging(); // Enable Serilog request logging

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
