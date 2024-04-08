# GraphQL API with HotChocolate, ASP.NET Core, and EF Core

This project implements a GraphQL API using HotChocolate, ASP.NET Core, and Entity Framework Core. It demonstrates CRUD operations for a `User` entity, showcasing how to create, read, update, and delete users in a SQL Server database through GraphQL queries and mutations.
Using Banana Cake Pop which is an incredible, beautiful, and feature-rich GraphQL IDE Playground

## Features

- **Create User**: Add new users to the database.
- **Read User**: Retrieve user information using their ID or list all users with filtering options.
- **Update User**: Modify existing user details, such as email and username.
- **Delete User**: Remove users from the database.

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- SQL Server (LocalDB or a server instance)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://your-repository-url.git
   cd your-project-directory
   ```
2. **Install dependencies**
   ```bash
   dotnet restore
   ```
3. **Configure the database connection**
   Edit the `appsettings.json` file to include your SQL Server connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server_address;Database=your_database_name;User Id=your_username;Password=your_password;"
   }
   ```
4. **Apply migrations**
   ```bash
   dotnet ef database update
   ```
5. **Run the application**
   ```bash
   dotnet run
   ```
   The GraphQL API is now accessible at `http://localhost:5000/graphql`.

## Using the API

You can interact with the API using any GraphQL client or the built-in Banana Cake Pop IDE at the `/graphql` endpoint.

### Queries

- **GetUserById**: Retrieve a single user by ID.
  ```graphql
  query {
    user(id: 1) {
      id
      username
      email
      createdDate
    }
  }
  ```
- **GetUsers**: List all users (with optional filtering).
  ```graphql
  query {
    users {
      id
      username
      email
      createdDate
    }
  }
  ```

### Mutations

- **CreateUser**: Add a new user.
  ```graphql
  mutation {
    createUser(input: { username: "newUser", email: "newUser@example.com" }) {
      id
      username
      email
      createdDate
    }
  }
  ```
- **UpdateUserEmail**: Update a user's email.
  ```graphql
  mutation {
    updateUserEmail(input: { id: 1, newEmail: "updatedEmail@example.com" }) {
      id
      username
      email
      createdDate
    }
  }
  ```
- **DeleteUser**: Remove a user by ID.
  ```graphql
  mutation {
    deleteUser(id: 1)
  }
  ```

## Architecture

This project is structured following the Clean Architecture principles, with separate layers for the API, Domain, and Infrastructure. Dependency Injection is used extensively to manage dependencies.

## Contributing

Contributions are welcome! Please open an issue to discuss your idea or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
