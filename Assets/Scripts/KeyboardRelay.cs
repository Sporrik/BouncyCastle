using UnityEngine;
using UnityEngine.InputSystem;
using System.Reflection;

public class KeyboardRelayMulti : MonoBehaviour
{
    [SerializeField] MonoBehaviour playerScript;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference attack;
    [SerializeField] InputActionReference dive;

    InputAction aMove, aAttack, aDive;
    MethodInfo mMove, mAttack, mDive;

    void Awake()
    {
        mMove = GetMethod("Move");
        mAttack = GetMethod("Attack");
        mDive = GetMethod("Dive");

        aMove = move ? move.action : null;
        aAttack = attack ? attack.action : null;
        aDive = dive ? dive.action : null;
    }

    MethodInfo GetMethod(string name) =>
        playerScript.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    void OnEnable()
    {
        if (aMove != null) { aMove.Enable(); aMove.performed += ForwardMove; aMove.canceled += ForwardMove; }
        if (aAttack != null) { aAttack.Enable(); aAttack.performed += ForwardAttack; aAttack.canceled += ForwardAttack; }
        if (aDive != null) { aDive.Enable(); aDive.performed += ForwardDive; aDive.canceled += ForwardDive; }
    }
    void OnDisable()
    {
        if (aMove != null) { aMove.performed -= ForwardMove; aMove.canceled -= ForwardMove; aMove.Disable(); }
        if (aAttack != null) { aAttack.performed -= ForwardAttack; aAttack.canceled -= ForwardAttack; aAttack.Disable(); }
        if (aDive != null) { aDive.performed -= ForwardDive; aDive.canceled -= ForwardDive; aDive.Disable(); }
    }

    void ForwardMove(InputAction.CallbackContext ctx) { mMove?.Invoke(playerScript, new object[] { ctx }); }
    void ForwardAttack(InputAction.CallbackContext ctx) { mAttack?.Invoke(playerScript, new object[] { ctx }); }
    void ForwardDive(InputAction.CallbackContext ctx) { mDive?.Invoke(playerScript, new object[] { ctx }); }
}
