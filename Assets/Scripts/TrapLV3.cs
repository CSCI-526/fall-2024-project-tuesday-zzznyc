using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLV3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 respawnPosition = new Vector2(0, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家传送到固定位置
            collision.gameObject.transform.position = respawnPosition;
        }
        else if (collision.gameObject.CompareTag("BOOM"))
        {
            // 子弹碰到陷阱时摧毁子弹
            Destroy(collision.gameObject);
        }
    }
}
