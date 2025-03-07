﻿using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;

    [SerializeField] private InitView _initPrefab;
    FirebaseLogService _firebaseLogService;
    LoadInitialDataUseCase _loadInitialDataUseCase;
    InitSceneUseCase _initSceneUseCase;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        var initView = Instantiate(_initPrefab, _canvasParent);

        var initViewModel = new InitViewModel().AddTo(_disposables);
        initView.SetViewModel(initViewModel);

        var sceneHandlerService = new UnitySceneHandler();
        //var userRepository = new UserRepository();

        //ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);
        //ServiceLocator.Instance.RegisterService<UserDataAccess>(userRepository);

        var eventDispatcher = new EventDispatcherService();
        _firebaseLogService = new FirebaseLogService(eventDispatcher);
        var firebaseStoreService = new FirebaseStoreService(eventDispatcher);

        var loginUseCase = new LoginUseCase(_firebaseLogService, eventDispatcher).AddTo(_disposables);
        var loadSceneUseCase = new LoadSceneUseCase(sceneHandlerService);
        var editNameUseCase = new EditNameUseCase(firebaseStoreService, eventDispatcher);

        _loadInitialDataUseCase = new LoadInitialDataUseCase(loadSceneUseCase);

        _initSceneUseCase = new InitSceneUseCase(eventDispatcher, _firebaseLogService, _loadInitialDataUseCase, editNameUseCase);

        new InitController(initViewModel).AddTo(_disposables);
        new InitPresenter(initViewModel, eventDispatcher).AddTo(_disposables);
    }

    private void Start()
    {
        _initSceneUseCase.Init();
        //await _firebaseLogService.InitByTask();
        //await _loadInitialDataUseCase.Init();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}