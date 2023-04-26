using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchManager : Singleton<TouchManager>
{
    #region Events
    public delegate void Select(Vector2 position);
    public event SelectItem OnSelectItem;    
    public delegate void Release(Vector2 position);
    public event ReleaseItem OnReleaseItem;
    #endregion

    private PlayerControls playerControls;
    private Camera mainCamera;

    private void Awake() {
        playerControls = new PlayerControls();
        mainCamera = Camera.main;
    }

    private void Start() {
        playerControls.Touch.PrimaryContact.started += ctx => Selected(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => Released(ctx);
    }

    private void OnEnable() {
        Debug.Log("OnEnable");
        playerControls.Enable();
    }

    private void OnDisable() {
        Debug.Log("OnDisable");
        playerControls.Disable();
    }

    

    private void Selected(InputAction.CallbackContext context) {
        if (OnSelectItem != null) OnSelectItem(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }

    private void Released(InputAction.CallbackContext context) {
        if (OnReleaseItem != null) OnSelectItem(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }

    public Vector2 PrimaryPosition() {
        return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
