public class Repository
{
    private string userName;

    private string userID;

    public string GetUserName() { return userName; }
    public void SetUserName(string _userName)
    {
        userName = _userName;
    }
    public string GetUserID() { return userID; }
    public void SetUserID(string _userID)
    {
        userID = _userID;
    }
}

