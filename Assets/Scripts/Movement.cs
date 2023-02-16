using System;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Collider2D ground;

    
    public float horizontalForce = 100;
    public float verticalForce;
    private float charge;
    private bool release;
    private float waitDelay = 0.1f;
    private float waitTime;

    private void Start(){
        
    }

    private bool IsMoving()
    {
        if (rb.velocity.x != 0) return true;
        return false;
    }
    
    private bool IsGrounded()
    {
        if (rb.IsTouching(ground)) return true;
        return false;
    }

    // called once per frame
    void Update()
    {
        
        
        // If the player isn't moving, check if space is being held down. If it is, add to charge every frame.
        if (!IsMoving())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                charge += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                release = true;
            } 
        }
        
    }
    
        // Fixed Update is also called once per frame, except Unity likes it better when ur working with physics
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            if (charge == 0.0)
            {
                if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
                { 
                    rb.velocity = new Vector2(horizontalForce * Time.deltaTime, rb.velocity.y);
                }

                if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
                {
                    rb.velocity = new Vector2(-horizontalForce * Time.deltaTime, rb.velocity.y);
                }
            }
            

            // if space bar has been released, commence with the vertical jump. Reset release and charge
            if (release)
            {
                verticalForce = 15 * charge;
                if (verticalForce > 12) verticalForce = 12;
                
                if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
                {
                    rb.velocity = new Vector2(horizontalForce * Time.deltaTime, verticalForce);
                }

                else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
                {
                    rb.velocity = new Vector2(-horizontalForce * Time.deltaTime, verticalForce);
                }

                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, verticalForce);
                }
                
                
                
                release = false;
                charge = 0f;
            }
            
            
        }
        
        
    }
}
