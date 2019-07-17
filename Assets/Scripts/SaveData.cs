using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<string> lstLevelsComplete;

    public SaveData()
    {
        if (lstLevelsComplete == null)
            lstLevelsComplete = new List<string>();
    }
}
