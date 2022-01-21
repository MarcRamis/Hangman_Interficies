using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
public class SendMessageUseCase : ISendMessageUseCase
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IFireBaseMessageService _fireBaseMessageService;

    public SendMessageUseCase(IFireBaseMessageService fireBaseMessageService, IEventDispatcherService eventDispatcherService)
    {
        _eventDispatcherService = eventDispatcherService;
        _fireBaseMessageService = fireBaseMessageService;
    }

    public void Init()
    {
        _fireBaseMessageService.Init();
    }

    public IEnumerator Init(float time)
    {
        yield return new WaitForSeconds(time);
        _fireBaseMessageService.Init();
    }
    public async Task InitByTask()
    {
        await _fireBaseMessageService.InitByTask();
    }

    public void End()
    {
        _fireBaseMessageService.UnSuscribe();
    }
}
