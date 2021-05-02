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
    HealthBar healthbar;
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

    private void Start()
    {
        if (healthbar is null) Debug.LogError("PlayerCombatController: Need to initialize HealthBar in inspector");
        healthbar.SetMaxHealth(health);
    }

    public void Attack()
    {
        {
            if(transform.localScale.x > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), transform.right * 3.5f);
                Debug.DrawRay(transform.position, transform.right * 3.5f);
                CheckForHit(hit);
            }
            else if(transform.localScale.x < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), -transform.right * 3.5f);
                Debug.DrawRay(transform.position, -transform.right * 3.5f);
                CheckForHit(hit);
            }
        }
    }
    private void CheckForHit(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            Debug.Log("This code is running");
            if(enemy is null)
            {
                enemyp2.HandleDamage();
            }
            else
            {
                enemy.HandleDamage();
            }
    }
    public void ReduceHealth()
    {
        health--;
        healthbar.SetHealth(health);
        if (health <= 0)
            HandleGameOver();
    }
    private void HandleGameOver()
    {
        StartCoroutine(GameOverDelay());
        sceneLoader.LoadDeathScreen();
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f);
    }
}
