using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMove : MonoBehaviour
{
    public float rotatespeed;
    public GameObject player;
   
    void Update()
    {   
        rotatespeed = 180.0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0f,0f, -1 * rotatespeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, rotatespeed * Time.deltaTime));
        }

       
        transform.position = player.transform.position;
    }
}
