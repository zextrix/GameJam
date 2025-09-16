using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Robot_Enemy_1_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1; // 1 for right, -1 for left
    private EnemyState enemyState;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (enemyState == EnemyState.Engaging)
        {
            Engage();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            // Attack logic would go here
            rb.linearVelocity = Vector2.zero; // Stop moving when attacking
        }
    }
    void Engage()
        {

        if (player.position.x > transform.position.x && facingDirection == -1 ||
                    player.position.x < transform.position.x && facingDirection == 1)
        {
            FacePlayer();
        }


        Vector2 direction = (player.position - transform.position).normalized; // We normalize in order to get a unit vector, which is a vector with a magnitude of 1, so that we can multiply it by the speed to get the desired velocity.
        rb.linearVelocity = direction * speed;
     }

    void FacePlayer()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer); // Check for player within detection range

        if (hits.Length > 0)
        {
            player = hits[0].transform; // Assume the first hit is the player
            /*}
            if (collision.gameObject.tag == "Player")
            {
                if (player == null) { 
                    player = collision.transform;
                }*/
            
            // if the player is within attack range and the attack cooldown has elapsed, switch to attacking state
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            // if the player is outside attack range, switch to Engaging state
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                ChangeState(EnemyState.Engaging);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop moving when the player is not detected
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        // Exit the current state
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Engaging)
            anim.SetBool("isEngaging", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        // Enter the new state
        enemyState = newState;

        // Update the animator parameters based on the new state
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Engaging)
            anim.SetBool("isEngaging", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }
}

/*public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Engaging
}*/
