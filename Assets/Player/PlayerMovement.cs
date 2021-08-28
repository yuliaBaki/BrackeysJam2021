using System;
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
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject generatedFloor;
    private readonly Vector3 standardGravity = new Vector3(0, -19.62f);
    [SerializeField] private int RespawnCooldown = 3;
    [SerializeField] private bool canRespawn = true;

    private List<Vector3> positions = new List<Vector3>();
    
    private void Awake()
    {
        transform.position = spawn.position;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        positions.Add(transform.position);
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
        if (canRespawn == false) return;
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        canRespawn = false;
        
        GameObject spawnedFloor = Instantiate(generatedFloor,transform.position, transform.rotation);
        spawnedFloor.SetActive(true);
        transform.position = spawn.position;
        
        var q = new Queue<Vector3>(positions);
        CloneManager.Instance.AddClonePositions(q);
        positions = new List<Vector3>();
        yield return new WaitForSeconds(RespawnCooldown);

        canRespawn = true;
    }
    private bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheck.position, 0.2f, groundLayer).Length > 0;
    }
}
