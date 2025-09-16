using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 5f;
    public int facingDirection = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        if ((horizontal > 0 && vertical > 0) || (horizontal < 0 && vertical < 0) || (horizontal > 0 && vertical < 0) || (horizontal < 0 && vertical > 0))
        {
            animator.SetFloat("horizontal", Mathf.Abs(horizontal));
            animator.SetFloat("vertical", (0));
        }
        /*else if ((horizontal > 0 && vertical < 0) || (horizontal < 0 && vertical > 0))
         {
             animator.SetFloat("horizontal", Mathf.Abs(horizontal));
             animator.SetFloat("vertical", (0));

         } */
        
        else
        { 
        animator.SetFloat("horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("vertical", (vertical));
        }

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;


        void Flip()
        {
            facingDirection *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
