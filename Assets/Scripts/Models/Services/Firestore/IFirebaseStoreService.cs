using System.Threading.Tasks;
public interface IFirebaseStoreService
{
    void GetCurrentUserName();
    Task GetCurrentUserNameByTask();
    void SetCurrentUserNameInHome();
    void StoreNewUserName(string newUserName);
    void LoadData();
}
