using GraphQL101HotChocolate.Models;
using GraphQL101HotChocolate.Schema;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure more services
// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections(); // This enables the use of projections, which allow for selecting specific fields in queries.

// build app
var app = builder.Build();

app.MapGraphQL();

app.Run();
