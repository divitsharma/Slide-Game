using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Manager : MonoBehaviour {
   
    // Menus
    public GameObject pauseMenu;
    public GameObject clearedMenu;

    // Velocity before pausing
    Vector3 previousVel;

    // Gems collected only in this scene
    public int gemsCollected;

    // All gems
    GameObject[] gems;

    // Makes script accessable by everything
    static Manager managerInst;
    public static Manager ManagerInst
    {
        get 
        {
            if (managerInst == null)
                managerInst = GameObject.FindObjectOfType<Manager>();
            return Manager.managerInst;
        }
    }
    
    // Stages of play
    bool paused;

    public bool Paused
    {
        get { return paused; }
    }


    public void Reset()
    {
        /*GameObject[] ballObjects = GameObject.FindGameObjectsWithTag("Slider");

        foreach (GameObject ballObject in ballObjects)
        {
            Move moveScript = ballObject.GetComponent<Move>();
            moveScript.inPlay = false;
            moveScript.positioning = true;
            moveScript.sliderHit = false;
            moveScript.destroyed = false;
            moveScript.rb.transform.position = moveScript.placedPos;
            moveScript.rb.velocity = Vector2.zero;
            moveScript.rb.angularVelocity = 0;
            moveScript.sr.color = new Color(moveScript.sr.color.r, moveScript.sr.color.g, moveScript.sr.color.b, 1);

        }
        

        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("Hole");
        foreach (GameObject hole in holeObjects)
        {
            hole.GetComponentInChildren<EdgeCollider2D>().enabled = false;
            hole.GetComponent<BallEntered>().entered = false;
        }

        gemsCollected = 0;
        LoadGems(PlayerPrefs.GetInt("Stars" + SceneManager.GetActiveScene().buildIndex) < 2);

        //foreach (GameObject gem in gems)
        //{
        //    gem.SetActive(!(PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString()) == 1 ? true : false));
        //}*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PauseGame()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        Rigidbody2D ballRb = GameObject.FindGameObjectWithTag("Slider").GetComponent<Move>().rb;
        if (paused)
        {
            previousVel = ballRb.velocity;

            ballRb.velocity = Vector3.zero;
        }
        else
            ballRb.velocity = previousVel;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadGems(bool state)
    {
        foreach (GameObject gem in gems)
        {
            gem.SetActive(state);
        }
    }

    void Awake()
    {
        pauseMenu.SetActive(false);
        clearedMenu.SetActive(false);

        // whether this level is beaten yet is set in the playerpref with key of scene index as string
        gems = GameObject.FindGameObjectsWithTag("Gem");

        // Load the gems if stars of this level is less than 3, so more can be collected
        LoadGems(PlayerPrefs.GetInt("Stars"+SceneManager.GetActiveScene().buildIndex) < 2);
    }




    //GameObject lineGO;
    //LineRenderer lineRenderer;
    //int i = 0;
    //void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            lineRenderer.SetVertexCount(i + 1);
    //            Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15);
    //            lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
    //            i++;
    //        }
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            /* Remove Line */

    //            lineRenderer.SetVertexCount(0);
    //            i = 0;
    //        }
    //    }
    //}
}
