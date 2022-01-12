using System.Threading.Tasks;

public interface IHangmanAPIService
{
    Task GetRandomLetter();
    void GetButtonLetters();
    void GuessLetter(string letter);
    void GetSolution();
}
