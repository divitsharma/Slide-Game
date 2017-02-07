using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class ScenesManager : MonoBehaviour {

    public Text gemNumber;
    public GameObject resetPanel;
    public GameObject guidePanel;

    GameObject musicPlayer;
    public AudioClip[] loops;

    public void ToggleResetPanel()
    {
        resetPanel.SetActive(!resetPanel.activeSelf);
    }

    public void ToggleGuidePanel()
    {
        guidePanel.SetActive(!guidePanel.activeSelf);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        // Set default slider to bought
        PlayerPrefs.SetInt("Slider", 1); // set bought
        PlayerPrefs.SetString("CurrentSlider", "Slider");

        // Reload scene
        SceneManager.LoadScene(0);
    }

    public void ChangeToScene(int index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            musicPlayer.GetComponent<AudioSource>().clip = loops[index / 13 + 1];
            musicPlayer.GetComponent<AudioSource>().Play();
            musicPlayer.GetComponent<MusicPlayer>().PlaySting();
            SceneManager.LoadScene(index);

        }
    }

    public void Rate()
    { //"http://play.google.com/store/apps/details?id=" + Application.bundleIdentifier
        Application.OpenURL("http://play.google.com/store/apps/details?id=" + Application.bundleIdentifier); 
    }

    public void SetGemText()
    {
        gemNumber.text = PlayerPrefs.GetInt("Gems").ToString();
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("Launched", 0);
        Application.Quit();
    }


    void Start()
    {
        // Create a gems variable if it is not already there
        if (!PlayerPrefs.HasKey("Gems"))
            PlayerPrefs.SetInt("Gems", 0);

        SetGemText();

        if (!PlayerPrefs.HasKey("CurrentSlider"))
            PlayerPrefs.SetString("CurrentSlider", "Slider");


        resetPanel.GetComponent<RectTransform>().offsetMin = resetPanel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        resetPanel.SetActive(false);

        guidePanel.GetComponent<RectTransform>().offsetMin = guidePanel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        guidePanel.SetActive(false);

        musicPlayer = GameObject.FindGameObjectWithTag("Music");

        musicPlayer.GetComponent<AudioSource>().clip = loops[0];
        musicPlayer.GetComponent<AudioSource>().Play();

    }
}
