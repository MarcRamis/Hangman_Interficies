using UnityEngine;
using System;

public class LoginUseCase : ILoginUseCase, IDisposable
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
            if (_firebaseLogService.GetCurrentUser() != null)
            {
                LoginEmail(_firebaseLogService.GetCurrentUser());
            }
        }
        else
        {
            LoginAnonym();
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

    public void Dispose()
    {
        _eventDispatcherService.Unsubscribe<LoggedEvent>(AlreadyConnected);
    }
}
