using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectSliderButton : MonoBehaviour {

    public ScenesManager sceneManager;
    public int cost;

    // Name of the slider this button represents
    private string sliderName;

    public void Select() // implies it is already bought or can be bought
    {        
        if (PlayerPrefs.GetInt(sliderName) != 1) // == 1 would mean its bought
        {
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") - cost);
            cost = 0;
            
            // Change text of users gems
            sceneManager.SetGemText();

            PlayerPrefs.SetInt(sliderName, 1); // set bought
        }

        // Set a string in playerprefs to the slider sprite's name
        PlayerPrefs.SetString("CurrentSlider", sliderName);


        // Change button elements to show its selected
        GetComponent<Image>().sprite = Resources.Load<Sprite>("SliderSelectedBackground");
        GetComponentInChildren<Text>().text = "SELECTED";
        
        transform.GetChild(0).transform.FindChild("GemImage").GetComponent<Image>().enabled = false;
    }

    public void Unselect()
    {
        // Change background picture and text
        GetComponent<Image>().sprite = Resources.Load<Sprite>("SliderSelectBackground");
        
        GetComponentInChildren<Text>().text = "SELECT";

    }

	// Use this for initialization
	void Start () {
        // Get the slider name
        sliderName = transform.Find("SliderImage").GetComponent<Image>().sprite.name;

        // Set button text according to whether its been bought
        if (PlayerPrefs.GetInt(sliderName) == 1)
            GetComponentInChildren<Text>().text = "SELECT";
        else
            GetComponentInChildren<Text>().text = cost.ToString();

        // If this is the current slider, set it as selected
        if (PlayerPrefs.GetString("CurrentSlider") == sliderName)
            Select();


	}

}
