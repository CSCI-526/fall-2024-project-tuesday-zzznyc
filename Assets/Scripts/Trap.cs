using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.CompareTag("BOOM"))
        {
            // 子弹碰到陷阱时销毁陷阱
            Destroy(gameObject);
        }
    }
}
