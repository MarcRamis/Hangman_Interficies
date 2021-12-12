public interface IFirebaseLogService
{
    void LogAnonym();
    void LogEmail(UserNameLog userNameLog);
    void RegisterEmail(UserNameLog userNameLog);
    void Logout();
    void SetDefaultData();
    void LoadData();
    string GetID();
}