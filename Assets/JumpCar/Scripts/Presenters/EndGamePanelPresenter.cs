using System;
using UnityEngine;

public class EndGamePanelPresenter : IDisposable
{
    private Score _score;
    private EndGamePanelView _view;

    public EndGamePanelPresenter(Score score, EndGamePanelView view)
    {
        _score = score;
        _view = view;

        PlayerController.OnPlayerDied += ShowPanel;
        _view.RestartButton.onClick.AddListener(_view.RestartLevel);
    }

    public void ShowPanel()
    {
        PlayerPrefs.SetInt("PlayerScore", _score.MaxValue); //

        _view.ShowPanel();
        _view.CurrentScoreText.text = _score.Value.ToString();
        _view.BestScoreText.text = _score.MaxValue.ToString();
    }

    public void Dispose()
    {
        PlayerController.OnPlayerDied -= ShowPanel;
        _view.RestartButton.onClick.RemoveListener(_view.RestartLevel);
    }
}
