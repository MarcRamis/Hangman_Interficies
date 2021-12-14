public class LoadInitialDataUseCase
{
    private readonly SceneLoader _sceneLoader;

    public LoadInitialDataUseCase(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    public async void Init()
    {
        await _sceneLoader.Load("Menu");
    }
}