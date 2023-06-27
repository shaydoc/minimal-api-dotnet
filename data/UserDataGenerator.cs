using System;
using System.Collections.Generic;


public static class UserDataGenerator
{
  private static readonly string[] FirstNames = {
            "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eve", "Frank",
            "Grace", "Hannah", "Ivan", "Jack", "Kelly", "Liam", "Mia", "Noah",
            "Olivia", "Paul", "Quincy", "Rachel", "Sarah", "Tom", "Uma", "Victor",
            "Wendy", "Xavier", "Yvonne", "Zack"
        };

  private static readonly string[] LastNames = {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller",
            "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez",
            "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Clark", "Lewis", "Robinson"
        };

  private static readonly Random Random = new Random();

  public static List<UserData> GenerateRandomNames(int n)
  {
    var userData = new List<UserData>();

    for (int i = 0; i < n; i++)
    {
      string firstName = FirstNames[Random.Next(FirstNames.Length)];
      string lastName = LastNames[Random.Next(LastNames.Length)];
      userData.Add(new UserData(firstName, lastName));
    }

    return userData;
  }

  public static string GenerateRandomUser()
  {
    string firstName = FirstNames[Random.Next(FirstNames.Length)];
    return firstName;
  }
}

public record UserData(string FirstName, string LastName);

