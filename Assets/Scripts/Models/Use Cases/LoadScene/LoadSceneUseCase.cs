using System.Threading.Tasks;
using System;
public class LoadSceneUseCase : SceneLoader, IDisposable
{
    private readonly SceneHandlerService _sceneHandlerService;

    public LoadSceneUseCase(SceneHandlerService sceneHandlerService)
    {
        _sceneHandlerService = sceneHandlerService;
    }

    public async Task Load(string sceneName)
    {
        await _sceneHandlerService.LoadScene(sceneName);
    }
    
    public void Dispose()
    {

    }
}