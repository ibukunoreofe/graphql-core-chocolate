namespace GraphQL101HotChocolate.Schema.Mutations.Inputs
{
    public class CreateUserInput
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
    }
}
