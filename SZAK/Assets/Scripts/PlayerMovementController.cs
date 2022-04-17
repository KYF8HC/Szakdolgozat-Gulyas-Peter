using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private bool FacingRight = true;
    public Animator animator;
    public UnityEvent OnLandEvent;
    public float jumpVelocity = 10f;
    float moveSpeed = 10f;
    public float BaseSpeed = 0f;
    public bool outsideForce;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public GameObject hitObject;
    }

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        // Handle Jump
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        if (!IsGrounded())
           // animator.SetBool("IsJumping", true);
        if (IsGrounded())
        {
            OnLandEvent.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Move(-1);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Move(1);
        else Move(0);
        //#region OldMovement
        ////if (!outsideForce)
        ////{
        ////    rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        ////    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        ////    {
        ////        animator.SetFloat("Speed", moveSpeed);
        ////        rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        ////        transform.localScale = new Vector3(5, 5, 1);
        ////        if (FacingRight)
        ////        {
        ////            FacingRight = !FacingRight;
        ////            transform.Rotate(0f, 180f, 0f);
        ////        }
        ////    }
        ////    else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        ////    {
        ////        animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        ////        rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
        ////        transform.localScale = new Vector3(5, 5, 1);
        ////        if (!FacingRight)
        ////        {
        ////            FacingRight = !FacingRight;
        ////            transform.Rotate(0f, 180f, 0f);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        animator.SetFloat("Speed", 0);
        ////        // No keys pressed
        ////        rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        ////        rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        ////    }
        ////}
        //#endregion
    }
    public void Move(int direction)
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (direction == -1)
        {
            //animator.SetFloat("Speed", moveSpeed);
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            transform.localScale = new Vector3(5, 5, 1);
            if (FacingRight)
            {
                FacingRight = !FacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (direction == 1)
        {
           // animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
            rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
            transform.localScale = new Vector3(5, 5, 1);
            if (!FacingRight)
            {
                FacingRight = !FacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            //animator.SetFloat("Speed", 0);
            // No keys pressed
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void OnLanding()
    {
        //animator.SetBool("IsJumping", false);
    }

    public bool IsGrounded()
    {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        return raycastHit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        outsideForce = false;


        if (other.gameObject.tag == "Platform")
            BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;


    }
}
