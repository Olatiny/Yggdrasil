using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [Header("Fields")]
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

    bool attacking = false;

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

        switch (GameManager.instance.stage)
        {
            case GameManager.PlayerStage.stage1:
                rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed * Time.deltaTime, moveDirection.y * moveSpeed * Time.deltaTime);
                break;
            case GameManager.PlayerStage.stage2:
            case GameManager.PlayerStage.stage3:
                rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed * Time.deltaTime * 10, moveDirection.y * moveSpeed * Time.deltaTime * 10);
                break;
            default:
                break;
        }
        
        if (GameManager.instance.stage == GameManager.PlayerStage.stage1)
        {
            if (rigidBody.velocity != Vector2.zero && !attacking)
            {
                if (moveDirection.y > 0)
                {
                    GetComponent<Animator>().Play("smallWalkUp");
                }
                else if (moveDirection.x < 0)
                {
                    GetComponent<Animator>().Play("smallWalkLeft");
                }
                else if (moveDirection.x > 0)
                {
                    GetComponent<Animator>().Play("smallWalkRight");
                }
                else
                {
                    GetComponent<Animator>().Play("smallWalkDown");
                }
            }
            else if (!attacking)
            {
                if (lastMoveDirection.y > 0)
                {
                    GetComponent<Animator>().Play("smallStillUp");
                }
                else if (lastMoveDirection.x < 0)
                {
                    GetComponent<Animator>().Play("smallStillLeft");
                }
                else if (lastMoveDirection.x > 0)
                {
                    GetComponent<Animator>().Play("smallStillRight");
                }
                else
                {
                    GetComponent<Animator>().Play("smallStillDown");
                }
            }
        }
        else if (GameManager.instance.stage == GameManager.PlayerStage.stage2)
        {
            // Stage 2 animations
        }
        else if (GameManager.instance.stage == GameManager.PlayerStage.stage3)
        {
            // Stage 3 animations
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.CanMove()) return;

        attacking = true;

        switch (GameManager.instance.stage)
        {
            case GameManager.PlayerStage.stage1:
                //GetComponent<Animator>().Play("smallAttackRight");
                StartCoroutine(AttackRoutineSmol());
                break;
            case GameManager.PlayerStage.stage2:
            case GameManager.PlayerStage.stage3:
                StartCoroutine(AttackRoutineBig());
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
            GetComponent<Animator>().Play("smallAttackUp");
            SmolAttackUp.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            SmolAttackUp.SetActive(false);
            //yield break;
        }

        if (rotation > 90 - 45 && rotation < 90 + 45)
        {
            GetComponent<Animator>().Play("smallAttackLeft");
            SmolAttackLeft.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            SmolAttackLeft.SetActive(false);
            //yield break;
        }

        if (rotation > 180 - 45 && rotation < 180 + 45)
        {
            GetComponent<Animator>().Play("smallAttackDown");
            SmolAttackDown.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            SmolAttackDown.SetActive(false);
            //yield break;
        }

        if (rotation > 270 - 45 && rotation < 270 + 45)
        {
            GetComponent<Animator>().Play("smallAttackRight");
            SmolAttackRight.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            SmolAttackRight.SetActive(false);
            //yield break;
        }

        attacking = false;

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
            yield return new WaitForSeconds(0.5f);
            BigAttackUp.SetActive(false);
            //yield break;
        }

        if (rotation > 90 - 45 && rotation < 90 + 45)
        {
            BigAttackLeft.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            BigAttackLeft.SetActive(false);
            //yield break;
        }

        if (rotation > 180 - 45 && rotation < 180 + 45)
        {
            BigAttackDown.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            BigAttackDown.SetActive(false);
            //yield break;
        }

        if (rotation > 270 - 45 && rotation < 270 + 45)
        {
            BigAttackRight.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            BigAttackRight.SetActive(false);
            //yield break;
        }

        attacking = false;

        yield break;
    }
}
