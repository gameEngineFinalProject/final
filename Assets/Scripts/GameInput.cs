using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;
 

    private PlayerInputActions playerInputActions;
    private bool isPaused = false;

    [SerializeField] private Button pauseButton;         // 暫停按鈕
    [SerializeField] private TMP_Text pauseButtonText;   // 按鈕文字顯示

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        // 綁定按鈕點擊事件
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);

        UpdatePauseButtonText(); // 初始化按鈕文字
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TogglePause(); // 按下 ESC 或綁定的鍵也觸發暫停
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // 切換時間流動
        Time.timeScale = isPaused ? 0f : 1f;

        // 更新按鈕顯示文字
        UpdatePauseButtonText();
    }

    public void UpdatePauseButtonText()
    {
        if (pauseButtonText != null)
        {
            pauseButtonText.text = isPaused ? "Continue" : "Pause";
        }
    }

    private void OnDestroy()
    {
        // 移除事件綁定
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        if (pauseButton != null)
            pauseButton.onClick.RemoveListener(TogglePause);
    }
}
