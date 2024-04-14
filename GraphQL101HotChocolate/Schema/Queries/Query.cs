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
        /// 
        /// query
        //{
        //    users(first: 3, after: "MA=="){
        //        edges{
        //            node{
        //                id
        //                username
        //                email
        //                createdDate
        //            }
        //            cursor
        //        }
        //        pageInfo{
        //            startCursor
        //            endCursor
        //        }
        //    }
        //}
        /// 
        /// 
        /// ORDER is important: UseDbContext -> UsePaging -> UseProjection -> UseFiltering -> UseSorting
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 2)]  
        [UseProjection]
        [UseSorting]
        public IQueryable<User> GetUsers([Service] IDbContextFactory<AppDbContext> contextFactory)
        {
            var context = contextFactory.CreateDbContext();
            return context.Users;
        }

        /// <summary>
        /// query
            //{
            //   offsetUsers (skip:1 , take: 2){
            //        items {
            //            id
            //            username
            //            email
            //            createdDate
            //        }
            //        pageInfo{
            //            hasNextPage
            //            hasPreviousPage
            //        }
            //        totalCount
            //    }
            //}
        /// </summary>
        /// <param name="contextFactory"></param>
        /// <returns></returns>
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 2)]
        [UseProjection] // Without this, it will select all columns in the table
        [UseFiltering]
        public IQueryable<User> GetOffsetUsers([Service] IDbContextFactory<AppDbContext> contextFactory)
        {
            var context = contextFactory.CreateDbContext();
            return context.Users.OrderBy(x => x.Id);
        }


        [GraphQLDeprecated("It was just for testing")]
        public string Hello() => "world";
    }
}
