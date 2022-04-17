using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAgent : Agent
{
    [SerializeField] private LayerMask platformLayermask;
    public Weapons weapon;
    public int score = 0;
    private BoxCollider2D boxCollider2D;
    private int health = 100;
    private Rigidbody2D rigidbody2D;
    private bool IsGrounded;
    public Animator animator2;
    private List<Collectable> Collectables;
    private GameArea gameArea;
    private Collectable nearestCollectable;
    private bool facingRight = true;
    [SerializeField]
    public LevelGenerator levelGenerator;


    public void Start()
    {
        
    }
    public override void Initialize()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameArea = GetComponentInParent<GameArea>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(-1.3f, -1.3f, transform.localPosition.z);
        var levelParts = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var item in levelParts)
        {
            Destroy(item);
        }
        levelGenerator.Awake();
        UpdateNearestCollectable();
    }   
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.normalized);
        Vector3 toCollectable = nearestCollectable.transform.position - transform.position;
        sensor.AddObservation(toCollectable.normalized);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float moveY = vectorAction[1];
        float jumpForce = 30f;
        float movespeed = 10f;
        //Debug.Log(vectorAction[0] +" , "+ vectorAction[1] + " , "+ IsGrounded);
        if (IsGrounded && moveY > 0)
        {
            rigidbody2D.velocity = Vector2.up * jumpForce;
            animator2.SetInteger("Jump", 1);
        }
        if (IsGrounded)
        {
            animator2.SetInteger("Jump", 0);
        }
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (moveX < 0)
        {
            animator2.SetInteger("Speed", 1);
            rigidbody2D.velocity = new Vector2(-movespeed, rigidbody2D.velocity.y);
            if (facingRight)
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (moveX > 0)
        {
            animator2.SetInteger("Speed", 1);
            rigidbody2D.velocity = new Vector2(movespeed, rigidbody2D.velocity.y);
            if (!facingRight)
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W pressed");
            actionsOut[1] = 1;
        }
        else actionsOut[1] = 0;
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = -1;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W)) actionsOut[0] = 0;
        //Debug.Log(actionsOut[0] + " , " + actionsOut[1]);
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //    actionsOut[1] = 1;
        //else actionsOut[1] = 0;
    }
    private void UpdateNearestCollectable() 
    {
        foreach (Collectable collectable in gameArea.collectables)
        {
            if (collectable != null)
            {
                if (nearestCollectable == null)
                    nearestCollectable = collectable;
                else
                {
                    float DistanceToCollectable = Vector3.Distance(collectable.transform.position, transform.position);
                    float DistanceToCurrentCollectable = Vector3.Distance(nearestCollectable.transform.position, transform.position);
                    if (DistanceToCollectable < DistanceToCurrentCollectable)
                        nearestCollectable = collectable;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            AddReward(.5f);
            UpdateNearestCollectable();
            //Debug.Log(nearestCollectable.transform.localPosition);
        }
        if (collision.tag == "Boundry")
        {
            Die();
            SetReward(-1);
        }
    }

    private void FixedUpdate()
    {
        if (Isgrounded()) IsGrounded = true;
        else IsGrounded = false;
        if (score == 1)
        {
            AddReward(1f);
            score = 0;
            EndEpisode();
        }
        if (transform.localPosition.y <= -200)
        {
            EndEpisode();
        }
        UpdateNearestCollectable();
        rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation; ;
        //rigidbody2D.velocity = new Vector2(Vector2.right.x * 10f, rigidbody2D.velocity.y);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        AddReward(-.1f);
        if (health <= 0)
            Die();
    }
    void Die()
    {
        SetReward(-1f);
        EndEpisode();
    }
    public bool Isgrounded()
    {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, platformLayermask);
        return raycastHit.collider != null;
    }
}
