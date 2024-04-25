using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Jumping jumping;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumping = GetComponent<Jumping>();
    }

    // Update is called once per frame
    void Update()
    {
        GiveParams();
    }

    public void GiveParams()
    {
        float ySpeed = rb.velocity.y;

        animator.SetFloat("ySpeed", ySpeed);

        float xSpeed = rb.velocity.x;

        animator.SetFloat("xSpeed", xSpeed);

        animator.SetBool("GroundCheck", jumping.Groundcheck());
    }
}
