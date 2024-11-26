using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector2 currentCheckpoint; // 当前的检查点位置
    private GameObject player; // 玩家对象

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 查找玩家对象
        if (player != null)
        {
            currentCheckpoint = player.transform.position; // 初始化为玩家初始位置
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint; // 更新检查点
        Debug.Log("Checkpoint updated to: " + newCheckpoint);
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            player.transform.position = currentCheckpoint; // 将玩家位置设置为检查点
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 重置速度
            }
        }
    }
}
