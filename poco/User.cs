using FluentValidation;

public class User
{
  public User()
  {
    this.Name = "Empty";
    this.Email = "foo@bar.com";
  }
  public User(int Id, string Name, string Email)
  {
    this.Id = Id;
    this.Name = Name;
    this.Email = Email;
  }

  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }

}

public class UserValidator : AbstractValidator<User>
{
  public UserValidator()
  {
    RuleFor(x => x.Id).NotNull();
    RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(50);
    RuleFor(x => x.Email).NotNull().EmailAddress();
  }
}
