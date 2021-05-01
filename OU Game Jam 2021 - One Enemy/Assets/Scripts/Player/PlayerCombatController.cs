using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCombatController : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private int health = 10;
    [SerializeField] private Sprite RightArm = null;
    [SerializeField] 
=======
    public int health = 20;
    [SerializeField]
    private GameObject RightArm = null;
    [SerializeField]
    private Phase1EnemyController enemy;
    [SerializeField]
    private SceneLoader sceneLoader;
>>>>>>> Stashed changes
    private float attackDelay = 0.5f;

    Vector2 aim;

    [SerializeField] Transform Gun;

    public void LookListener(CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
        
    }

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
        if (hit.collider != null)
            enemy.HandleDamage();
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
