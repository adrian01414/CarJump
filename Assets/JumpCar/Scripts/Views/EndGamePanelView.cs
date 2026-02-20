using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePanelView : MonoBehaviour
{
    public Button RestartButton;
    public Text CurrentScoreText;
    public Text BestScoreText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
