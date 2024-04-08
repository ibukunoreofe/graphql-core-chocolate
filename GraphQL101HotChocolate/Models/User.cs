namespace GraphQL101HotChocolate.Models;

public partial class User
{
    public int Id { get; set; }

    [GraphQLNonNullType]
    public string Username { get; set; } = null!;

    [GraphQLName("email")]
    [GraphQLNonNullType]
    public required string Email { get; set; }

    public DateTime CreatedDate { get; set; }

}
