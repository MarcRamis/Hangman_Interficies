using UnityEngine;

public class LoginUseCase : ILoginUseCase
{
    private readonly IFirebaseLogService _firebaseLogService;
    private readonly IEventDispatcherService _eventDispatcherService;

    public LoginUseCase(IFirebaseLogService firebaseLogService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseLogService = firebaseLogService;
        _eventDispatcherService = eventDispatcherService;

        _eventDispatcherService.Subscribe<LoggedEvent>(AlreadyConnected);
    }

    public void AlreadyConnected(LoggedEvent data)
    {
        if (data.isLogged)
        {
            Debug.Log("User exists");
            
            if (_firebaseLogService.GetCurrentUser() != null)
            {
                LoginEmail(_firebaseLogService.GetCurrentUser());
                Debug.Log("With Email");
            }
            //_eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
        }
        else
        {
            Debug.Log("User doesn't exist");
            LoginAnonym();

            //_eventDispatcherService.Dispatch(new LogEvent(_firebaseLogService.GetID()));
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

    public UserNameLog GetCurrentUser()
    {
        return _firebaseLogService.GetCurrentUser();
    }
}
