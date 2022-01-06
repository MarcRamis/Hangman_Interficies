using System.Threading.Tasks;
public interface IFirebaseStoreService
{
    void GetCurrentUserName();
    Task GetCurrentUserName1();
    void StoreNewUserName(string newUserName);
    void LoadData();
}
