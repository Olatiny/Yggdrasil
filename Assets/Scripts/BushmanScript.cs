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
        player = GameObject.FindGameObjectWithTag("Player");
        newDirection();
    }
    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) < eyesightMax)
        {
            if (Vector2.Distance(transform.position, player.transform.position) >= eyesightMin)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
            } else if (canAttack)
            {
                Instantiate(thornClump, transform.position, transform.rotation);
                StartCoroutine(Spawn());
            }
        } else {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime);
            if (Vector2.Distance(transform.position, target) < range)
            {
                newDirection();
            }
        }
    }
    private void newDirection()
    {
        target = new Vector2(Random.Range(-movement, movement), Random.Range(-movement, movement));
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        
    }
    IEnumerator Spawn()
    {
        canAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
    }

}
