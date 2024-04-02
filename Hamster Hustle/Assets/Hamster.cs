using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    //speed of character movement
    public float speed = 5;

    // called when initializing the character
    void Start()
    {
    }

    // changes movement of character
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }
}
