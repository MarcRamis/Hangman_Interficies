using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class UnitySceneHandler : SceneHandlerService
{
    public async Task LoadScene(string scene)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        await Task.Yield();
    }
}
