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
    [SerializeField] Transform GunHolster;
    [SerializeField] ItemSlot laserGunItemSlot;

    private void Start()
    {
        InteractAction.Enable();
       

    }

    private void Update()
    {
        if (InteractAction.triggered && CurrentInteractable && !CurrentInteractable.CompareTag("Enemy") && !CurrentInteractable.CompareTag("EnemyProjectile"))
        {
            if (CurrentInteractable.name == "Laser gun")
            {
                EquipWeapon(CurrentInteractable.transform);
                return;
            }
            CurrentInteractable.SendMessage("PlayerInteraction");
        }
    }

    public void EquipWeapon(Transform Weapon)
    {
        Weapon.GetComponent<Renderer>().sortingOrder = 24;
        Weapon.Rotate(new Vector3(0f, 0f, 90f));
        Weapon.SetParent(GunHolster);
        Weapon.localPosition = Vector3.zero;
        laserGunItemSlot.ObtainItem();
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
        else if(obj.CompareTag("Enemy") || obj.CompareTag("EnemyProjectile"))
        {
            FindObjectOfType<PlayerCombatController>().ReduceHealth();
        }


    }

    void OnTriggerExit2D(Collider2D obj)
    {


        if (obj.CompareTag("Enemy") != true && obj.CompareTag("EnemyProjectile") != true)
        {
            if(InstantiatedInteractable) Destroy(InstantiatedInteractable);
            if (obj.CompareTag("Interactible") && obj.gameObject == CurrentInteractable)
            {
                CurrentInteractable = null;
                CurrentInteractableLocation = null;
            }
            if (!CurrentInteractable && InstantiatedInteractable)
            {
                InstantiatedInteractable.SetActive(false);
            }
        }

    }
}
