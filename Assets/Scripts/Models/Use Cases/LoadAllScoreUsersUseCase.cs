public class LoadAllScoreUsersUseCase : ILoadAllScoreUsersUseCase
{
    private readonly IFirebaseRealtimeDatabaseService _firebaseRealtimeDatabaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    
    public LoadAllScoreUsersUseCase(IFirebaseRealtimeDatabaseService firebaseRealtimeDatabaseService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseRealtimeDatabaseService = firebaseRealtimeDatabaseService;
        _eventDispatcherService = eventDispatcherService;
    }

    public void GetAll()
    {
        _firebaseRealtimeDatabaseService.OrderedListByScore();
    }
}