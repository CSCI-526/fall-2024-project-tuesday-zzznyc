using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLV3 : MonoBehaviour
{
    // 设置玩家传送位置
    public Vector2 respawnPosition = new Vector2(0, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家传送到固定位置
            collision.gameObject.transform.position = respawnPosition;

            // 停止玩家一切运动
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // 停止线性速度
                rb.angularVelocity = 0f;    // 停止角速度
            }
        }
        else if (collision.gameObject.CompareTag("BOOM"))
        {
            // 子弹碰到陷阱时摧毁子弹
            Destroy(gameObject);
        }
    }
}
