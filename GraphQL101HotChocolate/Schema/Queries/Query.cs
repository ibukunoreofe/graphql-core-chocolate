using GraphQL101HotChocolate.Models;
using Microsoft.EntityFrameworkCore;
namespace GraphQL101HotChocolate.Schema.Queries
{
    /// <summary>
    /// 
    /// Injecting AppDbContext directly into the constructor of your GraphQL query class can lead to 
    /// issues with request scoping and DbContext lifecycle management, especially in the context of a 
    /// GraphQL server where multiple queries and mutations might be handled concurrently. 
    /// The preferred approach in HotChocolate for managing DbContext instances is to use a factory pattern, 
    /// as this aligns with the asynchronous and concurrent nature of GraphQL request handling.
    /// 
    /// </summary>
    public class Query
    {

        ///// <summary>
        ///// This approach is deprecated but works because ScopedService is deprecated here
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //[UseDbContext(typeof(AppDbContext))]
        //[UseProjection]
        //public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
        //{
        //    return context.Users;
        //}      

        /// <summary>
        /// Better Approach to fix the deprecation
        /// </summary>
        /// <param name="contextFactory"></param>
        /// <returns></returns>
        [UseProjection]
        public IQueryable<User> GetUsers([Service] IDbContextFactory<AppDbContext> contextFactory)
        {
            var context = contextFactory.CreateDbContext();
            return context.Users;
        }

        [GraphQLDeprecated("It was just for testing")]
        public string Hello() => "world";
    }
}
