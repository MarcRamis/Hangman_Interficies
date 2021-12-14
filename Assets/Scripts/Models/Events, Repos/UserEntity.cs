public class UserEntity
{
    public readonly string UserId;
    public string Name { get; private set; }

    public UserEntity(string userid, string name)
    {
        UserId = userid;
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}