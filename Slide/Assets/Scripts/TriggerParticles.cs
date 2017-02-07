using UnityEngine;
using System.Collections;

public class TriggerParticles : MonoBehaviour {

    // particle
    public Transform collisionParticle;
    public Transform destroyedParticle;
    public Transform splashParticles;

    public AudioClip collisionSound;
    private AudioSource source;

    public AudioClip destroyedSound;
    //private AudioSource destroyedSource;

	// Use this for initialization
	void Awake () {
        //collisionParticle.GetComponent<ParticleSystem>().Stop();
        
        source = gameObject.GetComponent<AudioSource>();
        
        
        //destroyedSource = gameObject.AddComponent<AudioSource>();
       // destroyedSource.clip = destroyedSound;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collisionParticleInstance = Instantiate(collisionParticle, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collisionParticle.position.z), collision.collider.transform.rotation) as Transform;
        Destroy(collisionParticleInstance.gameObject, 1.5f);
        //collisionParticle.position = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collisionParticle.position.z);
        //collisionParticle.GetComponent<ParticleSystem>().Play();
        
        source.clip = collisionSound;
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
        source.pitch = 0.9f + (GetComponent<Rigidbody2D>().velocity.magnitude / 15f)*0.2f;
        source.PlayOneShot(collisionSound, GetComponent<Rigidbody2D>().velocity.magnitude / 15);

        Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //destroyedSource.PlayOneShot(destroyedSound, 1f);
        if (collider.tag == "Hazard")
        {
            Transform destroyedParticleInstance = Instantiate(destroyedParticle, gameObject.transform.position, gameObject.transform.rotation) as Transform;
            Destroy(destroyedParticleInstance.gameObject, 1.5f);
            //destroyedParticle.position = gameObject.transform.position;//new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collisionParticle.position.z);
            //destroyedParticle.GetComponent<ParticleSystem>().Play();

            source.clip = destroyedSound;
            source.PlayOneShot(destroyedSound);

            gameObject.GetComponent<Move>().Destroy();
        }
        if (collider.tag == "Bridge")
        {
            GetComponent<Move>().onBridge = true;
        }
        else if (collider.tag == "Water" && !GetComponent<Move>().onBridge)
        {
            Transform splashParticleInstance = Instantiate(splashParticles, gameObject.transform.position, gameObject.transform.rotation) as Transform;
            Destroy(splashParticleInstance.gameObject, 1.5f);

            gameObject.GetComponent<Move>().Destroy();
            
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Water" && !GetComponent<Move>().onBridge)
        {
            Transform splashParticleInstance = Instantiate(splashParticles, gameObject.transform.position, gameObject.transform.rotation) as Transform;
            Destroy(splashParticleInstance.gameObject, 1.5f);

            gameObject.GetComponent<Move>().Destroy();

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Bridge")
            GetComponent<Move>().onBridge = false;
    }
}
