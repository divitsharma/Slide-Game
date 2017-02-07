using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private Animator animator;
    private CanvasGroup canvasGroup;

    public bool IsOpen
    {
        get { return animator.GetBool("IsOpen"); }
        set { animator.SetBool("IsOpen", value); }
    }


	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();

        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
        else
            canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
	}
}
