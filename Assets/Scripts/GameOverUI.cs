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
        gameOverPanel.SetActive(false); // �}�l�ɤ���� Game Over �e��
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowGameOver(int recipesDelivered)
    {
        Time.timeScale = 0f; // �Ȱ��C��
        gameOverPanel.SetActive(true);
        // recipeDeliveredText.text = "�e�X�����мƶq: " + recipesDelivered;
        scoreTextAtGameOver.text = "Final Score: " + ScoreManager.Instance.GetScore();

    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // ��_�ɶ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // ��_�ɶ�
        SceneManager.LoadScene("MainMenu"); // �T�O�A���@�ӦW�� MainMenu ������
    }
}
