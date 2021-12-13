using UnityEngine;

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

    public void End()
    {
        _fireBaseMessageService.UnSuscribe();
    }
}
