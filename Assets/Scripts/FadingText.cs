using System.Collections;
using UnityEngine;
using TMPro;

public class FadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;  // TextMeshProUGUI 组件

    private Color originalColor;
    public float displayDuration = 3f;  // 默认显示时间
    public float fadeDuration = 2f;     // 默认淡出时间

    void Start()
    {
        // 检查 messageText 是否已正确分配
        if (messageText == null)
        {
            Debug.LogError("MessageText reference is missing!");
            return;
        }

        // 获取文本的初始颜色
        originalColor = messageText.color;
        SetTextAlpha(0); // 初始状态下文本透明
    }

    // 显示消息并启动淡出协程
    public void ShowMessage(string message, float? customDisplayDuration = null, float? customFadeDuration = null)
    {
        messageText.text = message;
        SetTextAlpha(1); // 使文本可见

        // 使用自定义显示时间，或默认时间
        float durationToDisplay = customDisplayDuration ?? displayDuration;
        float durationToFade = customFadeDuration ?? fadeDuration;

        StartCoroutine(FadeOutText(durationToDisplay, durationToFade));
    }

    // 淡出文本协程
    private IEnumerator FadeOutText(float durationToDisplay, float durationToFade)
    {
        // 显示指定的持续时间
        yield return new WaitForSeconds(durationToDisplay);

        float elapsedTime = 0f;
        while (elapsedTime < durationToFade)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / durationToFade); // 计算新的 alpha 值
            SetTextAlpha(alpha);
            yield return null;
        }

        SetTextAlpha(0); // 确保文本完全透明
        messageText.text = ""; // 清空文本内容
    }

    // 设置文本透明度的辅助方法
    private void SetTextAlpha(float alpha)
    {
        if (messageText == null) return;

        Color color = messageText.color;
        color.a = alpha;
        messageText.color = color;
    }
}
