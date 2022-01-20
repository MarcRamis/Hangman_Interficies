public interface IUpdateGameUseCase
{
    void CheckButton(string letter);
    void Reset(bool playerWin);
}