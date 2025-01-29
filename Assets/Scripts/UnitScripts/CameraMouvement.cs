using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMouvement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector2 _moveInput;
    public void HandleCameraMovement(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
        if (context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouvement = moveSpeed * Time.deltaTime * (new Vector3(_moveInput.y, 0, -_moveInput.x));
        transform.Translate(mouvement, Space.World);
    }
}
