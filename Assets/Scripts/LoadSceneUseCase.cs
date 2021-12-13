using System.Threading.Tasks;

public class LoadSceneUseCase : SceneLoader
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
}