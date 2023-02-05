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

    [Header("Little Hitboxes")]
    [SerializeField] private GameObject SmolAttackLeft;
    [SerializeField] private GameObject SmolAttackRight;
    [SerializeField] private GameObject SmolAttackUp;
    [SerializeField] private GameObject SmolAttackDown;

    [Header("Big Hitboxes")]
    [SerializeField] private GameObject BigAttackLeft;
    [SerializeField] private GameObject BigAttackRight;
    [SerializeField] private GameObject BigAttackUp;
    [SerializeField] private GameObject BigAttackDown;

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

    //private void Start()
    //{
    //    GetComponent<Animator>().Play("smallWalkRight");
    //}

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        if (!GameManager.instance.CanMove()) return;

        moveDirection = playerMovement.ReadValue<Vector2>();

        if (moveDirection != Vector2.zero) lastMoveDirection = moveDirection;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.CanMove()) return;

        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed * Time.deltaTime * ((int)GameManager.instance.stage + 1), moveDirection.y * moveSpeed * Time.deltaTime * ((int)GameManager.instance.stage + 1));
    
        if (rigidBody.velocity != Vector2.zero)
        {
            GetComponent<Animator>().Play("smallWalkRight");
            //if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(gameObject.layer).IsName("smallWalkingRight"))
            //{
            //    GetComponent<Animator>().Play("smallWalkingRight");
            //}
            //else
            //{
            //    GetComponent<Animator>().Play("smallStillRight");
            //}
        } else
        {
            //GetComponent<Animator>().Play("smallStillRight");
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.CanMove()) return;

        switch (GameManager.instance.stage)
        {
            case GameManager.PlayerStage.stage1:
                StartCoroutine(AttackRoutineSmol());
                break;
            case GameManager.PlayerStage.stage2:
                StartCoroutine(AttackRoutineBig());
                break;
            case GameManager.PlayerStage.stage3:
                break;
            default:
                break;
        }

    }

    IEnumerator AttackRoutineSmol()
    {
        float rotation = Vector2.Angle(transform.up, lastMoveDirection);

        if (lastMoveDirection.x > 0)
        {
            rotation = 360 - rotation;
            Debug.Log(rotation);
        }

        if (rotation > 0 - 45 && rotation < 0 + 45)
        {
            SmolAttackUp.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            SmolAttackUp.SetActive(false);
            //yield break;
        }

        if (rotation > 90 - 45 && rotation < 90 + 45)
        {
            SmolAttackLeft.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            SmolAttackLeft.SetActive(false);
            //yield break;
        }

        if (rotation > 180 - 45 && rotation < 180 + 45)
        {
            SmolAttackDown.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            SmolAttackDown.SetActive(false);
            //yield break;
        }

        if (rotation > 270 - 45 && rotation < 270 + 45)
        {
            SmolAttackRight.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            SmolAttackRight.SetActive(false);
            //yield break;
        }

        yield break;
    }

    IEnumerator AttackRoutineBig()
    {
        float rotation = Vector2.Angle(transform.up, lastMoveDirection);

        if (lastMoveDirection.x > 0)
        {
            rotation = 360 - rotation;
            Debug.Log(rotation);
        }

        if (rotation > 0 - 45 && rotation < 0 + 45)
        {
            BigAttackUp.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            BigAttackUp.SetActive(false);
            //yield break;
        }

        if (rotation > 90 - 45 && rotation < 90 + 45)
        {
            BigAttackLeft.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            BigAttackLeft.SetActive(false);
            //yield break;
        }

        if (rotation > 180 - 45 && rotation < 180 + 45)
        {
            BigAttackDown.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            BigAttackDown.SetActive(false);
            //yield break;
        }

        if (rotation > 270 - 45 && rotation < 270 + 45)
        {
            BigAttackRight.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            BigAttackRight.SetActive(false);
            //yield break;
        }

        yield break;
    }
}
