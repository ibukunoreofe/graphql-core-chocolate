using GraphQL101HotChocolate.Models;
using GraphQL101HotChocolate.Schema.Mutations;
using GraphQL101HotChocolate.Schema.Queries;
using GraphQL101HotChocolate.Schema.Subscriptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure more services
// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddProjections() // This enables the use of projections, which allow for selecting specific fields in queries.
    .AddFiltering() // Optional, if you're using filtering
    .AddSorting() // Optional, if you're using sorting; 
    .AddInMemorySubscriptions(); // For events; 

// build app
var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
