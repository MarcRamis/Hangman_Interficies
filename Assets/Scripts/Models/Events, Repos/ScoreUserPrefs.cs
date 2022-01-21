public class ScoreUserPrefs
{
    public string Username;
    public int Position;
    public int Score;
    public int Timer;

    public ScoreUserPrefs()
    {
    }

    public ScoreUserPrefs(string userName)
    {
        Username = userName;
    }
    public ScoreUserPrefs(int score, int timer)
    {
        Score = score;
        Timer = timer;
    }
    public ScoreUserPrefs(string username, int score, int timer)
    {
        Username = username;
        Score = score;
        Timer = timer;
    }
    public ScoreUserPrefs(string username, int position, int score, int timer)
    {
        Username = username;
        Position = position;
        Score = score;
        Timer = timer;
    }
}