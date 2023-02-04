using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerInputActions playerControls;
    private InputAction playerMovement;
    private InputAction playerAttack;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerMovement = playerControls.Player.Move;
        playerMovement.Enable();

        playerAttack = playerControls.Player.Attack;
        playerAttack.Enable();
        playerAttack.performed += Attack;
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacked");
    }
}
