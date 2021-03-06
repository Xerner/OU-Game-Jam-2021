using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

public class Phase0BossEnemyController : MonoBehaviour
{
    Animator PhaseOneAnimator;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    List<GameObject> Turrets = new List<GameObject>();
    [SerializeField]
    private GameObject bullet;
    public int health = 2000;
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    HealthBar healthBar;
    bool vulnerable;

    // Start is called before the first frame update
    void Awake()
    {
        if (healthBar is null) Debug.Log("Phase0BossEnemyController: health bar must be assigned in the inspector.");
        else healthBar.SetMaxHealth(health);
        PhaseOneAnimator = GetComponent<Animator>();
        StartCoroutine(IdleToOpen());
        
    }
    private void Update()
    {
       
      
    }

    IEnumerator IdleToOpen()
    {
        vulnerable = true;
        PhaseOneAnimator.SetTrigger("Idle->Open");
        yield return new WaitForSeconds(5.0f);
        vulnerable = false;
        StartCoroutine(OpenToClosed());
        
    }

    IEnumerator OpenToClosed()
    {

        PhaseOneAnimator.SetTrigger("Open->Closed");
        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackOne_Bullet());
        StartCoroutine(ClosedToIdle());
    }

    IEnumerator ClosedToIdle()
    {
        PhaseOneAnimator.SetTrigger("Closed->Idle");
        yield return new WaitForSeconds(5.8f);
        StartCoroutine(IdleToOpen());

    }

    IEnumerator AttackOne_Bullet()
    {
        yield return new WaitForSeconds(.8f);
        foreach (GameObject turret in Turrets) {            
            for (int i = 0;i < 4; i++)
            {
                var dirX = Player.transform.position.x - turret.transform.position.x;
                var dirY = Player.transform.position.y - turret.transform.position.y;
                var angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
                Instantiate(bullet, turret.transform.position, Quaternion.Euler(0f, 0f, angle));
                yield return new WaitForSeconds(.2f);
            }
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("here");
    }

    public void HandleDamage()
    {
        if(vulnerable)
            health -= 100;
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
        if (health <= 0) {
            PhaseOneAnimator.SetBool("PhaseHealthDepleted", true);
            StartCoroutine(WaitForDeathAnimation());
            StartCoroutine(HandleNextPhase());
        }
    }

    IEnumerator HandleNextPhase()
    {
        yield return new WaitForSeconds(3.5f);
        sceneLoader.LoadNextPhase();
    }
    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(45f);
    }
}
