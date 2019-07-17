using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{

    private const string SAVE_FILE_NAME = "/levelCompleted.save";

    public static void SaveProgress(string LevelName)
    {
        Debug.Log("Saving Progress: " + LevelName);

        SaveData sd = LoadProgress();
        if(!sd.lstLevelsComplete.Contains(LevelName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + SAVE_FILE_NAME;

            FileStream fs = new FileStream(path, FileMode.Create);

            sd.lstLevelsComplete.Add(LevelName);

            bf.Serialize(fs, sd);

            fs.Close();
        }
    }

    public static SaveData LoadProgress()
    {
        Debug.Log("Loading Progress");
        SaveData sd = new SaveData();

        string path = Application.persistentDataPath + SAVE_FILE_NAME;
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            sd = bf.Deserialize(fs) as SaveData;

            fs.Close();
        }

        return sd;
    }
}
