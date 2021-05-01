using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCombatController : MonoBehaviour
{
	[Tooltip("Enables the GunArm and disables the RightArm")]
    public bool startRanged = false;
    [Header("Stats")]
    [SerializeField] 
    public int health = 20;
    [SerializeField] 
    float attackDelay = 0.5f;
    [HideInInspector] 
    public bool IsRanged = false;
    [Header("Meta")]
    [SerializeField]
    private Phase1EnemyController enemy;
    [SerializeField]
    private EnemyTestController enemyp2;
    [SerializeField]
    private SceneLoader sceneLoader;

    public void Attack()
    {
        {
            Debug.Log("BIG SWING");
            if(transform.localScale.x > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), transform.right * 5f);
                Debug.DrawRay(transform.position, transform.right * 5f);
                CheckForHit(hit);
            }
            else if(transform.localScale.x < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), -transform.right * 5f);
                Debug.DrawRay(transform.position, -transform.right * 5f);
                CheckForHit(hit);
            }
        }
    }
    private void CheckForHit(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("EnemyP1"))
            enemy.HandleDamage();
        else if (hit.collider != null && hit.collider.CompareTag("EnemyP2"))
        {

        }
           // enemyP2.HandleDamage();
    }
    public void ReduceHealth()
    {
        health--;
        if (health <= 0)
            HandleGameOver();
    }
    private void HandleGameOver()
    {
        sceneLoader.LoadDeathScreen();
    }
}
