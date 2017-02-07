using UnityEngine;
using System.Collections;

public class RookBrooge : MonoBehaviour {

    public ParticleSystem splashParticles;

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this, 20);
        GetComponentInParent<Animator>().Play("rookbrooge break");
        /*foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
        {
            if (collider.isTrigger)
            {
                collider.enabled = false;
            }
        }*/

		Transform splashParticleInstance = Instantiate(splashParticles, gameObject.transform.position, gameObject.transform.rotation).transform;
        Destroy(splashParticleInstance.gameObject, 1.5f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
