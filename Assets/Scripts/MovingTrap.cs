using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public float speed = 3f;               // Speed of the object's movement
    public float upperBoundary;      // Upper boundary for vertical movement
    public float lowerBoundary;     // Lower boundary for vertical movement
    private Vector2 direction = Vector2.up;  // Initial movement direction (upwards)

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
        // If the player collides with the object, restart the level
        if (other.CompareTag("Player"))
        {
            // Restart the current level
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("BOOM"))
        {
            //Destory enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
