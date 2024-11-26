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
    // private bool TrapBoom = false;

    private bool GravityNotUsed = true; //Got gravity item
    // private bool gravityEnabled = false;    //Enabled gravity change
    // private bool hasChosenGravity = false;

    [SerializeField] float specialPlatformJumpForce = 15f;  // New: Boosted jump force for special platform
    void Start()
    {           
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
    
    void Update()
    {   
        // Debug.Log("isGrounded  " + isGrounded);
        playerpos = (Vector2)transform.position;
        reticlepos = (Vector2)reticle.transform.position;
        direction = reticlepos - playerpos;
        // Debug.Log("GravityNotUsedGravityNotUsed111");
        if (IsPlayerColorEqual(Color.green) && GravityNotUsed)
        {   
            Debug.Log("GravityNotUsedGravityNotUsed222");
            CheckGravityInput();
            
        }
        if (Input.GetKeyDown(KeyCode.G) && IsPlayerColorEqual(Color.green))
        {   
            /*if(IsReticleColorEqual(Color.red)){
                return;
            }*/
            Physics2D.gravity = new Vector2(0, -9.8f); // Reset gravity to default
            ResetPlayerColor(); // Only reset if color is green
            GravityNotUsed = true;
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

        if (Input.GetKey(KeyCode.Return) && IsReticleColorEqual(Color.red))
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

        if (Input.GetKeyUp(KeyCode.Return) && IsReticleColorEqual(Color.red))
        {
            if (currentReticleDistance >= maxReticleDistance * 0.99f)
            {
                speedMultiplier = 1.1f;
            }
            Vector2 jumpDirection = (Vector2)(reticle.transform.position - transform.position).normalized;
            float jumpForce = currentReticleDistance * speed * speedMultiplier;

            ShootProjectile(jumpDirection, jumpForce);
            
            ResetReticleColor();
            currentReticleDistance = minReticleDistance;
            increasingDistance = true;
        }

        if (Input.GetKeyUp("r"))
        {
            // 调用 CheckpointManager 来重生玩家
            CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
            if (checkpointManager != null)
            {
                checkpointManager.RespawnPlayer(); // 使用检查点重生玩家
            }
            else
            {
                // 如果 CheckpointManager 不存在，回退到重置关卡的逻辑
                string currentscene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentscene);
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }

     private void OnTriggerEnter2D(Collider2D other)
    {
    }
    
    private int contactCount = 0; // 用于跟踪接触物体的数量

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("JumpPlatform"))
    {
        contactCount++;
        isGrounded = true; // 有接触时，设置isGrounded为true
    }

    if (collision.gameObject.CompareTag("Floor"))
    {
        rb.gravityScale = regGrav;
    }

    if (collision.gameObject.CompareTag("Wall"))
    {
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
    if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("JumpPlatform"))
    {
        contactCount--;
        if (contactCount <= 0)
        {
            contactCount = 0; // 防止计数器变为负数
            isGrounded = false; // 没有接触物体时，设置isGrounded为false
            rb.gravityScale = regGrav;
            reticleSpeed = 6.0f;
        }
    }
}

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public bool IsPlayerColorEqual(Color targetColor)
    {
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            return playerRenderer.color == targetColor;
        }
        return false;
    }


    public bool IsReticleColorEqual(Color targetColor)
    {
        if (reticle != null)
        {
            SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
            if (reticleRenderer != null)
            {
                return reticleRenderer.color == targetColor;
            }
        }
        return false;
    }
    
    public void ChangePlayerColor(Color newColor)
    {
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.color = newColor;
        }
    }

    public void ChangeReticleColor(Color newColor)
    {
        SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        if (reticleRenderer != null)
        {
            reticleRenderer.color = newColor;
        }
    }

    public void ResetPlayerColor()
    {
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.color = Color.white; // 将颜色重置为白色，或你可以替换为初始颜色
        }
    }

    private void ResetReticleColor()
    {
        SpriteRenderer reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        reticleRenderer.color = originalReticleColor;
    }

    private void CheckGravityInput()
    {   
        // 根据键盘输入来设置重力方向
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetGravityDirection(Vector2.up); // 上
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetGravityDirection(Vector2.down); // 下
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetGravityDirection(Vector2.left); // 左
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetGravityDirection(Vector2.right); // 右
        }
       
    }

    private void SetGravityDirection(Vector2 direction)
    {
        Physics2D.gravity = direction * 9.8f; // 乘以 9.8f 设置重力方向
        GravityNotUsed = false;
    }

    private void ShootProjectile(Vector2 direction, float distance)
    {   
        Color reticleColor = reticle.GetComponent<SpriteRenderer>().color;
        if (reticleColor == Color.green)
        {
            return;
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

    public void SetGravityNotUsed(bool value)
    {
        GravityNotUsed = value;
        Debug.Log(" GravityNotUsed = value;" +  GravityNotUsed);
    }
}