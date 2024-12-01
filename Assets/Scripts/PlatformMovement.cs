using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float horizontalDistance; // 水平移动的距离
    public float verticalDistance ;   // 垂直移动的距离
    public float speed;              // 移动速度
    public bool startMovingRight;  // 初始水平移动方向
    public bool moveVertical;     // 是否同时进行垂直移动

    private Vector2 startPosition;
    private bool movingRight;
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        movingRight = startMovingRight;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 计算水平目标位置
        Vector2 horizontalTarget = startPosition + (movingRight ? Vector2.right : Vector2.left) * horizontalDistance;

        // 计算垂直目标位置（基于正弦波实现上下循环移动）
        float verticalOffset = moveVertical ? Mathf.Sin(Time.time * speed) * verticalDistance * 10.0f : 0;
        Vector2 targetPosition = new Vector2(horizontalTarget.x, startPosition.y + verticalOffset);

        // 使用 Rigidbody2D 移动平台
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // 检查是否到达水平目标位置，并反转水平移动方向
        if (Mathf.Abs(rb.position.x - horizontalTarget.x) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }
}