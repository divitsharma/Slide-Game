using UnityEngine;
using System.Collections;

public class ObstacleLinearMovement : MonoBehaviour {

    public float forceX, forceY;

	// Use this for initialization
	void Update () {
        if (transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x 
            || transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
            forceX = -forceX;
        transform.position += new Vector3(forceX, forceY, 0) * Time.deltaTime;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Gem" && collision.collider.tag != "Slider")
            Debug.Log("HITTT");   
        forceX = -forceX;
    }

}
