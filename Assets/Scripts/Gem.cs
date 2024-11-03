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
    [SerializeField] private Color gemColors;
    private bool isCollected = false;

    private void Start()
    {
        // 获取当前Gem的颜色并赋值给gemColors
        SpriteRenderer gemRenderer = GetComponent<SpriteRenderer>();
        if (gemRenderer != null)
        {
            gemColors = gemRenderer.color;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            collision.GetComponent<PlayerMovement>().ChangeReticleColor(gemColors);

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