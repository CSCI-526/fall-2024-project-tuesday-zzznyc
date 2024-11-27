using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPreviousLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene current = SceneManager.GetActiveScene();
        string currname = current.name;

        Regex regex = new Regex(@"\d+");
        Match match = regex.Match(currname);
        int currnum = int.Parse(match.Value);
        //Console.WriteLine(orderNumber); // Output: 12345


        //int currnum = int.Parse(currname);
        
        string prev = "previousLevel";

        PlayerPrefs.SetInt(prev, currnum);
        Debug.Log("set int =" + currnum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
