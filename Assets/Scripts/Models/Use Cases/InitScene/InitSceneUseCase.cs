using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class InitSceneUseCase : IInitSceneUseCase
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IFirebaseLogService _firebaseLogService;
    private readonly LoadInitialDataUseCase _loadInitialDataUseCase;
    private readonly IEditNameUseCase _editNameUseCase;
    public InitSceneUseCase(IEventDispatcherService eventDispatcherService, IFirebaseLogService firebaseLogService, LoadInitialDataUseCase loadInitialDataUseCase, IEditNameUseCase editNameUseCase)
    {
        _eventDispatcherService = eventDispatcherService;
        _firebaseLogService = firebaseLogService;
        _loadInitialDataUseCase = loadInitialDataUseCase;
        _editNameUseCase = editNameUseCase;
    }
    public async void Init()
    {
        await _firebaseLogService.InitByTask();
        await _editNameUseCase.InitByTask();
        await _loadInitialDataUseCase.Init();
    }
}
