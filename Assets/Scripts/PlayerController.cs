using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Transform _groundCheck;

    private Rigidbody2D _rb;
    private Vector2 _direction;

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.started) return;
    }
}
