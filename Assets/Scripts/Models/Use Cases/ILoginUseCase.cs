public interface ILoginUseCase
{
    void AlreadyConnected(bool data);
    void LoginEmail(UserNameLog userNameLog);
    void LoginAnonym();
}
