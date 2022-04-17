using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public float distance = 1f;
    PlayerMovementController movement;
    public float speed = 2f;
    bool walljumping;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x/5, distance);


        if ((Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.Space)) && !movement.IsGrounded()
            && hit.collider != null )
        {
            {
                Debug.Log(hit.collider.name);
                movement.outsideForce = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
                StartCoroutine("TurnIt"); 
            }
        }
    }

    IEnumerator TurnIt()
    {
        yield return new WaitForFixedUpdate();

        transform.localScale = transform.localScale.x == 5 ? new Vector2(-5, 5) : new Vector2(5,5);

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x / 5 * distance);

    }
}