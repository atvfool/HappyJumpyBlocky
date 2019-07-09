using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpScript : Selectable
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsBtnPressed()
    {
        return IsPressed();
    }
}
