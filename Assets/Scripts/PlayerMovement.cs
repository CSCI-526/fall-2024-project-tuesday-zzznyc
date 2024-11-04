using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{   
    bool isGrounded = false;
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject reticlecenter;
    [SerializeField] private GameObject projectilePrefab;

    Rigidbody2D rb;
    // bool facingRight = true;
    bool wallSliding;
    bool wallJumping;

    Vector2 playerpos;
    Vector2 reticlepos;
    Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float regGrav = 1.0f;
    [SerializeField] float speedMultiplier;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] float minReticleDistance;  
    [SerializeField] float maxReticleDistance; 
    [SerializeField] float reticleSpeed;
    private bool increasingDistance = true;          
    private float currentReticleDistance; 

    private Color originalReticleColor; 
    private bool TrapBoom = false;

    private bool hasGravityPowerUp = false; //Got gravity item
    private bool gravityEnabled = false;    //Enabled gravity change
    private bool hasChosenGravity = false;

    [SerializeField] float specialPlatformJumpForce = 15f;  // New: Boosted jump force for special platform
    void Start()
    {   
        // minReticleDistance;
        // maxReticleDistance = 6.0f;
        speed = 1.0f;
        speedMultiplier = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        currentReticleDistance = minReticleDistance;
        Physics2D.gravity = new Vector2(0, -9.8f);

        if (reticle != null)
        {
            SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
            if (reticleRenderer != null)
            {
                originalReticleColor = reticleRenderer.color;
            }
        }
    }

    public void ChangeReticleColor(Color newColor)
    {
        SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        if (reticleRenderer != null)
        {
            reticleRenderer.color = newColor;
            TrapBoom = true;
        }
    }

    private void ShootProjectile(Vector2 direction, float distance)
    {   
        Color reticleColor = reticle.GetComponent<SpriteRenderer>().color;
        if (reticleColor == Color.green)
        {
            return; // 停止执行
        }
        Vector2 spawnPosition = (Vector2)transform.position + ((Vector2)reticle.transform.position - (Vector2)transform.position).normalized * 0.5f; // 调整0.5f为所需的偏移距离
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        SpriteRenderer projectileRenderer = projectile.GetComponent<SpriteRenderer>();

        projectileRenderer.color = reticle.GetComponent<SpriteRenderer>().color;
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(direction * speed * distance, ForceMode2D.Impulse);
        }
    }
    private void ResetReticleColor()
    {
        SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        if (reticleRenderer != null)
        {
            reticleRenderer.color = originalReticleColor;
            TrapBoom = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GravityPowerUp"))
        {
            //Change Rectile color after pickup gravity object
            //ChangeReticleColor(Color.green); 
            //Destroy(other.gameObject); //Destroy object
            hasGravityPowerUp = true; //Get Gravity control object
        }
        
    }
    void Update()
    {   
        playerpos = (Vector2)transform.position;
        reticlepos = (Vector2)reticle.transform.position;
        direction = reticlepos - playerpos;

        if (Input.GetKeyUp(KeyCode.G) && hasGravityPowerUp)
        {
            gravityEnabled = !gravityEnabled; //Activate gravity control
            hasChosenGravity = false;
            if (!gravityEnabled)
            {
                //Return retile color after disable control
                Physics2D.gravity = new Vector2(0, -9.8f);
                 Color reticleColor = reticle.GetComponent<SpriteRenderer>().color;
                if (reticleColor == Color.green)
                {
                    ResetReticleColor(); // Only reset if color is green
                }
                // ResetReticleColor();
                hasGravityPowerUp = false;
            }
        }

        //Change gravity
        if (gravityEnabled && !hasChosenGravity)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Physics2D.gravity = new Vector2(0, 9.8f); //Up
                hasChosenGravity = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Physics2D.gravity = new Vector2(0, -9.8f); //Down
                hasChosenGravity = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Physics2D.gravity = new Vector2(-9.8f, 0); //Left
                hasChosenGravity = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Physics2D.gravity = new Vector2(9.8f, 0); //Right
                hasChosenGravity = true;
            }
        }


        if (Input.GetKey("space") && isGrounded)
        {   
            reticleSpeed = 6.0f;
            if (increasingDistance)
            {   
                currentReticleDistance += reticleSpeed * Time.deltaTime;
                if (currentReticleDistance >= maxReticleDistance)
                {   
                    increasingDistance = false;
                }   
            }
            reticle.transform.position = (Vector2)transform.position + direction.normalized * currentReticleDistance / 5.0f;
        }

        if (Input.GetKeyUp("space") && isGrounded)
        {   
            if (currentReticleDistance >= maxReticleDistance * 0.99f)  // 如果蓄力接近满
            {
                speedMultiplier = 1.1f;
            }
            Vector2 jumpDirection = (Vector2)(reticle.transform.position - transform.position).normalized;
            float jumpForce = currentReticleDistance * speed * speedMultiplier;

            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            currentReticleDistance = minReticleDistance;
            reticle.transform.position = (Vector2)transform.position + direction.normalized * currentReticleDistance / 5.0f;
            increasingDistance = true;
        }

        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Return) && TrapBoom)
        {
            reticleSpeed = 6.0f;
            if (increasingDistance)
            {
                currentReticleDistance += reticleSpeed * Time.deltaTime;
                if (currentReticleDistance >= maxReticleDistance)
                {
                    increasingDistance = false;
                }
            }
            reticle.transform.position = (Vector2)transform.position + direction.normalized * currentReticleDistance / 5.0f;
        }

        if (Input.GetKeyUp(KeyCode.Return) && TrapBoom)
        {
            if (currentReticleDistance >= maxReticleDistance * 0.99f)
            {
                speedMultiplier = 1.1f;
            }
            Vector2 jumpDirection = (Vector2)(reticle.transform.position - transform.position).normalized;
            float jumpForce = currentReticleDistance * speed * speedMultiplier;

            ShootProjectile(jumpDirection, jumpForce);
            SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
            reticleRenderer.color = originalReticleColor;
            TrapBoom = false;

            currentReticleDistance = minReticleDistance;
            increasingDistance = true;
        }
        // if (Input.GetKeyDown(KeyCode.Return) && TrapBoom)
        // {   
        //     reticleSpeed = 6.0f;
           
        //     Debug.Log("increasingDistance  " + increasingDistance);
        //     if (increasingDistance)
        //     {   
        //         currentReticleDistance += reticleSpeed * Time.deltaTime;
        //         if (currentReticleDistance >= maxReticleDistance)
        //         {   
        //             increasingDistance = false;
        //         }   
        //     }
        //     reticle.transform.position = (Vector2)transform.position + direction.normalized * currentReticleDistance / 5.0f; 
        // }

        // if(Input.GetKeyUp(KeyCode.Return) && TrapBoom)
        // {   
        //     if (currentReticleDistance >= maxReticleDistance * 0.99f)
        //     {
        //         speedMultiplier = 1.1f;
        //     }
        //     Vector2 jumpDirection = (Vector2)(reticle.transform.position - transform.position).normalized;
        //     float jumpForce = currentReticleDistance * speed * speedMultiplier;

        //     ShootProjectile(jumpDirection, jumpForce);
        //     SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        //     reticleRenderer.color = originalReticleColor;
        //     TrapBoom = false;
        // }
        // WallSlide();
        // WallJump();
        // Flip();
    }

    // void Flip()
    // {
    //     if ((facingRight && reticle.transform.position.x - transform.position.x < 0) || (!facingRight && reticle.transform.position.x - transform.position.x > 0))
    //     {
    //         facingRight = !facingRight;
    //         Vector3 localScale = transform.localScale;
    //         localScale.x *= -1f;
    //         transform.localScale = localScale;
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            rb.gravityScale = regGrav;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
            rb.gravityScale = 0.1f;
            reticleSpeed = 100.0f;
        }

        if (collision.gameObject.CompareTag("JumpPlatform"))
        {
            // Apply a boosted jump force automatically
            Vector2 jumpDirection = Vector2.up;  // Modify if you want a different direction
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset Y velocity for a clean jump
            rb.AddForce(jumpDirection * specialPlatformJumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
            rb.gravityScale = regGrav;
            reticleSpeed = 6.0f;
        }
    }

    bool IsGrounded()
    {
        return true;
    }

    bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    // void WallSlide()
    // {
    //     if (IsWalled() && !IsGrounded())
    //     {
    //         wallSliding = true;
    //         rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -2f, float.MaxValue));
    //         reticlecenter.transform.rotation = Quaternion.Lerp(reticlecenter.transform.rotation, Quaternion.Euler(Vector3.forward * Mathf.Clamp(reticlecenter.transform.rotation.z, 10f, 170f)), 30f * Time.deltaTime);
    //     }
    //     else
    //     {
    //         wallSliding = false;
    //     }
    // }
    
    // void WallJump()
    // {
    //     if (wallSliding && Input.GetKeyDown("space"))
    //     {
    //         rb.velocity = Vector2.zero;
    //         playerpos = (Vector2)transform.position;
    //         reticlepos = (Vector2)reticle.transform.position;
    //         direction = reticlepos - playerpos;
    //         rb.AddForce(direction.normalized * speed);
    //     }
    // }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}