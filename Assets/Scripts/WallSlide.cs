using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D colrb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 vel = colrb.velocity;
        Vector2 grav = Physics2D.gravity;
        if(grav.y > 0)
        {
            if (vel.y > 2)
            {
                vel.y = 2f;
            }
        }
        if(grav.y < 0)
        {
            if(vel.y < -2)
            {
                vel.y = -2f;
            }
        }
    }
}
