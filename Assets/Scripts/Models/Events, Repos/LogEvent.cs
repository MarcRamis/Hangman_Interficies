public class LogEvent
{
    public readonly string Text;
    public readonly bool isLogged;

    public LogEvent(string text)
    {
        Text = text;
    }
}