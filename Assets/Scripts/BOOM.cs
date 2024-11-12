using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BOOM : MonoBehaviour
{   
    private int playerCollisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollisionCount++;  // Increment the counter when colliding with the player

            if (playerCollisionCount >= 2)
            {
                Destroy(gameObject);  // Destroy the object if collided with player twice
            }
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);  // Destroy immediately if it hits a wall or floor
        }
    }
}
