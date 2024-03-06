using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static int currLevel = 0;
    public static string[] levels = new string[] { "Level 1", "Level 2" };
    public static string WinSceneName = "WinScene";


    public static int GoToNextLevel()
    {
        currLevel = (currLevel + 1) % levels.Length;
        return currLevel;
    }

    public static int GetCurrLevel()
    {
        return currLevel;
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
