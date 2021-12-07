using UnityEngine;

public class DoAuthUseCase : IDoAuthUseCase
{
    private readonly IFirebaseLogService firebaseLogService;
    private readonly IEventDispatcherService eventDispatcherService;

    public DoAuthUseCase(IFirebaseLogService _firebaseLogService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLogService = _firebaseLogService;
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<bool>(AlreadyConnected);
    }

    public void AlreadyConnected(bool data)
    {
        if (data)
        {
            eventDispatcherService.Dispatch(new LogEvent(firebaseLogService.GetID()));
            Debug.Log("User exists");
        }
        else
        {
            firebaseLogService.Log();
            firebaseLogService.SetData();
            
            eventDispatcherService.Dispatch(new LogEvent(firebaseLogService.GetID()));
            Debug.Log("User doesn't exist");
        }
    }
}