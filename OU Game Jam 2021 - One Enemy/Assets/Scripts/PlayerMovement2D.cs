using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement2D : MonoBehaviour
{
    CharacterController2D controller;
    [Range(1, 10)]
    [SerializeField] float speed = 3;
    Vector2 movement;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Start()
    {
        if (controller is null) Debug.Log("No CharacterController2D set");
    }

    // Start is called before the first frame update
    void Update()
    {
        //Debug.Log(movement + " | " + movement.magnitude + " | " + speed + " | ");
        if (movement.magnitude != 0)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    public void MovementListener(CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);
    }
}
