using System.Collections;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

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
        if (InteractAction.triggered && CurrentInteractable && !CurrentInteractable.CompareTag("Enemy"))
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
        else if(obj.CompareTag("Enemy") || obj.CompareTag("Enemy Projectile"))
        {
            FindObjectOfType<PlayerCombatController>().ReduceHealth();
        }


    }

    void OnTriggerExit2D(Collider2D obj)
    {


        if (obj.CompareTag("Enemy") != true)
        {
            Destroy(InstantiatedInteractable);
            if (obj.CompareTag("Interactible") && obj.gameObject == CurrentInteractable)
            {
                CurrentInteractable = null;
                CurrentInteractableLocation = null;
            }
            if (!CurrentInteractable)
            {
                InstantiatedInteractable.SetActive(false);
            }
        }

    }
}
