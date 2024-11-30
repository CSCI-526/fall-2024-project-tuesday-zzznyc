using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLV3 : MonoBehaviour
{
    public Vector2 respawnPosition = new Vector2(0, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = respawnPosition;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  
                rb.angularVelocity = 0f;    
            }
        }
        else if (collision.gameObject.CompareTag("BOOM"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
