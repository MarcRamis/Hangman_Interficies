public interface IUpdateGameUseCase
{
    void CheckButton(string letter);
    void Reset(bool playerWin);
    void GoMenu(ScoreUserPrefs scoreuser);
    void UpdateTime(float delta);
}