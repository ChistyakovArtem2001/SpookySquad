using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    Rigidbody rb;
    int wholeNumber = 3;
    float dacimaNumber = 3.45f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector3(0, 5f, 0);
        }

        if (Input.GetKey("w"))
        {
            rb.velocity = new Vector3(0, 0, 5f);
        }

        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector3(5f, 0, 0);
        }

        if (Input.GetKey("s"))
        {
            rb.velocity = new Vector3(0, 0, -5f);
        }

        if (Input.GetKey("a"))
        {
            rb.velocity = new Vector3(-5f, 0, 0);
        }

    }
}

