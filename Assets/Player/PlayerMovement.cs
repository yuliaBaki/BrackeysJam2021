using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float horizontal;
    private float speed = 8f;
    private float jumpForce = 12f;
    private readonly Vector3 standardGravity = new Vector3(0, -19.62f);
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlaceHolderHorizontalMovement();
        
        rigid.MovePosition(rigid.position + new Vector3(horizontal * speed, 0) * Time.fixedDeltaTime);
        if (!IsGrounded() && rigid.velocity.y < 0.1f)
        {
            Physics.gravity = standardGravity * 1.8f;
        }
        else
        {
            Physics.gravity = standardGravity;
        }
    }

    private void PlaceHolderHorizontalMovement()
    {
        //I apologize for spaghetti code
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) horizontal = 1;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) horizontal = -1;
        else horizontal = 0;
    }
    
   // public void Move(InputAction.CallbackContext context)
   // {
    //    horizontal = context.ReadValue<Vector2>().x;
  //  }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
        if(context.canceled && rigid.velocity.y > 0f)
        {
            
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.4f);
        }
        
    }
    public void Die(InputAction.CallbackContext context)
    {
        //Seppuku
    }
    private bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheck.position, 0.2f, groundLayer).Length > 0;
    }
}
