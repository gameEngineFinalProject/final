using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // �˼Ʈɶ��]��^
    private float currentTime;

    public TextMeshProUGUI countdownText; // ��J UI Text ����
    private bool isGameStopped = false;

    void Start()
    {
        currentTime = countdownTime;
        Time.timeScale = 1f; // �T�O�C���@�}�l�O���`�t��
    }

    void Update()
    {
        if (isGameStopped) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            countdownText.text = /*"�˼ơG" +*/ Mathf.CeilToInt(currentTime).ToString();
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
        Time.timeScale = 0f; // ����Ҧ��ɶ���������s�]Update/���z/�ʵe�^
        // �p����ܼȰ��e���ο��A�i�H�b�o��Ĳ�o
        Debug.Log("Game over�I");
    }
}
