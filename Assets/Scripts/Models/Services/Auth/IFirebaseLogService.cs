using System.Collections;
using System.Threading.Tasks;
public interface IFirebaseLogService
{
    void Init();
    Task Init3();
    public IEnumerator Init(float time);
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