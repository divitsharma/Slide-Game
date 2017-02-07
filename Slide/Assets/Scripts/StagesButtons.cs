using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StagesButtons : MonoBehaviour {

    public bool pressed = false;

    public int thisStage;
    
    public void Pressed()
    {
        pressed = !pressed;
        if (pressed)
        {
            //GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            GetComponent<Animator>().Play("ButtonSwipeLeft");
            Button[] buttons = transform.parent.GetComponentsInChildren<Button>();
            

            // Disable each button that is not this one 
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].gameObject.GetComponent<StagesButtons>() == null)
                {
                    // need to check if previous level was beaten, playerprefs(previouslevelbuildindex ==1 or not)
                    if (thisStage + i > 0 && PlayerPrefs.GetInt((thisStage + i).ToString()) != 1) //previous not beaten
                    {
                        buttons[i].interactable = false;
                        buttons[i].gameObject.GetComponentInChildren<Text>().color = Color.gray;
                        buttons[i].transform.Find("Star").GetComponent<Image>().color = Color.clear;
                        buttons[i].transform.Find("Star (1)").GetComponent<Image>().color = Color.clear;
                        buttons[i].transform.Find("Star (2)").GetComponent<Image>().color = Color.clear;
                    }
                    else // Show stars
                    {
                        // If this level was beaten
                        if (PlayerPrefs.GetInt((thisStage + i + 1).ToString()) == 1)
                        {
                            Image[] images = buttons[i].GetComponentsInChildren<Image>();
                            images[1].color = Color.white;
                            for (int j = 0; j < PlayerPrefs.GetInt("Stars" + (thisStage + i + 1).ToString()/*buttons[i].GetComponentInChildren<Text>().text*/); j++)
                            {
                                images[j + 2].color = Color.white;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GetComponent<Animator>().Play("ButtonSwipeRight");
        }
    }
    
}
