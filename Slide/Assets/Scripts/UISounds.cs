using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UISounds : MonoBehaviour {

    public AudioClip clickSound;
    private AudioSource clickSource;

	// Use this for initialization
	void Start () {
        clickSource = GetComponent<AudioSource>();
        clickSource.clip = clickSound;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (/*Input.GetTouch(0).phase == TouchPhase.Began || */Input.GetMouseButtonDown(0))
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
            {
                clickSource.pitch = Random.Range(0.8f, 1.2f);
                clickSource.PlayOneShot(clickSound);
            }
    }
}
