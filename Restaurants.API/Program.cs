using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
