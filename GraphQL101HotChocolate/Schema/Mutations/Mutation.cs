using GraphQL101HotChocolate.Models;
using GraphQL101HotChocolate.Schema.Mutations.Inputs;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL101HotChocolate.Schema.Mutations
{
    public class Mutation
    {
        public async Task<User> UpdateUserEmailAsync(UpdateUserEmailInput input, [Service] IDbContextFactory<AppDbContext> contextFactory, [Service] ITopicEventSender topicEventSender)
        {
            using var context = contextFactory.CreateDbContext();

            var user = await context.Users.FindAsync(input.Id) ?? throw new Exception("User not found");
            user.Email = input.NewEmail;

            // Update username only if it's provided
            if (!string.IsNullOrEmpty(input.Username))
            {
                user.Username = input.Username;
            }

            context.Users.Update(user);
            await context.SaveChangesAsync();

            // works
            await topicEventSender.SendAsync( $"{user.Id}_{nameof(Subscriptions.Subscription.UserUpdated)}", user);

            // // testing generics - won't work because it won't be able to resolve the function name
            // await topicEventSender.SendAsync( $"{user.Id}__user_updated_broadcast", user);

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id, [Service] IDbContextFactory<AppDbContext> contextFactory)
        {
            using var context = contextFactory.CreateDbContext();

            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                // Optionally handle the case where the user doesn't exist.
                // You might throw an exception or simply return false.
                return false;
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return true; // Return true to indicate the user was successfully deleted.
        }

        public async Task<User> CreateUserAsync(CreateUserInput input, [Service] IDbContextFactory<AppDbContext> contextFactory, [Service] ITopicEventSender topicEventSender)
        {
            using var context = contextFactory.CreateDbContext();

            var newUser = new User
            {
                Username = input.Username,
                Email = input.Email,
                CreatedDate = DateTime.UtcNow // Assuming you want to set this upon creation
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            // Send out event
            await topicEventSender.SendAsync(nameof(Subscriptions.Subscription.UserCreated), newUser);

            return newUser;
        }
    }
}
