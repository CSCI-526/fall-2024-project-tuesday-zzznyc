using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformController : MonoBehaviour
{
    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Check if the player wants to drop through the platform
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private IEnumerator DisableCollisionTemporarily()
    {
        // Disable the player's collision with the platform for a short time
        playerCollider.enabled = false;
        yield return new WaitForSeconds(1.0f);  // Adjust the time as needed
        playerCollider.enabled = true;
    }
}
