using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSettingScene : MonoBehaviour
{
    // 用于加载 Setting 场景
    public void LoadSettingScene()
    {   
        Time.timeScale = 0;
        SceneManager.LoadScene("Setting"); 
        
    }
}