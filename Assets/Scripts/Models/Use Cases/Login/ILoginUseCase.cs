public interface ILoginUseCase
{
    void AlreadyConnected(LoggedEvent data);
    void LoginEmail(UserNameLog userNameLog);
    void LoginAnonym();
    UserNameLog GetCurrentUser();
}
