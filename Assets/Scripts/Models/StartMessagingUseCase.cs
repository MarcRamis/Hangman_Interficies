using UnityEngine;

public class StartMessagingUseCase : IStartMessagingUseCase
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IFireBaseMessageService _fireBaseMessageService;

    public StartMessagingUseCase(IFireBaseMessageService fireBaseMessageService, IEventDispatcherService eventDispatcherService)
    {
        _eventDispatcherService = eventDispatcherService;
        _fireBaseMessageService = fireBaseMessageService;
    }

    public void Init()
    {
        _fireBaseMessageService.Init();
    }
}
