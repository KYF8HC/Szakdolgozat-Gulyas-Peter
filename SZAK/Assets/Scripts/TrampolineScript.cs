using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    public bool customSpeed;
    public Vector2 customVelocity;
    public float multiplier;


    int onTop = 0;
    public GameObject bouncer;
    Vector2 velocity;

    void Update()
    {
        if (onTop != 0)
            Jump();
    }
    private void Start()
    {
        bouncer = GameObject.FindGameObjectWithTag("Player");
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (onTop == 1 || onTop == 2)
        {
            bouncer = other.gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            onTop = 1;
        if (other.tag == "AI")
            onTop = 2;
    }

    void OnTriggerExit2D()
    {
        onTop = 0;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
            onTop = 1;
        if (other.tag == "AI")
            onTop = 2;
    }
    void Jump()
    {
        if (onTop == 1)
        {
            if (customSpeed)
                velocity = customVelocity;
            else
                velocity = transform.up * multiplier;
            if (bouncer.tag == "Player")
            {
                bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
                bouncer.GetComponent<Animator>().SetInteger("Jump", 2);
            }
        }
        if (onTop == 2)
        {
            if (customSpeed)
                velocity = customVelocity;
            else
                velocity = transform.up * multiplier;
            if (bouncer.tag == "AI")
            {
                bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
                bouncer.GetComponent<Animator>().SetInteger("Jump", 2);
            }
        }
    }
}