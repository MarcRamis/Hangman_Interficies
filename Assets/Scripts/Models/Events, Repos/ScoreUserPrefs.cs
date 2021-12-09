public class ScoreUserPrefs
{
    public string Username;
    public int Position;
    public int Score;
    public float Timer;

    public ScoreUserPrefs()
    {
    }

    public ScoreUserPrefs(string username, int position, int score, float timer)
    {
        Username = username;
        Position = position;
        Score = score;
        Timer = timer;
    }
}