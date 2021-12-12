using UnityEngine;

public class LoginUseCase : ILoginUseCase
{
    private readonly IFirebaseLogService _firebaseLogService;
    private readonly IEventDispatcherService _eventDispatcherService;

    public LoginUseCase(IFirebaseLogService firebaseLogService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseLogService = firebaseLogService;
        _eventDispatcherService = eventDispatcherService;

        eventDispatcherService.Subscribe<bool>(AlreadyConnected);
    }

    public void AlreadyConnected(bool data)
    {
        if (data)
        {
            //LoginEmail();

            _eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
            Debug.Log("User exists");
        }
        else
        {
            LoginAnonym();

            _eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
            Debug.Log("User doesn't exist");
        }
    }

    public void LoginEmail(UserNameLog userNameLog)
    {
        _firebaseLogService.LogEmail(userNameLog);
    }

    public void LoginAnonym()
    {
        _firebaseLogService.LogAnonym();
    }
}