using System.Collections;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentObj;
    public InputAction interactAction;
    private void Start()
    {
        interactAction.Enable();
    }
    private void Update()
    {
        if(interactAction.triggered && currentObj)
        {
            currentObj.SendMessage("PlayerInteraction");
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactible"))
        {
            currentObj = obj.gameObject;
        }
     
        
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactible") && obj.gameObject == currentObj)
        {
            currentObj = null;
        }


    }
}
