using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    [SerializeField]
    private GameObject RightArm = null;
    private float attackDelay = 0.5f;

    Vector2 aim;

    [SerializeField] Transform Gun;

    public void LookListener(CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
        
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("Attacking!");
    }

}
