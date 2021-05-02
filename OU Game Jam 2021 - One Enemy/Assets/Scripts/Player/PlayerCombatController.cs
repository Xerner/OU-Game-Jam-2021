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
    [HideInInspector] 
    public bool IsRanged = false;
    [Header("Meta")]
    [SerializeField]
    private Phase1EnemyController enemy;
    [SerializeField]
    private EnemyTestController enemyp2;
    [SerializeField]
    private Phase0BossEnemyController enemyp0;
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private AudioSource attackAudio;
    [Header("Bullet")]
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform gunArm;
    [SerializeField] GameObject bullet;

    private void Start()
    {
        if (healthbar is null)
            Debug.LogWarning("PlayerCombatController: Need to initialize HealthBar in inspector");
        else
            healthbar.SetMaxHealth(health);
        if (bulletSpawnPoint is null)
            Debug.LogWarning("PlayerCombatController: Need to initialize Bullet Spawn Point in inspector");
        if (gunArm is null)
            Debug.LogWarning("PlayerCombatController: Need to initialize GunArm in inspector");
    }

    public void Attack()
    {
        {
            attackAudio.Play();
            if (IsRanged)
            {
                /*var dirX = bulletSpawnPoint.position.x - transform.position.x;
                var dirY = bulletSpawnPoint.position.y - transform.position.y;
                var angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;*/
                Instantiate(bullet, bulletSpawnPoint.position, gunArm.rotation).transform.Rotate(new Vector3(0f, 0f, -90f));
            }
            else
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
    }
    private void CheckForHit(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            if(enemy is null && enemyp0 is null)
            {
                enemyp2.HandleDamage();
            }
            else if(enemy is null && enemyp2 is null)
            {
            enemyp0.HandleDamage();
            }
            else if(enemyp0 is null && enemyp2 is null)
            {
                enemy.HandleDamage();

            }
    }
    public void ReduceHealth()
    {
        health--;
        if (healthbar != null) healthbar.SetHealth(health);
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
