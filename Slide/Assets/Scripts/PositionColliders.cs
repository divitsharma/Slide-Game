using UnityEngine;
using System.Collections;

public class PositionColliders : MonoBehaviour {


    public Transform rightCollider;
    public Transform leftCollider;
    public Transform bottomCollider;
    public Transform topCollider;
    // Use this for initialization
	void Awake () {

        rightCollider.position = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        leftCollider.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        topCollider.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        bottomCollider.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
    }
}
