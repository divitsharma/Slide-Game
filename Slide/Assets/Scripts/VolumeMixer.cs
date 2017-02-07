using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class VolumeMixer : MonoBehaviour {

    public AudioMixer masterMixer;

    public void ChangeMusicVolume(float vol)
    {
        PlayerPrefs.SetFloat("MusicVol", vol);

        if (vol <= -29) vol = -80;
        masterMixer.SetFloat("musicVol", vol);
    }

    public void ChangeSFXVolume(float vol)
    {
        PlayerPrefs.SetFloat("SFXVol", vol);

        if (vol <= -29) vol = -80;
        masterMixer.SetFloat("sfxVol", vol);

    }
}
