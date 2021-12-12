public interface IFirebaseLogService
{
    void Init();
    void LogAnonym();
    void LogEmail(UserNameLog userNameLog);
    void RegisterEmail(UserNameLog userNameLog);
    void Logout();
    void SetDefaultData();
    void LoadData();
    string GetID();
    void SetCurrentUser();
    UserNameLog GetCurrentUser();
}