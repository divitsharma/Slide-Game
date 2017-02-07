using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public Menu curMenu;

    public void ShowMenu(Menu menu)
    {
        if (menu != null)
            curMenu.IsOpen = false;

        curMenu = menu;
        curMenu.IsOpen = true;

    }

    void Start()
    {
        ShowMenu(curMenu);
    }

    public GameObject stagesButtonImg;
    public GameObject stagesText;
    public Menu mainMenu;

    public void StagesToMain()
    {
        stagesButtonImg.GetComponent<Animator>().Play("StagesBackgroundImgClose");
        stagesText.GetComponent<Animator>().Play("StagesTextClosed");

        ShowMenu(mainMenu);
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                    