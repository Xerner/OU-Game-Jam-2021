using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyPath destination;
    [SerializeField]
    private GameObject playerPos;
    [SerializeField]
    private GameObject bullet;
    private bool isMoving = false;
    private bool isVulnerable = false;
    [SerializeField]
    private GameObject[] Levers = new GameObject[4];
    [SerializeField]
    private int health = 3000;
    [SerializeField]
    private SceneLoader sceneLoader;
    private float endDelay = 3;
    [SerializeField]
    HealthBar healthBar;
    Animator Anim;

    AudioSource enemyAudio;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        if (healthBar is null) Debug.Log("HealthBar must be initialized in the inspector");
        else healthBar.SetMaxHealth(health);
        enemyAudio = GetComponent<AudioSource>();
        StartCoroutine(MovementDelay());

    }

    IEnumerator MovementDelay()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(Fire());
        isMoving = true;
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 10; i++)
        {
            var dirX = playerPos.transform.position.x - transform.position.x;
            var dirY = playerPos.transform.position.y - transform.position.y;
            var angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(3f);
    }

    private void Update()
    {
        if (isMoving == true && isVulnerable == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, .05f);
        }
        if (Vector2.Distance(transform.position, destination.transform.position) < .1f && isMoving == true)
        {
            destination = destination.nextDestination;
            isMoving = false;
            StartCoroutine(MovementDelay());
        }
        if (CheckLevers() && isVulnerable == false)
        {
            StartCoroutine(VulnerablePhase());
        }
        if(health <= 0)
        {
            Anim.SetTrigger("DeathState");
            EndOfPhase();
        }
    }

    private void EndOfPhase()
    {
        isVulnerable = true;
        foreach(GameObject lever in Levers)
        {
            lever.SetActive(false);
        }
        endDelay -= Time.deltaTime;
        Debug.Log(endDelay);
        if(endDelay<=0)
            sceneLoader.LoadNextPhase();
    }

    private bool CheckLevers()
    {
        if (Levers[0].CompareTag("Untagged") && Levers[1].CompareTag("Untagged") && Levers[2].CompareTag("Untagged") && Levers[3].CompareTag("Untagged"))
            return true;
        else return false;
    }
    IEnumerator VulnerablePhase()
    {
        isMoving = false;
        isVulnerable = true;
        enemyAudio.Play();
        yield return new WaitForSeconds(6f);
        foreach(GameObject lever in Levers)
        {
            lever.GetComponent<LeverController>().DeActivate();
            lever.tag = "Interactible";
        }
        isVulnerable = false;
        isMoving = true;

    }
    public void HandleDamage()
    {
        if(isVulnerable)
            health -= 100;
        if(healthBar != null) healthBar.SetHealth(health);

        
    }
}

