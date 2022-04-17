using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player2MovementController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2D;
    private bool facingRight = true;
    private Animator animator2;
    private float jumpvelocity = 30f;
    private float movespeed = 10f;
    [SerializeField] private LayerMask platformLayermask;
    //public UnityEvent OnLandevent;
    //public float baseSpeed = 0f;
    //public bool outsideforce;

    private void Awake() {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        animator2 = transform.GetComponent<Animator>();
        //if (OnLandevent == null)
        //    OnLandevent = new UnityEvent();
    }
    public void Move2(int direction) {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (direction == -1) {
            animator2.SetFloat("Speed", movespeed);
            rigidbody2d.velocity = new Vector2(-movespeed, rigidbody2d.velocity.y);
            if (facingRight) {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (direction == 1) {
            animator2.SetFloat("Speed", movespeed);
            rigidbody2d.velocity = new Vector2(movespeed, rigidbody2d.velocity.y);
            if (!facingRight) {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else {
            animator2.SetFloat("Speed", 0);
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Move2(-1);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            Move2(1);
        }
        else {
            Move2(0);
        }
        if (Isgrounded() && Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody2d.velocity = Vector2.up * jumpvelocity;
        }
        if (!Isgrounded()) {
            animator2.SetFloat("Jump", 1);
        }
        if (Isgrounded()) {
            animator2.SetFloat("Jump", 0);
        }
    }

    public bool Isgrounded() {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, platformLayermask);
        return raycastHit.collider != null;
    }
    #region Kommentelt
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    outsideforce = false;
    //    if (other.gameObject.tag == "Platform")
    //        baseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    //}
    private void Update()
    {
        //if (!Isgrounded())
            //animator2.SetInteger("Jump", 1);
        //if (Isgrounded())
        //{
        //    OnLandevent.Invoke();
        //}
    }
    //public void OnLanding()
    //{
    //    animator2.SetInteger("Jump", 0);
    //}
    #endregion
}
