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
    public int health = 5000;
    [SerializeField]
    Transform[] swipeLocations = new Transform[8];
    [SerializeField]
    FinalPhaseKnockbackController kb;
    [SerializeField]
    private SceneLoader sceneLoader;
    private AudioSource hit;


    private void Awake()
    {
        hit = GetComponent<AudioSource>();
        StartCoroutine(StartPhaseDelay());
    }

    IEnumerator StartPhaseDelay()
    {
        isPerformingAttack = true;
        yield return new WaitForSeconds(5f);
        isPerformingAttack = false;
    }

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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SwipeHandler());
        yield return new WaitForSeconds(2.5f);
        isPerformingAttack = false;
        hasLungeFinished = true;
    }

    IEnumerator SwipeHandler()
    {
        var dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        foreach(Transform t in swipeLocations)
        {
            RaycastHit2D ray = Physics2D.Raycast(t.position, dir, 3f);
            Debug.DrawRay(t.position, dir, Color.red, 5f);
            if (ray.collider != null && ray.collider.CompareTag("Player"))
            {
                hit.Play();
                kb.ExternalKnockbackHandler(dir);
                player.ReduceHealth();
                StopCoroutine(SwipeHandler());

            }

        }
        yield return new WaitForSeconds(.1f);
    }

    IEnumerator Projectile()
    {
        for(int i = 0; i < 10; i++)
        {
            var dirX = player.transform.position.x - transform.position.x;
            var dirY = player.transform.position.y - transform.position.y;
            var angle = Mathf.Atan2(dirY ,dirX) * Mathf.Rad2Deg;
            Debug.Log(Quaternion.Euler(0, 0, angle));
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(2.5f);
        isPerformingAttack = false;
    }
    public void HandleDamage()
    {
        health -= 100;
        if (health <= 0)
        {
            StartCoroutine(HandleWinState());
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit.Play();
            for(int i = 0; i<4; i++)
                player.ReduceHealth();
        }
    }
    IEnumerator HandleWinState()
    {
        hasLungeFinished = true;
        isPerformingAttack = true;
        yield return new WaitForSeconds(2);
        sceneLoader.LoadWinScene();
    }
}
