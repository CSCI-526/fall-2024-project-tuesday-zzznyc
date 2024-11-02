// using UnityEngine;

// public class Gem : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             // 调用玩家脚本中的方法来更改 reticle 颜色
//             collision.GetComponent<PlayerMovement>().ChangeReticleColor(Color.blue);
//             // gameObject.SetActive(True); 
//         }
//     }
// }

using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // 改变 reticle 颜色
            collision.GetComponent<PlayerMovement>().ChangeReticleColor(Color.blue);

            // 暂时隐藏 gem，然后重新激活
            StartCoroutine(TemporaryDisable());
        }
    }

    private IEnumerator TemporaryDisable()
    {
        gameObject.GetComponent<Renderer>().enabled = false; // 隐藏外观而不禁用对象
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Renderer>().enabled = true;
        isCollected = false;
    }
}