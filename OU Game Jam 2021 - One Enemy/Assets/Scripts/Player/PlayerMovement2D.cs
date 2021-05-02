using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement2D : MonoBehaviour
{
    CharacterController2D controller;
    PlayerCombatController combatController;
    [SerializeField] float speed = 3;
    Vector2 movement;
    Animator animator;
    private float attackCooldown;
    private bool isDisabled = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        combatController = GetComponent<PlayerCombatController>();
		animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (controller is null) Debug.Log("No CharacterController2D set");
    }

    public void DisbaleUserInput() => isDisabled = true;
    public void EnableUserInput() => isDisabled = false;

    void Update()
    {
        if (movement.magnitude != 0)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }

		animator.SetBool("IsMoving", movement.magnitude > 0.001);
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    public void MovementListener(CallbackContext context)
    {
        if(!isDisabled)
            movement = context.ReadValue<Vector2>();
    }

    public void AttackListener(CallbackContext context)
    {
        if (!isDisabled)
        {
            if (context.ReadValueAsButton() == true && context.performed == false && attackCooldown <= 0)
            {
                combatController.Attack();
                attackCooldown = .2f;
            }

        }
    }
}
