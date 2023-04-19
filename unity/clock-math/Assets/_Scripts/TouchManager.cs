using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerInput playerInput;

    private InputAction selectAction;
    private InputAction moveAction;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        selectAction = playerInput.actions["Select"];
    }

    private void OnEnable() {
        Debug.Log("OnEnable");
        selectAction.performed += Selected;
        moveAction.performed += Move;
    }

    private void OnDisable() {
        Debug.Log("OnDisable");
        selectAction.performed -= Selected;
        moveAction.performed -= Move;
    }

    private void Selected(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log("Selected value: " + value);
        Vector3 position = Camera.main.ScreenToWorldPoint(selectAction.ReadValue<Vector2>());
        position.z = player.transform.position.z;
    }

    private void Move(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log("Move value: " + value);
        Vector3 position = Camera.main.ScreenToWorldPoint(moveAction.ReadValue<Vector2>());
        // Maintain z position of original
        position.z = player.transform.position.z;
        player.transform.position = position;
    }
}
