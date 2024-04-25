using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Jumping jumping;
    public Animator animator;
    private Vector3 newScale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumping = GetComponent<Jumping>();
        newScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        PlayerFacing();
    }

    public void HandleAnimation()
    {
        float ySpeed = rb.velocity.y;

        animator.SetFloat("ySpeed", ySpeed);

        float xSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

        animator.SetFloat("xSpeed", xSpeed);

        animator.SetBool("GroundCheck", jumping.Groundcheck());
    }

    public void PlayerFacing()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");

        if (xMovement > 0)
        {
            newScale.x = xMovement;
        }
        else if (xMovement < 0)
        {
            newScale.x = xMovement;
        }

        transform.localScale = newScale;
    }
}
