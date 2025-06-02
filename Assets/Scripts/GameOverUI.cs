using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    //[SerializeField] private TextMeshProUGUI recipeDeliveredText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreTextAtGameOver;

    private void Start()
    {
        gameOverPanel.SetActive(false); // 開始時不顯示 Game Over 畫面
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowGameOver(int recipesDelivered)
    {
        Time.timeScale = 0f; // 暫停遊戲
        gameOverPanel.SetActive(true);
        // recipeDeliveredText.text = "送出的食譜數量: " + recipesDelivered;
        scoreTextAtGameOver.text = "Final Score: " + ScoreManager.Instance.GetScore();

    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // 恢復時間
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // 恢復時間
        SceneManager.LoadScene("MainMenu"); // 確保你有一個名為 MainMenu 的場景
    }
}
