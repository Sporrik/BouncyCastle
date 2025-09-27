using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _diveMultiplier;
    private bool _isDiving;

    private Rigidbody2D _rb;
    private Vector2 _direction;
    private Vector2 _input;

    [SerializeField] private float _attackRate;
    [SerializeField] private Transform _weapon;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = _input;
    }

    public void Dive(InputAction.CallbackContext context)
    {
        _isDiving = context.ReadValue<float>() > 0.5f;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        StartCoroutine(StartAttack());
    }

    private void ApplyMovement()
    {
        _rb.gravityScale = _isDiving ? _diveMultiplier : 1f;
        _rb.linearVelocity = new Vector2(_direction.x * _moveSpeed, _rb.linearVelocity.y);
    }

    private IEnumerator StartAttack()
    {
        _weapon.localRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z - 145f + 75f);

        yield return new WaitForSeconds(_attackRate);

        _weapon.localRotation = Quaternion.identity;
    }
}
