using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{

    // Start is called before the first frame update
    public void PlayerInteraction()
    {
        //fill out a list of player interactions based on the objects type
        Debug.Log("here");
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
