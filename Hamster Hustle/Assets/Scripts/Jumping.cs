using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public Rigidbody2D rg;
    public float jumpAmount = 10;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask groundlayer;

    void Start()
    {
        
    }

    // Update wird einmal pro Frame aufgerufen, führt Springen aus
    void Update()
    {
        if (Groundcheck() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))){
            rg.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance,boxSize);
    }
    //malt Cube, keine Parameter

    bool Groundcheck(){
        if(Physics2D.BoxCast(transform.position,boxSize,0,-transform.up,maxDistance,groundlayer)){
            return true;
        }else{
            return false;
        }
    }
    /*https://www.youtube.com/watch?v=jxCVHBMdTWo
    Erklärung für den Boxcast, checkt ob der Charakter auf dem Boden ist*/
}

