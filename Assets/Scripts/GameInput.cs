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

    public bool IsPaused { get; private set; }

    [Header("UI 元件")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private TMP_Text pauseButtonText;
    [SerializeField] private Image pauseButtonImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        UpdatePauseUI(); // 初始化顯示
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TogglePause();
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnPauseButtonClicked()
    {
        TogglePause();
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        UpdatePauseUI();
    }

    private void UpdatePauseUI()
    {
        // 更新圖片
        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite = IsPaused ? playSprite : pauseSprite;
        }
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }
    }
}
