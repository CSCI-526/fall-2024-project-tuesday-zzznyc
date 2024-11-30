using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float horizontalDistance; // 水平移动的距离
    public float verticalDistance;   // 垂直移动的距离
    public float horizontalSpeed;    // 水平移动速度
    public float verticalSpeed;      // 垂直移动速度
    public bool startMovingRight;    // 初始水平移动方向
    public bool moveVertical;        // 是否进行垂直移动

    private Vector2 startPosition;
    private bool movingRight;
    private bool movingUp; // 垂直移动方向
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        movingRight = startMovingRight;
        movingUp = true; // 默认开始向上移动
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 水平目标位置计算
        float targetX = startPosition.x + (movingRight ? horizontalDistance : -horizontalDistance);
        
        // 垂直目标位置计算
        float targetY = moveVertical
            ? startPosition.y + (movingUp ? verticalDistance : -verticalDistance)
            : rb.position.y; // 如果不垂直移动，则保持当前垂直位置
        
        // 计算目标位置
        Vector2 targetPosition = new Vector2(targetX, targetY);

        // 移动到目标位置
        float currentHorizontalSpeed = horizontalSpeed * Time.fixedDeltaTime;
        float currentVerticalSpeed = verticalSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, Mathf.Max(currentHorizontalSpeed, currentVerticalSpeed));
        rb.MovePosition(newPosition);

        // 检查是否到达水平目标位置并反转方向
        if (Mathf.Abs(rb.position.x - targetX) < 0.1f)
        {
            movingRight = !movingRight;
        }

        // 检查是否到达垂直目标位置并反转方向
        if (moveVertical && Mathf.Abs(rb.position.y - targetY) < 0.1f)
        {
            movingUp = !movingUp;
        }
    }
}