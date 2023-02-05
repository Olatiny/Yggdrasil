using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LonelyThorn : MonoBehaviour
{
    private GameManager manager;
    private void Awake()
    {
        manager = GameManager.instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.instance.stage == GameManager.PlayerStage.stage1)
        {
            manager.loseHP(1);
            //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.GetComponent<Rigidbody2D>().enabled = false;
        }
    }
}
