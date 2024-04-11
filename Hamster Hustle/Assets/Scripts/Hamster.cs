using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    //speed of character movement
    public Jumping jumping;
    public float speed = 1;
    public Rigidbody2D rg;
    //private Transform groundCheck;
    //private float groundCheckRadius;
    private LayerMask whatIsGround;
    private Vector2 capsuleColliderSize;
    private float slopeCheckDistance;
    private bool isOnSlope;
    private float slopeSideAngle;
    private Vector2 slopeNormalPerp;
    private float slopeDownAngle;
    private float lastSlopeAngle;
    private float maxSlopeAngle;
    private bool canWalkOnSlope;
    public float moveHorizontal;
    private PhysicsMaterial2D noFriction;
    private PhysicsMaterial2D fullFriction;
    private Vector2 newVelocity;

    // called when initializing the character
    void Start()
    {
        
    }

    // changes movement of character
    void FixedUpdate()
    {
        
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jumping.Groundcheck();
        SlopeCheck();
        ApplyMovement();
        
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
    }

    /*private void Groundcheck(){
        if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround)){
            isGrounded = true;
        }else{
            isGrounded = false;
        }
        if(rg.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
        //isjumping = false setzen in jumping.cs, wenn nicht am springen also taste nicht gedrückt
    }*/

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        //moveHorizontal = Input.GetAxis("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;            
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }                       
            lastSlopeAngle = slopeDownAngle;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && moveHorizontal == 0.0f)
        {
            rg.sharedMaterial = fullFriction;
        }
        else
        {
            rg.sharedMaterial = noFriction;
        }
    }

    private void ApplyMovement()
    {
        //moveHorizontal = Input.GetAxis("Horizontal");
        Debug.Log(jumping.Groundcheck() + " " + isOnSlope + " " + canWalkOnSlope + " " + !jumping.isJumping);
        if (jumping.Groundcheck() && !isOnSlope && !jumping.isJumping) //if not on slope
        {
            //Debug.Log("horizontal");
            newVelocity.Set(speed * moveHorizontal, 0.0f);
            rg.velocity = newVelocity;
            rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
        else if (jumping.Groundcheck() && isOnSlope && canWalkOnSlope && !jumping.isJumping) //If on slope
        {
            Debug.Log("on slope");
            newVelocity.Set(speed * slopeNormalPerp.x * -moveHorizontal, speed * slopeNormalPerp.y * -moveHorizontal);
            rg.velocity = newVelocity;
            rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
        else if (!jumping.Groundcheck()) //If in air
        {
            Debug.Log("in air");
            newVelocity.Set(speed * moveHorizontal, rg.velocity.y);
            rg.velocity = newVelocity;
            rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("not good");
        }

    }
}
