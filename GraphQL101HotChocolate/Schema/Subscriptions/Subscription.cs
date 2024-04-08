using GraphQL101HotChocolate.Models;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL101HotChocolate.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public User UserCreated([EventMessage] User user) => user;

        // This works except this attribute is deprecated
        // TODO: How to do this on latest version without the deprecate attribute
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<User>> UserUpdated(int userId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            return topicEventReceiver.SubscribeAsync<User>($"{userId}_{nameof(UserUpdated)}");
        }

        //  // Won't work because it requires matching function name to resolve
        //  //Testing without the name of part, using generic names
        //[SubscribeAndResolve]
        //public ValueTask<ISourceStream<User>> UserUpdated(int userId, [Service] ITopicEventReceiver topicEventReceiver)
        //{
        //    return topicEventReceiver.SubscribeAsync<User>($"{userId}_user_updated_broadcast");
        //}
    }
}
