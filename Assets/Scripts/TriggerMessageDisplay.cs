using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerMessageDisplay : MonoBehaviour
{
    public FadingText fadingText;  // 引用 FadingText 脚本
    public string message = "Arrow Keys/ AD: Control Direction";  // 默认消息
    public float customDisplayTime = 3f;     // 自定义显示时间
    public float customFadeTime = 2f;        // 自定义淡出时间

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 检查是否是玩家进入触发区域
        {
            // 使用自定义消息和时间
            fadingText.ShowMessage(message, customDisplayTime, customFadeTime);
        }
    }
}
