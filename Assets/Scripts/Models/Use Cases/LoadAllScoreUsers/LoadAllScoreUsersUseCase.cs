using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
public class LoadAllScoreUsersUseCase : ILoadAllScoreUsersUseCase
{
    private readonly IFirebaseRealtimeDatabaseService _firebaseRealtimeDatabaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    
    public LoadAllScoreUsersUseCase(IFirebaseRealtimeDatabaseService firebaseRealtimeDatabaseService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseRealtimeDatabaseService = firebaseRealtimeDatabaseService;
        _eventDispatcherService = eventDispatcherService;
    }

    public async Task GetAll()
    {
        await _firebaseRealtimeDatabaseService.ReadDataBase1();
    }

    public IEnumerator GetAll(float time)
    {
        yield return new WaitForSeconds(time);
        _firebaseRealtimeDatabaseService.ReadDataBase();
    }
}