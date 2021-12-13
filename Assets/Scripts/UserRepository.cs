public class UserRepository : UserDataAccess
{
    private UserEntity _userEntity;

    public UserEntity GetLocalUser()
    {
        return _userEntity;
    }
    public void SetLocarUser(UserEntity userEntity)
    {
        _userEntity = userEntity;
    }
}
