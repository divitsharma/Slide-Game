using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {
        if (PlayerPrefs.GetInt("Launched") == 0)
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.offsetMax = rect.offsetMin = Vector2.zero;

            Destroy(this.gameObject, 1.5f);
            PlayerPrefs.SetInt("Launched", 1);
        }
    }

}
