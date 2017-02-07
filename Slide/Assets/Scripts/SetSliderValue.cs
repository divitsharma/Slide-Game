using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetSliderValue : MonoBehaviour {
    public string key;
	// Use this for initialization
	void Start () {
        Debug.Log(PlayerPrefs.HasKey(key));
        if (PlayerPrefs.HasKey(key))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat(key);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
