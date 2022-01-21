using System.Threading.Tasks;
public interface IFireBaseMessageService
{
    void Init();
    Task InitByTask();
    void UnSuscribe();
}
