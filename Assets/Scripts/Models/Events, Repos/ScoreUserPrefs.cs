public class ScoreUserPrefs
{
    public string Username;
    public int Position;
    public int Score;
    public float Timer;

    public ScoreUserPrefs()
    {
    }

    public ScoreUserPrefs(string userName)
    {
        Username = userName;
    }
    public ScoreUserPrefs(int score, float timer)
    {
        Score = score;
        Timer = timer;
    }
    public ScoreUserPrefs(string username, int score, float timer)
    {
        Username = username;
        Score = score;
        Timer = timer;
    }
    public ScoreUserPrefs(string username, int position, int score, float timer)
    {
        Username = username;
        Position = position;
        Score = score;
        Timer = timer;
    }
}