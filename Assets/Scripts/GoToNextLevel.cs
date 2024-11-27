using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    public void ToNextLevel()
    {
        string prev = "previousLevel";
        int next = PlayerPrefs.GetInt(prev) + 1;
        Debug.Log("next int =" + next);
        SceneManager.LoadScene("Level " + next);
    }
}
