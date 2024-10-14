using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float moveDistance = 5f;       // 水平移动的距离
    public float verticalDistance = 3f;   // 垂直移动的距离
    public float speed = 2f;              // 移动速度
    public bool startMovingRight = true;  // 初始移动方向，true 表示向右，false 表示向左
    public bool moveVertical = false;     // 是否开启上下移动

    private Vector3 startPosition;
    private bool movingRight;

    void Start()
    {
        // 记录初始位置并设置初始方向
        startPosition = transform.position;
        movingRight = startMovingRight;
    }

    void Update()
    {
        // 计算水平目标位置
        Vector3 horizontalTarget = startPosition + (movingRight ? Vector3.right : Vector3.left) * moveDistance;
        // 计算垂直目标位置
        Vector3 verticalTarget = startPosition + Vector3.up * Mathf.Sin(Time.time * speed) * verticalDistance;

        // 平滑移动到目标位置
        Vector3 targetPosition;
        if (moveVertical)
        {
            // 同时上下和左右移动
            targetPosition = new Vector3(horizontalTarget.x, verticalTarget.y, startPosition.z);
        }
        else
        {
            // 仅左右移动
            targetPosition = new Vector3(horizontalTarget.x, startPosition.y, startPosition.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 检查是否到达水平目标位置，并反转移动方向
        if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(horizontalTarget.x, 0, 0)) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }
}