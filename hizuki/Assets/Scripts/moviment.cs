using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment : MonoBehaviour
{
    [SerializeField] 
    public float speed;
    
    [SerializeField]
    float jumpForce;
    
    [SerializeField]
    bool canJump;
    
    [SerializeField]
    Rigidbody2D rb;
    
    
    [SerializeField]
    Transform groundCheck;
    
    [SerializeField]
    LayerMask groundLayer;
    
    float moveX;
    bool facingRight = true;
 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(
            moveX * speed,
            rb.linearVelocity.y
        );
    }
    
    
    private void Flip()
    {
       if(facingRight && moveX < 0f || !facingRight && moveX > 0f)
       {
            facingRight = !facingRight;
            var localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
       }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            0.2f,
            groundLayer
        );

        return hit.collider != null;
    }
    
    
    private void OnJump(InputValue value)
    {
        var input = value.Get<float>();
        
       Debug.unityLogger.Log("input:" + input);
       Debug.Log(IsGrounded()); 
       
       
         if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, input * jumpForce);
        }
      
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        Debug.unityLogger.Log("input:" + input);
        moveX = input.x;
        Flip();
    }
    
    
    
    
}
