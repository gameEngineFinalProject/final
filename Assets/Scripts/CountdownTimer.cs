using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // 倒數時間（秒）
    private float currentTime;

    public TextMeshProUGUI countdownText; // 拖入 UI Text 物件
    private bool isGameStopped = false;

    void Start()
    {
        currentTime = countdownTime;
        Time.timeScale = 1f; // 確保遊戲一開始是正常速度
    }

    void Update()
    {
        if (isGameStopped) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            countdownText.text = /*"倒數：" +*/ Mathf.CeilToInt(currentTime).ToString();
        }
        else
        {
            countdownText.text = "Time up!";
            StopGame();
        }
    }

    void StopGame()
    {
        isGameStopped = true;
        Time.timeScale = 0f; // 停止所有時間相關的更新（Update/物理/動畫）
        // 如需顯示暫停畫面或選單，可以在這裡觸發
        Debug.Log("Game over！");
    }
}
