using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测玩家是否碰到检查点
        if (other.CompareTag("Player"))
        {
            // 更新玩家当前检查点位置
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SetCurrentCheckpoint(transform.position);
                Debug.Log("CheckPoint activated at: " + transform.position);
            }
        }
    }
}
