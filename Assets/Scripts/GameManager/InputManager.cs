using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private MyPlayerInput playerInput;

    public event Action OnJumpInitiated;
    public event Action OnJumpCanceled;
    public event Action OnInteractionInitiated;
    public event Action OnInteractionCanceled;
    public event Action OnCamInitiated;
    public event Action OnCamCanceled;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInput = new MyPlayerInput();

        playerInput.Player.Jump.started += ctx => OnJumpInitiated?.Invoke();
        playerInput.Player.Jump.canceled += ctx => OnJumpCanceled?.Invoke();

        playerInput.Player.Interact.performed += ctx => OnInteractionInitiated?.Invoke();
        playerInput.Player.Interact.canceled += ctx => OnInteractionCanceled?.Invoke();
        
        playerInput.Player.Cam.started += ctx => OnCamInitiated?.Invoke();
        playerInput.Player.Cam.canceled += ctx => OnCamCanceled?.Invoke();
        
    }

    private void OnEnable() => playerInput?.Player.Enable();
    private void OnDisable() => playerInput?.Player.Disable();
    public Vector2 GetMovement()
    {
        return playerInput.Player.Move.ReadValue<Vector2>().normalized;
    }

}
