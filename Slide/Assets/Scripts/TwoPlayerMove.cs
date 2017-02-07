using UnityEngine;
using System.Collections;

public class TwoPlayerMove : MonoBehaviour
{

    //public Rigidbody2D rb1;
    //public Rigidbody2D rb2;

    Rigidbody2D rb;
    
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
    public bool inPlay = false;

    public float strengthFactor;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets rigidbody component from the slider
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

            if (colliderHit.collider != null && colliderHit.collider.gameObject.tag == "Player1")
            {
                //rb = colliderHit.collider.gameObject.GetComponent<Rigidbody2D>();
                placedPos = rb.transform.position;

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
            rb.AddForce(new Vector2(xStrength, yStrength));


            // Reset Variables
            dist = new Vector3(0, 0, 0);
            sliderHit = false;
        }
    }

    // MAIN LOOP: Update is called once per frame
    void Update()
    {
        //if (!Manager.ManagerInst.Paused)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y * 0.99f); // "friction"

            // If touching
            if (Input.touchCount > 0 && !inPlay)
            {
                Touch touch;
                touch = Input.GetTouch(0);
                
                // if first touch hit the player 1 slider
                //if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    // Send ray to touch position and return whether it collided with something
                    RaycastHit2D colliderHit = Physics2D.Raycast(touchPos, Vector2.zero);
                    Debug.Log(colliderHit.collider.gameObject.tag);
                    if (colliderHit.collider != null && colliderHit.collider.gameObject.tag == "Player1")
                    {
                        sliderHit = true;
                        startTime = Time.time;
                        startPos = touchPos; // IMPORTANT: in world units
                        Swipe(touch);
                    }
                    else
                    {
                        touch = Input.GetTouch(1);
                        Swipe(touch);
                    }
                }
                
                
                /*foreach (Touch touch in Input.touches)
                {
                    //if (touch.phase == TouchPhase.Moved ||)
                    {
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        // Send ray to touch position and return whether it collided with something
                        RaycastHit2D colliderHit = Physics2D.Raycast(touchPos, Vector2.zero);

                        if (colliderHit.collider != null && colliderHit.collider.gameObject.tag == "Slider")
                        {
                            colliderHit.collider.gameObject.transform.position = new Vector3(touchPos.x, touchPos.y, -2);
                        }
                    }
                }*/
                 //   Swipe(touch);
            }

        }
    }
}