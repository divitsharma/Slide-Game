using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectSlider : MonoBehaviour {


    public void ChangeCurrentSliderSprite()
    {
        GameObject buttonPressed = EventSystem.current.currentSelectedGameObject;
        if (PlayerPrefs.GetInt("Gems") >= buttonPressed.GetComponent<SelectSliderButton>().cost)
        {
            // Unselect other buttons
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                if (button.gameObject.GetComponent<Image>().sprite.name == "SliderSelectedBackground")
                    button.GetComponent<SelectSliderButton>().Unselect();
            }

            buttonPressed.GetComponent<SelectSliderButton>().Select();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
