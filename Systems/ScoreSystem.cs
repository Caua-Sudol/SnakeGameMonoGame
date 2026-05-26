namespace SnakeGameMonoGame;

public class ScoreSystem
{
    public int CountScore { get; private set; } = 0;

    public string Score
    {
        get { return $"Score: {CountScore}"; }
    }

    public void AddScore()
    {
        CountScore += 1;
    }
}
