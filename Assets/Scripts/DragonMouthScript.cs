using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMouthScript : MonoBehaviour
{
    private bool wait = true;
    [SerializeField] float waitTimer;
    [SerializeField] GameObject fireball;
    [SerializeField] int dragonHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wait)
        {
            Instantiate(fireball, transform.position, transform.rotation);
            StartCoroutine(Spawn());
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
}
