using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BushmanScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private GameObject player;
    [SerializeField] float range;
    [SerializeField] float movement;
    [SerializeField] float eyesightMin;
    [SerializeField] float eyesightMax;
    [SerializeField] GameObject thornClump;
    private bool canAttack = true;
    private Vector2 target;
    private GameManager manager;
    private float bushmanHP = 3;
    private bool pickNewDirection = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        manager = GameManager.instance;
        player = GameObject.FindGameObjectWithTag("Player");
        newDirection();
    }

    private void FixedUpdate()
    {
        if (!manager.CanMove()) return;

        player = GameObject.FindGameObjectWithTag("Player");   
        if (Vector2.Distance(transform.position, player.transform.position) < eyesightMax)
        {
            if (Vector2.Distance(transform.position, player.transform.position) >= eyesightMin)
            {
                Vector2 oldPos = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
                Vector2 dir = (Vector2)transform.position - oldPos;
                
                if (dir.x > 0 && Math.Abs(dir.x) > Math.Abs(dir.y))
                {
                    GetComponent<Animator>().Play("bushmanWalkRight");
                }
                else if (dir.x < 0 && Math.Abs(dir.x) > Math.Abs(dir.y))
                {
                    GetComponent<Animator>().Play("bushmanWalkLeft");
                }
                else if (dir.y > 0)
                {
                    GetComponent<Animator>().Play("bushmanWalkUp");
                }
                else
                {
                    GetComponent<Animator>().Play("bushmanWalkDown");
                }

            } else if (canAttack && manager.stage == GameManager.PlayerStage.stage1)
            {
                GetComponent<Animator>().Play("bushmanAttack");
                Instantiate(thornClump, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 72)));
                StartCoroutine(Spawn());
            } 
        } else {
            Vector2 oldPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime);
            Vector2 dir = (Vector2)transform.position - oldPos;

            if (dir.x > 0 && Math.Abs(dir.x) > Math.Abs(dir.y))
            {
                GetComponent<Animator>().Play("bushmanWalkRight");
            }
            else if (dir.x < 0 && Math.Abs(dir.x) > Math.Abs(dir.y))
            {
                GetComponent<Animator>().Play("bushmanWalkLeft");
            }
            else if (dir.y > 0)
            {
                GetComponent<Animator>().Play("bushmanWalkUp");
            }
            else
            {
                GetComponent<Animator>().Play("bushmanWalkDown");
            }

            if (pickNewDirection/*Vector2.Distance(transform.position, target) < range*/)
            {
                newDirection();
                StartCoroutine(NewDirectionTimeout());
            }
        }
    }

    private void newDirection()
    {
        target = new Vector2(Random.Range(transform.position.x - movement, transform.position.x + movement), Random.Range(transform.position.y -movement, transform.position.y + movement));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && manager.stage == GameManager.PlayerStage.stage1)
        {
            manager.loseHP(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("hit wall");
            newDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            bushmanHP--;

            GetComponent<Animator>().Play("bushmanTakeDamage");

            if (bushmanHP <= 0)
            {
                GameManager.instance.addXP(3);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator NewDirectionTimeout()
    {
        pickNewDirection = false;

        yield return new WaitForSeconds(2);

        target = transform.position;

        GetComponent<Animator>().Play("bushmanStillDown");

        yield return new WaitForSeconds(1);

        pickNewDirection = true;
    }

    IEnumerator Spawn()
    {
        canAttack = false;
        yield return new WaitForSeconds(2.5f);
        canAttack = true;
    }

}
