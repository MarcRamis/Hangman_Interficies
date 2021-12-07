public class UserNameLogEvent
{
    private string email;
    private string password;

    public UserNameLogEvent(string _email, string _password)
    {
        email = _email;
        password = _password;
    }
}