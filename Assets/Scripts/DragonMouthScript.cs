using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMouthScript : MonoBehaviour
{
    private bool wait = true;
    [SerializeField] float waitTimer;
    [SerializeField] GameObject fireball;
    [SerializeField] int dragonHP;
    private bool stop = false;
    private int fireballs = 0;
    private bool wave = false;
    private Vector3 first;
    private Quaternion firstRotation;
    private Vector3 second;
    private Quaternion secondRotation;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StopSpawning());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wait && !stop)
        {
            Instantiate(fireball, transform.position, transform.rotation);
            StartCoroutine(Spawn());
            fireballs++;
            if (fireballs % 15 == 0) {
                StartCoroutine(StopSpawning());
            }
        } else if (stop && !wave && Vector3.Angle(player.transform.position - transform.position, -transform.up) < 2.5f) {
            Debug.Log("test");
            StartCoroutine(WaveSpawning());
        }
        
    }
    IEnumerator Spawn()
    {
        wait = false;
        yield return new WaitForSeconds(waitTimer);
        wait = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            GameManager.instance.loseHP(5);
        }
        if (collision.tag == "PlayerAttack")
        {
            dragonHP -= 2;
            if (dragonHP <= 0)
            {
                GameManager.instance.StartOutroCutscene();
                Destroy(gameObject.transform.parent.transform.parent.gameObject);
            }
        }
    }
    IEnumerator StopSpawning() {
        stop = true;
        yield return new WaitForSeconds(3);
        stop = false;
    }
    IEnumerator WaveSpawning() {
        wave = true;
        first = transform.position;
        firstRotation = transform.rotation;
        yield return new WaitForSeconds(.25f);
        second = transform.position;
        secondRotation = transform.rotation;
        yield return new WaitForSeconds(.25f);
        GameObject fireballOne = Instantiate(fireball, transform.position, transform.rotation);
        GameObject fireballTwo = Instantiate(fireball, first, firstRotation);
        GameObject fireballThree = Instantiate(fireball, second, secondRotation);
        fireballOne.transform.localScale *= 2.5f;
        fireballTwo.transform.localScale *= 2.5f;
        fireballThree.transform.localScale *= 2.5f;
        yield return new WaitForSeconds(2.5f);
        wave = false;
    }

}
