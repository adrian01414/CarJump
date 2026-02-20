using System;

public class ScoreEventHandler : IDisposable
{
    private Score _score;

    public ScoreEventHandler(Score score)
    {
        _score = score;

        Platform.OnPlayerLanded += IncreaseScore;
    }

    private void IncreaseScore()
    {
        _score.Increase();
    }

    public void Dispose()
    {
        Platform.OnPlayerLanded -= IncreaseScore;
    }
}
