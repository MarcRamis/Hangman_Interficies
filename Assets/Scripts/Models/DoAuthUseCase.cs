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

    public void Log()
    {
        firebaseLogService.Login();
    }
    public void AlreadyConnected(bool data)
    {
        if (data)
        {
            Debug.Log("User exists");
            eventDispatcherService.Dispatch(new LogEvent(firebaseLogService.GetID()));
        }
        else
        {
            Log();
            eventDispatcherService.Dispatch(new LogEvent(firebaseLogService.GetID()));
            Debug.Log("User doesn't exist");
        }
    }
}