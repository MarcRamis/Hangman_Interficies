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
        //_eventDispatcherService.Subscribe<>
    }

    public void AlreadyConnected(LoggedEvent data)
    {
        if (data.isLogged)
        {
            if (_firebaseLogService.GetCurrentUser() != null)
            {
                LoginEmail(_firebaseLogService.GetCurrentUser());
            }
            //Debug.Log("Entro en el if");
            //Debug.Log("Current user: " + Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            //ServiceLocator.Instance.playerInfo.SetUserID(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            //_eventDispatcherService.Dispatch<CurrentNameEvent>();
            LoginAnonym(); //Quitar esto a futuro
        }
        else
        {
            LoginAnonym();
        }
        Debug.Log("Entro Aqui");
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
