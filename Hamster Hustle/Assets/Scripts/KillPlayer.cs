using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    // vor dem ersten Frame-Update
    void Start()
    {
        
    }

    // einmal pro Frame
    void Update()
    {
        
    }
    
    // setzt bei Kollision die Position des Players auf den Respawn Punkt zurück
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            player.transform.position = respawnPoint.position;
        }
    }
}
