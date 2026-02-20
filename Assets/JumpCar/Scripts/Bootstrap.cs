using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public CounterView CounterView;
    public EndGamePanelView EndGamePanelView;

    private ScoreEventHandler _scoreEventHandler;
    private ScorePresenter _scorePresenter;

    private EndGamePanelPresenter _endGamePanelPresenter;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Score score = new(PlayerPrefs.GetInt("PlayerScore", 0));

        _scoreEventHandler = new(score);

        _scorePresenter = new(score, CounterView);

        _endGamePanelPresenter = new(score, EndGamePanelView);
    }

    private void OnDisable()
    {
        _scoreEventHandler.Dispose();
        _scorePresenter.Dispose();
        _endGamePanelPresenter.Dispose();
    }
}
