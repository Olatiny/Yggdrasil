using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerInputActions playerControls;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject AttackLeft;
    [SerializeField] private GameObject AttackRight;
    [SerializeField] private GameObject AttackUp;
    [SerializeField] private GameObject AttackDown;

    private InputAction playerMovement;
    private InputAction playerAttack;
    Vector2 moveDirection = Vector2.zero;
    Vector2 lastMoveDirection = Vector2.zero;

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
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        moveDirection = playerMovement.ReadValue<Vector2>();

        if (moveDirection != Vector2.zero) lastMoveDirection = moveDirection;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacked");

        float rotation = Vector2.Angle(transform.up, lastMoveDirection);

        if (lastMoveDirection.x > 0)
        {
            rotation = 360 - rotation;
            Debug.Log(rotation);
        }

        //Debug.Log(rotation);

        StartCoroutine(AttackRoutine(rotation));
    }

    IEnumerator AttackRoutine(float rotation)
    {
        if (rotation >= 0 - 45 && rotation <= 0 + 45)
        {
            AttackUp.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            AttackUp.SetActive(false);
            //yield break;
        }

        if (rotation >= 90 - 45 && rotation <= 90 + 45)
        {
            AttackLeft.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            AttackLeft.SetActive(false);
            //yield break;
        }

        if (rotation >= 180 - 45 && rotation <= 180 + 45)
        {
            AttackDown.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            AttackDown.SetActive(false);
            //yield break;
        }

        if (rotation >= 270 - 45 && rotation <= 270 + 45)
        {
            AttackRight.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            AttackRight.SetActive(false);
            //yield break;
        }

        yield break;
    }
}
