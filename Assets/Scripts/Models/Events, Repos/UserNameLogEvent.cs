public class UserNameLogEvent
{
    public readonly bool UserLogged;
    public readonly string Email;
    public readonly string Password;

    public UserNameLogEvent(bool userLogged, string email, string password)
    {
        UserLogged = userLogged;
        Email = email;
        Password = password;
    }
}
