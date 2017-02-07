using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

    public ParticleSystem destroyed;

    public AudioClip hitSound;
    private AudioSource hitSource;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Slider")
        {
            Manager.ManagerInst.gemsCollected += 1;


            destroyed.gameObject.transform.position = this.gameObject.transform.position;
            destroyed.Play();

            hitSource.pitch = Random.Range(0.9f, 1.1f);
            hitSource.PlayOneShot(hitSound);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    void Awake()
    {
        hitSource = GetComponent<AudioSource>();
        hitSource.clip = hitSound;
    }
}
