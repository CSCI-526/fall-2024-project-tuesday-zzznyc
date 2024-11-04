using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 initialGravity;           
    private Vector2 chosenGravity;            
    private bool hasGravityPowerUp = false;   
    private bool gravitySelected = false;     

    void Start()
    {
        initialGravity = Physics2D.gravity;
        chosenGravity = initialGravity;
    }

    void Update()
    {

        if (hasGravityPowerUp && !gravitySelected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                chosenGravity = new Vector2(0, 9.8f); 
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                chosenGravity = new Vector2(0, -9.8f); 
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                chosenGravity = new Vector2(-9.8f, 0); 
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                chosenGravity = new Vector2(9.8f, 0); 
                gravitySelected = true;
            }
        }

        if (gravitySelected && Input.GetKeyDown(KeyCode.G))
        {
            if (Physics2D.gravity == initialGravity)
            {
                Physics2D.gravity = chosenGravity; 
            }
            else
            {
                Physics2D.gravity = initialGravity; 
                hasGravityPowerUp = false;        
                gravitySelected = false;           
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GravityPowerUp"))
        {
            hasGravityPowerUp = true;        
            gravitySelected = false;          
            Destroy(other.gameObject);        
        }
    }
}
