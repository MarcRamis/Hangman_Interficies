public class ButtonCheckedEvent
{
    public readonly bool isCorrect;

    public ButtonCheckedEvent(bool _isCorrect)
    {
        isCorrect = _isCorrect;
    }
}