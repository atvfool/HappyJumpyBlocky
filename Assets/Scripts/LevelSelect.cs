using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int MaxLevels = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This does some stuff, Saves the current level we're loading and determines if there's a next one, this may just be needlessly complex and I may strip it out later
    /// </summary>
    /// <param name="Level"></param>
    public void SelectLevel(string Level)
    {
        if (Level == string.Empty) // If no level selected
            Level = "LevelSelect";

        PlayerPrefs.SetString("CurrentLevel", Level);

        string cleanedLevelName = Level.Replace("Level", "");
        int tempInt = 0;
        string nextLevel = string.Empty;
        if (int.TryParse(cleanedLevelName, out tempInt)) // If this is a level then this should return the level number, if not it sets it to blank
        {
            if (tempInt < MaxLevels)
                nextLevel = "Level" + (tempInt + 1);
        }

        PlayerPrefs.SetString("NextLevel", nextLevel);

        UnityEngine.SceneManagement.SceneManager.LoadScene(Level);
    }
}
