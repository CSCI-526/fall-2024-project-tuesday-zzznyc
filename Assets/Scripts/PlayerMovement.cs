using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{   
    bool isGrounded = false;
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject reticlecenter;

    Rigidbody2D rb;
    bool facingRight = true;
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
    private bool isCustomColorActive = false;

    void Start()
    {   
        // minReticleDistance;
        // maxReticleDistance = 6.0f;
        speed = 1.0f;
        speedMultiplier = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        currentReticleDistance = minReticleDistance;

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
            isCustomColorActive = true;
        }
    }

    void Update()
    {   
        playerpos = (Vector2)transform.position;
        reticlepos = (Vector2)reticle.transform.position;
        direction = reticlepos - playerpos;

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

        if (Input.GetKeyDown(KeyCode.Return) && isCustomColorActive)
        {
            SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
            if (reticleRenderer != null)
            {
                reticleRenderer.color = originalReticleColor;
                isCustomColorActive = false; // 重置标记
                Debug.Log("Reticle color reverted to original.");
            }
        }

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