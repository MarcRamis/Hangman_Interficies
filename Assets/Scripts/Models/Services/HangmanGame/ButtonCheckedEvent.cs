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

public class VaryLiveEvent
{
    public readonly int _live;

    public VaryLiveEvent(int live)
    {
        _live = live;
    }
}