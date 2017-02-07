using UnityEngine;
using System.Collections;

public class SpikeSwitch : MonoBehaviour
{

    public Animator[] spikes;
    public AudioClip switchsound;

    bool on;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (!on)
        {
            on = true;

            GetComponent<AudioSource>().clip = switchsound;
            GetComponent<AudioSource>().PlayOneShot(switchsound);

            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SwitchOn");

            foreach (Animator spike in spikes)
            {
                if (spike.transform.localScale.y * -1 == Mathf.Abs(spike.transform.localScale.y))
                    spike.Play("SpikeHideFlipped");
                else
                    spike.Play("SpikeHide");
                spike.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }
}
