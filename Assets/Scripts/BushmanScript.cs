using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BushmanScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float movement = moveSpeed * Time.deltaTime;
        if (!inRange)
        {
            int rng = Random.Range(0, 10);
            if (rng > 6)
            {
                transform.position = RandomDirection(movement);
            }
            
        }
        
    }

    private Vector3 RandomDirection(float movement)
    {
        var x = Random.Range(-movement, movement);
        var y = Random.Range(-movement, movement);
        var z = Random.Range(-movement, movement);
        return new Vector3(x, y, z);
    }

    private void OnCollisionStay2D(Collision2D collider) {
        inRange = true;
        float movement = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, collider.gameObject.transform.position, movement);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        inRange = false;
    }

}
