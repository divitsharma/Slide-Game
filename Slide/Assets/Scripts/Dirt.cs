using UnityEngine;
using System.Collections;

public class Dirt : MonoBehaviour {

    public Move movescript;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Move>().friction = 0.93f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Move>().friction = 0.99f;
    }
}
