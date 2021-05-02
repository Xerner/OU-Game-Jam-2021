using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    // Start is called before the first frame update
    public void PlayerInteraction(GameObject caller)
    {
        //fill out a list of player interactions based on the objects type
        Debug.Log(caller.name);
        //gameObject.SetActive(false);
    }

    public void LeverInteraction()
    {

    }

    public void BossInteraction()
    {

    }
}
