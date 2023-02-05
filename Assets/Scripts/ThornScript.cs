using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    private Transform[] thorns;
    [SerializeField] private float moveSpeed;
    private GameManager manager;
    private void Awake()
    {
        manager = GameManager.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        thorns = GetComponentsInChildren<Transform>();
        StartCoroutine(thornDeath());
    }
    IEnumerator thornDeath()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Transform thorn in thorns)
        {
            thorn.position += new Vector3((thorn.position.x - transform.position.x), (thorn.position.y - transform.position.y), transform.position.z).normalized * moveSpeed * Time.fixedDeltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manager.loseHP(1);
        }
    }
}
