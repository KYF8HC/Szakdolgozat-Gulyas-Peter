using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tsz : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody2D.AddForce(new Vector2(1f, 0f) * 10);
        }
    }
}
