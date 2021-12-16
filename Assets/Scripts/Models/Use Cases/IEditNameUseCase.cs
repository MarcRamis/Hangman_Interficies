using System.Collections;
public interface IEditNameUseCase
{
    void SetUserNameFromFirestore();

    void Edit(string changeName);

    public IEnumerator Init(float time);
}