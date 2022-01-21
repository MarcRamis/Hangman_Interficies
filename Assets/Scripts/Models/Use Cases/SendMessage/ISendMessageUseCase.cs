using System.Collections;
using System.Threading.Tasks;
public interface ISendMessageUseCase
{
    void Init();
    Task InitByTask();
    public IEnumerator Init(float time);
    void End();
}
