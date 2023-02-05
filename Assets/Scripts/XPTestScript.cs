using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPTestScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.addXP(15);
        }
    }
}
