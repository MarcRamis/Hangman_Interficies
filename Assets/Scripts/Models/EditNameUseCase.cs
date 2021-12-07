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
}