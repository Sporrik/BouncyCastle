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
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _attackPoint1;
    [SerializeField] private Transform _attackPoint2;

    [SerializeField] float swingTime, returnTime, attackHold;
    [SerializeField] SpriteRenderer sr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        UpdateFacing();
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
        _rb.gravityScale = _isDiving ? _diveMultiplier : 4f;
        _rb.linearVelocity = new Vector2(_direction.x * _moveSpeed, _rb.linearVelocity.y);
    }

    private void UpdateFacing()
    {
        if (Mathf.Abs(_direction.x) < 0.001f) return;
        
        sr.flipX = _direction.x < 0f;

        if (sr.flipX)
        {
            _attackPoint.localPosition = new Vector3(_attackPoint2.localPosition.x, -0.51f, 0f);
        }
        else
        {
            _attackPoint.localPosition = new Vector3(_attackPoint1.localPosition.x, -0.51f, 0f);
        } 
    }


    int FacingSign => (sr && sr.flipX) || transform.localScale.x < 0f ? -1 : 1;

    IEnumerator StartAttack()
    {
        _weapon.GetComponent<Collider2D>().enabled = true;

        float poseZ = transform.eulerAngles.z - 145f + 75f;
        float targetZ = FacingSign * poseZ;

        yield return RotateZ(_weapon, targetZ, swingTime);
        yield return new WaitForSeconds(attackHold);
        yield return RotateZ(_weapon, 0f, returnTime);

        _weapon.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator RotateZ(Transform t, float toZ, float dur)
    {
        float fromZ = t.localEulerAngles.z;
        float tAcc = 0f;
        while (tAcc < dur)
        {
            tAcc += Time.deltaTime;
            float s = Mathf.Clamp01(tAcc / dur);
            float z = Mathf.LerpAngle(fromZ, toZ, s);
            t.localRotation = Quaternion.Euler(0f, 0f, z);
            yield return null;
        }
        t.localRotation = Quaternion.Euler(0f, 0f, toZ);
    }
}
