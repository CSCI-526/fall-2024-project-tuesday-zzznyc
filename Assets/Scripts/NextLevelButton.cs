using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string prev = "previousLevel";
        int next = PlayerPrefs.GetInt(prev);
        if (next == 7)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
