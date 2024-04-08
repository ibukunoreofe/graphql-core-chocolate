namespace GraphQL101HotChocolate.Schema.Mutations.Inputs
{
    public class UpdateUserEmailInput
    {
        public int Id { get; set; }
        public required string NewEmail { get; set; }

        public string? Username { get; set; } // Optional username field
    }
}
