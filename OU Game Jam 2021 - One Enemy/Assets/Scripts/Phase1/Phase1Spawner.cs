using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Spawner : MonoBehaviour
{
    private float spawnTimer = 0f;
    [SerializeField]
    private GameObject projectile;
    [SerializeField][Range(1,4)]
    private float spawnDelay = 3f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            spawnTimer = 0;
        }
    }
}
