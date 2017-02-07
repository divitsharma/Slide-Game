using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public AudioSource stingSource;
    public AudioClip sting;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
	}

    public void PlaySting()
    {
        stingSource.clip = sting;
        stingSource.PlayOneShot(sting);
    }
}
