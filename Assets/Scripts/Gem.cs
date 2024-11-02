using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 调用玩家脚本中的方法来更改 reticle 颜色
            collision.GetComponent<PlayerMovement>().ChangeReticleColor(Color.blue);

            // 获取 GemManager 并通知它销毁当前宝石并重新生成
            gameObject.SetActive(false); 
        }
    }
}