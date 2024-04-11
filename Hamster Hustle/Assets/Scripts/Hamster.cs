using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    public Jumping jumping;
    public float speed = 1;
    public Rigidbody2D rg;
    public LayerMask whatIsGround;
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
    public CapsuleCollider2D boxcollider;

    void Start()
    {
        boxcollider = GetComponent<CapsuleCollider2D>();
        capsuleColliderSize = boxcollider.size;
    }

    // ändere Charakterbewegung
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jumping.Groundcheck();
        SlopeCheck();
        ApplyMovement();
    }

    //überprüfe auf Hügel
    private void SlopeCheck()
    {
        Vector2 checkPos =  transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    //zeichne senkrechten Pfeil zur Erkennung der Schrägen
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

    //zeichne Pfeil entlang der Slope im 45 Grad Winkel
    private void SlopeCheckVertical(Vector2 checkPos)
    {
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
        if (jumping.Groundcheck() && !isOnSlope && !jumping.isJumping) //if on ground
        {
            newVelocity.Set(speed * moveHorizontal, rg.velocity.y);
            rg.velocity = newVelocity;
            //rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
        else if (jumping.Groundcheck() && isOnSlope && canWalkOnSlope && !jumping.isJumping) //If on slope
        {
            newVelocity.Set(speed * slopeNormalPerp.x * -moveHorizontal, speed * slopeNormalPerp.y * -moveHorizontal);
            rg.velocity = newVelocity;
            //rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
        else if (!jumping.Groundcheck()) //If in air
        {
            newVelocity.Set(speed * moveHorizontal, rg.velocity.y);
            rg.velocity = newVelocity;
            //rg.AddForce(Vector2.right * speed * moveHorizontal, ForceMode2D.Impulse);
        }
    }

    //mit Inspiration aus folgendem Video: https://www.youtube.com/watch?v=QPiZSTEuZnw
}
