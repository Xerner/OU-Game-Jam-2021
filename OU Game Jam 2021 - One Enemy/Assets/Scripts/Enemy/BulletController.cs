using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float despawnCounter = 0;
    [SerializeField]
    private AudioSource hitSound;

    private void Update()
    {
        transform.position += transform.right * .07f;
        despawnCounter += Time.deltaTime;
        if (despawnCounter > 10)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCombatController>().ReduceHealth();
            hitSound.Play();
            Destroy(gameObject);
        }
        if (collision.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }
}
