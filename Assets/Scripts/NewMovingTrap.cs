using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovingTrap : MonoBehaviour
{
    public float speed = 3f;               // Speed of the object's movement
    public float upperBoundary;      // Upper boundary for vertical movement
    public float lowerBoundary;     // Lower boundary for vertical movement
    private Vector2 direction = Vector2.up;  // Initial movement direction (upwards)

    // ´«ËÍÎ»ÖÃ
    public Vector2 respawnPosition = new Vector2(0, 0);

    private void Update()
    {
        // Move the object up and down
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the object has reached the boundaries and reverse direction
        if (transform.position.x > upperBoundary || transform.position.x < lowerBoundary)
        {
            transform.Rotate(0f, 0f, 180f);  // Rotate the object 180 degrees around the Z-axis
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player collides with the object, teleport to a specified location
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BOOM"))
        {
            // Destroy the object that collided and the trap itself
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void RespawnPlayer(GameObject player)
    {
        // Set the player's position to the respawn position
        player.transform.position = respawnPosition;

        // Stop the player's movement if they have a Rigidbody2D component
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;       // Stop linear velocity
            rb.angularVelocity = 0f;         // Stop angular velocity
        }
    }
}
