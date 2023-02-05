using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.fixedTime * rotationSpeed, 120) - 60);
    }
}
