using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float despawnCounter = 0;
    private void Update()
    {
        transform.position += transform.right * .015f;
        despawnCounter += Time.deltaTime;
        //if (despawnCounter > 2)
        //    Destroy(this.gameObject);

    }
}
