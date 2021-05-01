using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement2D : MonoBehaviour
{
    CharacterController2D controller;
    PlayerCombatController CombatController;
    [Range(1, 10)]
    [SerializeField] float speed = 3;
    [SerializeField] Transform GunArm;
    Vector2 aim;
    Vector2 movement;
    Animator animator;
    private float attackCooldown;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        CombatController = GetComponent<PlayerCombatController>();
		animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (controller is null) Debug.Log("No CharacterController2D set");
    }

    void Update()
    {
        if (movement.magnitude != 0)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }
		animator.SetBool("IsMoving", movement.magnitude > 0.001f);
        if (aim.magnitude > 0.01)
        {
            //GunArm.Rotate(new Vector3(0f, 0f, );
        }

		animator.SetBool("IsMoving", movement.magnitude > 0.001);
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

    }

    public void MovementListener(CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void AimListener(CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    public void AttackListener(CallbackContext context)
    {
        if (context.ReadValueAsButton() == true && context.performed == false && attackCooldown <= 0)
        {
            CombatController.Attack();
            attackCooldown = .2f;
        }
        
    }
}
