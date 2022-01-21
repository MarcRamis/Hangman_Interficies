using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using System;
public class LoadAllScoreUsersUseCase : ILoadAllScoreUsersUseCase,IDisposable
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
        //await _firebaseRealtimeDatabaseService.ReadDataBase1();
        await _firebaseRealtimeDatabaseService.OrderedListByScoreByTask();
    }

    public IEnumerator GetAll(float time)
    {
        yield return new WaitForSeconds(time);
        _firebaseRealtimeDatabaseService.ReadDataBase();
    }
    public void Dispose()
    {

    }
}