using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Vector3 parentPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject pivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        parentPosition = GameObject.FindGameObjectWithTag("DragonBody").transform.position;
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3((transform.position.x - parentPosition.x), (transform.position.y - parentPosition.y), parentPosition.z).normalized * moveSpeed * Time.fixedDeltaTime;
    }

    //void OnTriggerE

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.tag == "Player")
        {
            GameManager.instance.loseHP(3);
            Destroy(gameObject);
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
