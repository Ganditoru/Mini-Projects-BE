namespace MiniProject.Modules.Authentification.Domain.Users;

public sealed class User
{
    private User() { }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string UserName { get; set; }


    public static User Create(string name, string email, string username, string password)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            UserName = username,
            Password = password
        };

        return user;
    }
}

