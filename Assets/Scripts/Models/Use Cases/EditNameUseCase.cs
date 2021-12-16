using System.Collections;
using UnityEngine;
public class EditNameUseCase : IEditNameUseCase
{
    private readonly IFirebaseStoreService _firebaseStoreService;
    private readonly IEventDispatcherService _eventDispatcherService;

    public EditNameUseCase(IFirebaseStoreService firebaseStoreService, IEventDispatcherService eventDispatcherService)
    {
        _firebaseStoreService = firebaseStoreService;
        _eventDispatcherService = eventDispatcherService;
    }
    public void SetUserNameFromFirestore()
    {
        _firebaseStoreService.GetCurrentUserName();
    }

    public void Edit(string changeName)
    {
        _firebaseStoreService.StoreNewUserName(changeName);
        _firebaseStoreService.GetCurrentUserName();
    }

    public IEnumerator Init(float time)
    {
        yield return new WaitForSeconds(time);
        _firebaseStoreService.GetCurrentUserName();
    }
}