public interface IEditNameUseCase
{
    void SetUserNameFromFirestore();

    void Edit(string changeName);
}