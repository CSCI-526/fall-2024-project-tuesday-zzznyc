using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    // 加载 Setting 场景
    public void LoadSettingScene()
    {   
        Time.timeScale = 0;
        Debug.Log("Loading Setting Scene...");
        SceneManager.LoadScene("Settings",LoadSceneMode.Additive);
        Debug.Log("Setting Scene Loaded!");
        // 暂停当前游戏
        
        // SceneManager.LoadScene("Setting", LoadSceneMode.Additive); // 加载 Setting 场景（附加模式）
    }

    // 关闭 Setting 场景
    public void CloseSettingScene()
    {
        // 卸载 Setting 场景
        SceneManager.UnloadSceneAsync("Settings");

        // 恢复游戏时间
        Time.timeScale = 1;
    }

    // 返回主菜单
    public void GoToMainMenu()
    {
        // 恢复游戏时间
        Time.timeScale = 1;

        // 加载主菜单场景
        SceneManager.LoadScene("StartScene"); // 将 "StartScene" 替换为你的主菜单场景名称
    }

    // 重新启动当前关卡
    public void RestartLevel()
    {
        // 恢复游戏时间
        Time.timeScale = 1;

        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToLevel()
    {
        // 恢复游戏时间
        Time.timeScale = 1;

        // 加载主菜单场景
        SceneManager.LoadScene("LevelPickScene");
    }
}