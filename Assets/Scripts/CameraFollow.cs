using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 拖入 Player 的 Transform
    public Vector3 offset;   // 摄像机相对于 Player 的偏移量
    public float smoothSpeed = 0.125f; // 平滑跟随速度

    void LateUpdate()
    {
        // 计算目标位置
        Vector3 targetPosition = player.position + offset;

        // 平滑过渡到目标位置
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // 设置摄像机位置
        transform.position = smoothedPosition;
    }
}