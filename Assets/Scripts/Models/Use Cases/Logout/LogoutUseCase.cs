using System;

public class LogoutUseCase : ILogoutUseCase, IDisposable
{
    IEventDispatcherService _eventDispatcher;
    IFirebaseLogService _firebaseLog;

    public LogoutUseCase(IEventDispatcherService eventDispatcher, IFirebaseLogService firebaseLog)
    {
        _eventDispatcher = eventDispatcher;
        _firebaseLog = firebaseLog;
    }

    public void Logout()
    {
        _firebaseLog.Logout();
    }
    public void Dispose()
    {
    }
}