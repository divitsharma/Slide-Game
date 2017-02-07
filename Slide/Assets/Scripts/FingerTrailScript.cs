using UnityEngine;
using System.Collections;

public class FingerTrailScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        trail.sortingLayerName = "FingerTrail";
        trail.sortingOrder = 4;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
