using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene loading

public class BackToMainMenu : MonoBehaviour
{
    // This function will be triggered by the button click
    public void GoToMainPage()
    {
        // Replace "MainMenuScene" with the exact name of your main page (menu) scene
        SceneManager.LoadScene("StartScene");
    }
}
