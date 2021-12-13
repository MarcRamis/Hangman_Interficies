using System.Threading.Tasks;

public interface SceneLoader
{
    Task Load(string sceneName);
}