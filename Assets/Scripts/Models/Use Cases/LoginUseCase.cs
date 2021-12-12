using UnityEngine;

public class LoginUseCase : ILoginUseCase
{
    private readonly IFirebaseLogService _firebaseLogService;
    private readonly IEventDispatcherService _eventDispatcherService;

    public LoginUseCase(IFirebaseLogService firebaseLogService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseLogService = firebaseLogService;
        _eventDispatcherService = eventDispatcherService;

        _eventDispatcherService.Subscribe<bool>(AlreadyConnected);
    }

    public void AlreadyConnected(bool data)
    {
        if (data)
        {
            Debug.Log("User exists");
            
            //LoginEmail(_firebaseLogService.GetCurrentUser());
            _eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
        }
        else
        {
            Debug.Log("User doesn't exist");
            LoginAnonym();

            _eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
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
