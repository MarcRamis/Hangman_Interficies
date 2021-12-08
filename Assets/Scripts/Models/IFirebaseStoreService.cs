public interface IFirebaseStoreService
{
    void GetCurrentUserName();
    void StoreNewUserName(string newUserName);
    void LoadData();
}
