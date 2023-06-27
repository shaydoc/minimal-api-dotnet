static class UserEndpoint
{
  public static RouteGroupBuilder MapGroupOneUserApi(this RouteGroupBuilder group)
  {
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-7.0
    // Do call all data access APIs asynchronously.
    group.MapGet("/", async (IUserService service) => await service.GetUsersAsync())
    .WithName("List All Users v1")
    .WithOpenApi()
    .Produces<List<User>>(StatusCodes.Status200OK);
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-7.0
    // Do call all data access APIs asynchronously.
    group.MapGet("/{id:int}", async (IUserService service, int Id) =>
    {
      User? user = await service.GetUserAsync(Id);
      return user == null ? Results.NotFound() : Results.Ok(user);
    }).WithName("Get a single users v1")
    .WithOpenApi()
    .Produces<User>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

    group.MapPost("/", (IUserService service, User user) =>
    {
      var newUser = service.CreateUser(user, out var validationResult);
      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }

      return Results.Created($"/api/v1/users/{newUser.Id}", newUser);
    }).WithName("Create a user v1")
    .WithOpenApi()
    .Produces<User>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

    group.MapPut("/{id:int}", (IUserService service, int Id, User user) =>
    {
      var updatedUser = service.UpdateUser(Id, user, out var validationResult);
      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }

      if (updatedUser == null)
      {
        return Results.NotFound();
      }

      return Results.Ok(updatedUser);
    }).WithName("Update a user v1")
    .WithOpenApi()
    .Produces<User>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);

    group.MapDelete("/{id:int}", (IUserRepository repository, int Id) =>
      {
        var deleted = repository.DeleteUser(Id);

        // No need to the client know if the user doesn't exist
        // if (!deleted)
        // {
        //   return Results.NotFound();
        // }

        return Results.NoContent();
      }).WithName("Delete a user v1")
      .WithOpenApi()
      .Produces(StatusCodes.Status204NoContent);

    return group;
  }
}
