using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 10;
    [SerializeField] float attackDelay = 0.5f;
    [HideInInspector] public bool IsRanged = false;
	[Tooltip("Enables the GunArm and disables the RightArm")]
    public bool startRanged = false;

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("Attacking!");
    }
}
