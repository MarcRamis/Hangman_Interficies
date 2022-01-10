using System.Threading.Tasks;
public class LoadInitialDataUseCase
{
    private readonly SceneLoader _sceneLoader;

    public LoadInitialDataUseCase(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    public async Task Init()
    {
        await _sceneLoader.Load("Menu");
    }
}