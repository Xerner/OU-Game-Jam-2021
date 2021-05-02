using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float despawnCounter = 0;
    private void Update()
    {
        transform.position += transform.right * .08f;
        despawnCounter += Time.deltaTime;
        if (despawnCounter > 10)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCombatController>().ReduceHealth();
        }
    }
}
