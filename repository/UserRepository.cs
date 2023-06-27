
using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
  User? FindUserByEmail(string name);
  Task<List<User>> GetUsersAsync();
  Task<User?> GetUserAsync(int Id);
  User CreateUser(User user);
  User? UpdateUser(int Id, User user);
  bool DeleteUser(int Id);
}

public class UserRepository : IUserRepository
{

  private readonly UserContext db;
  public UserRepository(UserContext db)
  {
    this.db = db;
  }


  public User? FindUserByEmail(string email)
  {
    return this.db.Users.FirstOrDefault(u => u.Email == email);
  }
  public Task<List<User>> GetUsersAsync()
  {
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-7.0#return-ienumerablet-or-iasyncenumerablet
    // use ToListAsync before returning the enumerable.
    return this.db.Users.ToListAsync();
  }

  public Task<User?> GetUserAsync(int Id)
  {
    // do read operations asynchronously
    return this.db.Users.FirstOrDefaultAsync(u => u.Id == Id);
  }

  public User CreateUser(User user)
  {
    db.Users.Add(user);
    db.SaveChanges(true);
    return user;
  }

  public User? UpdateUser(int Id, User user)
  {
    User? updateUser = db.Users.FirstOrDefault(u => u.Id == Id);
    if (updateUser == null || user == null || user.Name == null)
    {
      return user;
    }
    updateUser.Name = user.Name ?? updateUser.Name; // add null check here
    updateUser.Email = user.Email ?? updateUser.Email; // add null check here
    db.SaveChanges();
    return updateUser;
  }

  public bool DeleteUser(int Id)
  {
    User? user = db.Users.FirstOrDefault(u => u.Id == Id);
    if (user == null)
    {
      return false;
    }
    db.Users.Remove(user);
    db.SaveChanges();
    return true;
  }
}
