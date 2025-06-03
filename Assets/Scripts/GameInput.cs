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

    [SerializeField] private Button pauseButton;         // �Ȱ����s
    [SerializeField] private TMP_Text pauseButtonText;   // ���s��r���

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        // �j�w���s�I���ƥ�
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);

        UpdatePauseButtonText(); // ��l�ƫ��s��r
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TogglePause(); // ���U ESC �θj�w����]Ĳ�o�Ȱ�
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

        // �����ɶ��y��
        Time.timeScale = isPaused ? 0f : 1f;

        // ��s���s��ܤ�r
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
        // �����ƥ�j�w
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        if (pauseButton != null)
            pauseButton.onClick.RemoveListener(TogglePause);
    }
}
