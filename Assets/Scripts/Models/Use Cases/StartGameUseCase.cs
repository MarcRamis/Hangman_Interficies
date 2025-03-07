﻿using System.Threading.Tasks;
using System;

public class StartGameUseCase : IDisposable
{
    IEventDispatcherService _eventDispatcher;
    IHangmanAPIService _hangmanAPIService;
    IGoogleMobileAdsService _googleMobileAdsService;

    public StartGameUseCase(IEventDispatcherService eventDispatcher, IHangmanAPIService hangmanAPIService, IGoogleMobileAdsService googleMobileAdsService)
    {
        _eventDispatcher = eventDispatcher;
        _hangmanAPIService = hangmanAPIService;
        _googleMobileAdsService = googleMobileAdsService;
    }

    public async Task StartGame()
    {
        await _googleMobileAdsService.LoadReward();
        await _hangmanAPIService.GetRandomLetter();
        _hangmanAPIService.GetButtonLetters();
        
        _eventDispatcher.Dispatch<LoadScreenEvent>();
    }
    public void Dispose()
    {

    }
}
