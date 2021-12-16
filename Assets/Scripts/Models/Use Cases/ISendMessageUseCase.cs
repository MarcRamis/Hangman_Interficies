using System.Collections;
public interface ISendMessageUseCase
{
    void Init();
    public IEnumerator Init(float time);
    void End();
}
