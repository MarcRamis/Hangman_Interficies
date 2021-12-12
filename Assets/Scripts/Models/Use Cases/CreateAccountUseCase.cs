public class CreateAccountUseCase : ICreateAccountUseCase
{
    IEventDispatcherService _eventDispatcher;
    IFirebaseLogService _firebaseLog;

    public CreateAccountUseCase(IEventDispatcherService eventDispatcher, IFirebaseLogService firebaseLog)
    {
        _eventDispatcher = eventDispatcher;
        _firebaseLog = firebaseLog;
    }

    public void Register(UserNameLog userNameLog)
    {
        _firebaseLog.RegisterEmail(userNameLog);
    }
}