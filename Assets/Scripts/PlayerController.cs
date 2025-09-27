using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 _input;
    private float _xInput;
    private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    private Vector3 _direction;
    private float _rotationSpeed = 500f;

    private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier;
    private float _velocity;

    [SerializeField]
    private GameObject _weapon;
    private float _attackRate = 2f;
    private float _attackTimer;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyMovement();
    }

    private bool IsGrounded() => _characterController.isGrounded;

    private void ApplyMovement()
    {
        _characterController.Move(_direction * _speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.2f)
        {
            _velocity = 0f;
        }
        else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
            _direction.y = _velocity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += _jumpPower;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (Time.time >= _attackTimer)
        {
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        //Update attack timer
        _attackTimer = Time.time + 1f / _attackRate;

        //Check if any enemies are in swing range, then damages them
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        foreach (Collider enemy in hitEnemies)
        {
            //Push/stun player
            //enemy.GetComponent<Enemy>().TakeDamage();
        }

        //"Animate" weapon
        _weapon.transform.rotation = Quaternion.Euler(75f, transform.eulerAngles.y - 145f, -115f);

        yield return new WaitForSeconds(0.5f);

        _weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //bounce off walls
        if (collision.gameObject.CompareTag("Bouncy"))
        {
            Vector3 collisionNormal = collision.GetContact(0).normal;
            _direction = Vector3.Reflect(_direction, collisionNormal).normalized;
        }
    }
}
