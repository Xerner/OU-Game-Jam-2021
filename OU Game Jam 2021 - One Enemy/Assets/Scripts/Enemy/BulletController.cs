using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float despawnCounter = 0;
    private void Update()
    {
        transform.position += transform.right * .1f;
        despawnCounter += Time.deltaTime;
        if (despawnCounter > 10)
            Destroy(this.gameObject);
    }
    
}
