using System.Collections;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject CurrentInteractable;
    public InputAction InteractAction;
    [SerializeField]
    private GameObject InteractionPopup;
    private Transform CurrentInteractableLocation;
    private GameObject InstantiatedInteractable;


    private void Start()
    {
        InteractAction.Enable();
       

    }
    private void Update()
    {
        if(InteractAction.triggered && CurrentInteractable)
        {
            CurrentInteractable.SendMessage("PlayerInteraction");
        }

       
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactible"))
        {
            CurrentInteractable = obj.gameObject;
            CurrentInteractableLocation = obj.transform;
            InstantiatedInteractable = Instantiate(InteractionPopup,
                   new Vector2(CurrentInteractableLocation.position.x,
                   CurrentInteractableLocation.position.y +1f),
                   Quaternion.identity);
        }
     
        
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactible") && obj.gameObject == CurrentInteractable)
        {
            CurrentInteractable = null;
            CurrentInteractableLocation = null;
        }
        if (!CurrentInteractable)
        {
            Destroy(InstantiatedInteractable);
            InstantiatedInteractable.SetActive(false);
        }


    }
}
