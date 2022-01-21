using System.Collections;
using System.Threading.Tasks;
public interface IEditNameUseCase
{
    void SetUserNameFromFirestore();

    void Edit(string changeName);

    public IEnumerator Init(float time);

    Task InitByTask();

    void GetUserName();
}