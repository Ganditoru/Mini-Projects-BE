namespace MiniProject.Modules.Authentification.Domain.Users;

public sealed class User
{
    public User() { }

    public User(string name, string email,string username, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        UserName = username;
        Password = password;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string UserName { get; set; }

}

