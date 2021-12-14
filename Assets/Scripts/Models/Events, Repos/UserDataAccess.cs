public interface UserDataAccess
{
    UserEntity GetLocalUser();
    void SetLocarUser(UserEntity userEntity);
}
