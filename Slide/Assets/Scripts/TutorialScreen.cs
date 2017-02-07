using UnityEngine;
using System.Collections;

public class TutorialScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Tutorial") || PlayerPrefs.GetInt("Tutorial") == 0)
        {
            GetComponent<Animator>().Play("TutorialShow");
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
            gameObject.SetActive(false);
	}

    public void HideTutorial()
    {
        gameObject.SetActive(false);
    }
}
