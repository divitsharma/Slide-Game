using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float xStrength, yStrength;

    // Variables for swipe function
    float startTime, dTime;
    public float minDist = 1;

    // Finger move distance since start point
    Vector3 dist;
    Vector3 startPos;
    public Vector3 placedPos;

    // States of the ball
    public bool sliderHit = false;
    public bool positioning = true;
    public bool inPlay = false;
    public bool destroyed = false;
    public bool onBridge = false;

    public float strengthFactor;
    public float maxSpeed;
    public float friction;


    public AudioClip slideSound;
    private AudioSource slideSource;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets rigidbody component from the slider
        sr = GetComponent<SpriteRenderer>();
        
        if (PlayerPrefs.HasKey("CurrentSlider"))
            sr.sprite = Resources.Load<Sprite>("Sliders/" + PlayerPrefs.GetString("CurrentSlider"));
        else
            sr.sprite = Resources.Load<Sprite>("Sliders/Slider");

        slideSource = gameObject.AddComponent<AudioSource>();
        slideSource.clip = slideSound;

        friction = 0.99f;
    }

    public void Destroy()
    {
        destroyed = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0f);

        GetComponent<CircleCollider2D>().enabled = false;
    }

    /****** Drag the ball to place it ******/
    void PositionSlider(Touch touch)
    {
        
        // Just touched
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 curPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D colliderHit = Physics2D.Raycast(curPosWorld, Vector2.zero);

            if (colliderHit.collider != null && colliderHit.collider.gameObject.tag == "Slider")
            {
                sliderHit = true;
                rb.transform.position = new Vector3(curPosWorld.x, curPosWorld.y, -2);
            }
            else sliderHit = false;
        }

        // Moving fingers around
        else if (sliderHit && touch.phase == TouchPhase.Moved)
        {
            Vector3 curPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
            // If finger is inside barrier and above bottom of screen
            if (curPosWorld.y < -3.6 && curPosWorld.y > -4.56)
                rb.transform.position = new Vector3(curPosWorld.x, curPosWorld.y, -2);
            else if (curPosWorld.y < -4.56) rb.transform.position = new Vector3(curPosWorld.x, -4.56f, -2);
            else
                rb.transform.position = new Vector3(curPosWorld.x, -3.6f, -2);
        }

        // Lift finger, update current placed position
        else if (sliderHit && touch.phase == TouchPhase.Ended)
        {
            sliderHit = false;
            positioning = false;
            placedPos = rb.position;
        }
    }

    // Move finger to swipe, swipe is counted once finger is lifted or dragged too long
    void Swipe(Touch touch)
    {
        // Current position
        Vector3 curPosWorld = Camera.main.ScreenToWorldPoint(touch.position);

        // First touch, see if ball was touched, if so, set start time
        if (touch.phase == TouchPhase.Began)
        {
            // Send ray to touch position and return whether it collided with something
            RaycastHit2D colliderHit = Physics2D.Raycast(curPosWorld, Vector2.zero);

            if (colliderHit.collider != null && colliderHit.collider.gameObject.tag == "Slider")
            {
                sliderHit = true;
                startTime = Time.time;
                startPos = curPosWorld; // IMPORTANT: in world units
            }
        }

        // Moving ball around with fingers until a certain point, after which move on to the next if-statement
        else if (sliderHit && touch.phase == TouchPhase.Moved)
        {
            // If finger is inside minimum swipe distance
            dist = curPosWorld - startPos;
            if (Mathf.Abs(dist.magnitude) < minDist)
                rb.transform.position = placedPos + new Vector3(dist.x, dist.y, 0);
        }

        // If finger is lifted or dragged too long
        if (sliderHit && (touch.phase == TouchPhase.Ended || Mathf.Abs(dist.magnitude) > minDist))
        {
            dTime = Time.time - startTime;

            dist = curPosWorld - startPos;

            xStrength = (dist.x / dTime) * strengthFactor;
            yStrength = (dist.y / dTime) * strengthFactor;

            if (Mathf.Abs(xStrength) > 1000) xStrength = xStrength/Mathf.Abs(xStrength) * 1000;
            if (Mathf.Abs(yStrength) > 1000) yStrength = yStrength / Mathf.Abs(yStrength) * 1000;
            rb.AddForce(new Vector2(xStrength, yStrength));

            inPlay = true;

            // Reset Variables
            dist = new Vector3(0, 0, 0);
            sliderHit = false;

            slideSource.PlayOneShot(slideSound, 1f);
        }
    }

    // MAIN LOOP: Update is called once per frame
    void Update()
    {
        if (!Manager.ManagerInst.Paused && !destroyed)
        {
            rb.velocity = new Vector2(rb.velocity.x * friction, rb.velocity.y * friction); // "friction"

            // Positioning will only be true again if reset is pressed
            if (positioning && !inPlay)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f); // Reduce opacity
            else
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

            // If touching
            if (Input.touchCount == 1 && !inPlay)
            {
                Touch touch = Input.GetTouch(0);

                if (positioning)
                    PositionSlider(touch);
                else
                {
                    Swipe(touch);
                }

            }

        }
    }
}