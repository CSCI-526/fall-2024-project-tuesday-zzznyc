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
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            // 检查 gem 的颜色，并根据颜色更改 player 或 reticle 的颜色
            if (gemColors == Color.green)
            {   
                // Debug.Log("greengreengreengreen");
                player.ChangePlayerColor(Color.green); // 如果 gem 是绿色，则更改 player 的颜色为绿色
            }
            else if (gameObject.CompareTag("BOOM"))
            {   
                // Debug.Log("BoomBoomBoomBoomBoom");
                player.ChangeReticleColor(Color.red); // 如果 Gem 的标签为 Boom，则将 Player 的颜色更改为红色
            }
            else if (gemColors == Color.red)
            {   
                // Debug.Log("redredredredred");
                player.ChangeReticleColor(Color.red); // 如果 gem 是红色，则更改 reticle 的颜色为红色
            }
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