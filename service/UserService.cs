using FluentValidation;
using FluentValidation.Results;


public interface IUserService
{
  /// <summary>
  ///  Get all users from the Table Async
  /// </summary>
  /// <returns>
  /// returns a list of users
  /// </returns>
  Task<List<User>> GetUsersAsync();
  /// <summary>
  /// Gets a single user from the Table Async
  /// </summary>
  /// <param name="Id">integer Id of the User</param>
  /// <returns>a user if one is found else null</returns>

  Task<User?> GetUserAsync(int Id);
  /// <summary>
  ///  Creates a new user in the Table
  /// </summary>
  /// <param name="user">User object</param>
  /// <param name="validationResult">output parameter with the results of the validation check</param>
  /// <returns>
  /// returns the user if the validation is successful else returns the user with the validation errors
  /// </returns>

  User CreateUser(User user, out ValidationResult validationResult);
  /// <summary>
  /// Updates a user in the Table
  /// </summary>
  /// <param name="Id">Id of the user to update</param>
  /// <param name="user">User Object</param>
  /// <param name="validationResult">Output parameter with validation results</param>
  /// <returns>
  /// returns the user if the validation is successful else returns the user with the validation errors
  /// else returns null if the user is not found
  /// </returns>

  User? UpdateUser(int Id, User user, out ValidationResult validationResult);
  /// <summary>
  /// Deletes the user from the Table
  /// </summary>
  /// <param name="Id">Id of the user to delete</param>
  /// <returns>
  /// returns true if the user is deleted else returns false, it may return false if the user is not found
  /// </returns>
  bool DeleteUser(int Id);
}

public class UserService : IUserService
{

  private readonly IValidator<User> validator;
  private readonly IUserRepository repository;

  public UserService(IValidator<User> validator, IUserRepository repository)
  {
    this.validator = validator;
    this.repository = repository;
  }

  /// <summary>
  ///  Get all users from the Table Async
  /// </summary>
  /// <returns>
  /// returns a list of users
  /// </returns>
  public Task<List<User>> GetUsersAsync()
  {
    return this.repository.GetUsersAsync();
  }

  /// <summary>
  /// Gets a single user from the Table Async
  /// </summary>
  /// <param name="Id">integer Id of the User</param>
  /// <returns>a user if one is found else null</returns>
  public Task<User?> GetUserAsync(int Id)
  {
    return this.repository.GetUserAsync(Id);
  }

  /// <summary>
  ///  Creates a new user in the Table
  /// </summary>
  /// <param name="user">User object</param>
  /// <param name="validationResult">output parameter with the results of the validation check</param>
  /// <returns>
  /// returns the user if the validation is successful else returns the user with the validation errors
  /// </returns>
  public User CreateUser(User user, out ValidationResult validationResult)
  {
    validationResult = validator.Validate(user);
    if (!validationResult.IsValid)
    {
      return user;
    }

    // Check user already exists
    User? existingUser = repository.FindUserByEmail(user.Email);

    if (existingUser != null)
    {
      validationResult.Errors.Add(new ValidationFailure("Name", "User already exists"));
      return user;
    }

    return this.repository.CreateUser(user);
  }

  /// <summary>
  /// Updates a user in the Table
  /// </summary>
  /// <param name="Id">Id of the user to update</param>
  /// <param name="user">User Object</param>
  /// <param name="validationResult">Output parameter with validation results</param>
  /// <returns>
  /// returns the user if the validation is successful else returns the user with the validation errors
  /// else returns null if the user is not found
  /// </returns>
  public User? UpdateUser(int Id, User user, out ValidationResult validationResult)
  {
    validationResult = validator.Validate(user);
    if (!validationResult.IsValid)
    {
      return null;
    }

    User? updateUser = repository.GetUserAsync(Id).Result;

    if (updateUser == null)
    {
      return null;
    }

    return this.repository.UpdateUser(Id, user);
  }

  /// <summary>
  /// Deletes the user from the Table
  /// </summary>
  /// <param name="Id">Id of the user to delete</param>
  /// <returns>
  /// returns true if the user is deleted else returns false, it may return false if the user is not found
  /// </returns>
  public bool DeleteUser(int Id)
  {
    return this.repository.DeleteUser(Id);
  }

}
