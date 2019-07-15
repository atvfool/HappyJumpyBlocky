using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CButton : Selectable
{

    public bool IsBtnPressed()
    {
        return IsPressed();
    }

}
