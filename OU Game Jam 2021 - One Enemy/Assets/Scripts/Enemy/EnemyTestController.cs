using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestController : MonoBehaviour
{
    [SerializeField]
    private PlayerCombatController player;
    [SerializeField]
    private GameObject bullet;
    private bool isPerformingAttack = false;
    private bool hasLungeFinished = true;
    private float swipeRange = 2f;
    private Vector2 LungeDestination;
    private int health = 10000;


    // Update is called once per frame
    void Update()
    {
        if (isPerformingAttack == false) {
            //if the player is inside of the swipe range
            if (Vector2.Distance(transform.position, player.transform.position) < swipeRange)
            {
                isPerformingAttack = true;
                StartCoroutine(Swipe());
            }
            else
            {
                var rand = Random.Range(0, 4);
                Debug.Log(rand);
                if(rand < 3)
                {
                    isPerformingAttack = true;
                    StartCoroutine(Lunge());
                }
                else if (rand == 3)
                {
                    isPerformingAttack = true;
                    StartCoroutine(Projectile());
                }
            }
        }
        if(hasLungeFinished == false)
        {
            HandleLunge();
        }
    }

    private void HandleLunge()
    {
        transform.position = Vector2.MoveTowards(transform.position, LungeDestination, .08f);
    }

    IEnumerator Lunge()
    {
        LungeDestination = player.transform.position;
        yield return new WaitForSeconds(0.2f);
        hasLungeFinished = false;
        StartCoroutine(Swipe());

    }
    IEnumerator Swipe()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("SWIPE");
        yield return new WaitForSeconds(3f);
        isPerformingAttack = false;
        hasLungeFinished = true;
    }
    IEnumerator Projectile()
    {
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("FIRE!" + i);
            var dirX = player.transform.position.x - transform.position.x;
            var dirY = player.transform.position.y - transform.position.y;
            var angle = Mathf.Atan2(dirY ,dirX) * Mathf.Rad2Deg;
            Debug.Log(Quaternion.Euler(0, 0, angle));
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(3f);
        isPerformingAttack = false;
    }
    public void HandleDamage()
    {
            health -= 100;
    }
}
