using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    [SerializeField]
    private GameObject weapon = null;
    private float attackDelay = 0.5f;

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("Attacking!");
    }

}
