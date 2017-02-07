using UnityEngine;
using System.Collections;

public class StagesManager : MonoBehaviour {



    public void UnpressButtons()
    {
        StagesButtons[] buttons = GetComponentsInChildren<StagesButtons>();
        foreach (StagesButtons button in buttons)
        {
            if(button.pressed)
                button.Pressed();
        }
    }
}
