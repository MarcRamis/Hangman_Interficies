public class ButtonCheckedEvent
{
    public readonly bool isCorrect;
    public readonly string letterID;

    public ButtonCheckedEvent(bool _isCorrect, string _letterID)
    {
        isCorrect = _isCorrect;
        letterID = _letterID;
    }
}