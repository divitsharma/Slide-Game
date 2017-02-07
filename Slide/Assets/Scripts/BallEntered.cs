using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallEntered : MonoBehaviour {

    public EdgeCollider2D insideCollider;
    Sprite holeSprite;

    // If the ball has entered
    public bool entered = false;

    Collider2D[] colliderArray = new Collider2D[1];

    // Properties of the hole
    float holeScale;
    float holeRadius;

    // Winning particles
    public ParticleSystem winningParticles;

    public ParticleSystem gravityField;

    public AudioClip winningSound;
    private AudioSource winningSoundSource;

	// Use this for initialization
	void Start () {
        holeSprite = GetComponent<SpriteRenderer>().sprite;

        winningSoundSource = GetComponent<AudioSource>();
        winningSoundSource.clip = winningSound;

        insideCollider.enabled = false;
        winningParticles.Stop();
        gravityField.Stop();

        holeScale = transform.localScale.x;
        holeRadius = holeSprite.bounds.extents.x * holeScale;
	}
	
	// Update is called once per frame
    void Update()
    {


        // Check if a collider overlaps circle defined by hole's position and radius multiplied by its scale factor- 1<<8 bit shift '1' 8 units
        if (!entered
            && Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x, transform.position.y), holeRadius + 0.25f, colliderArray, 1 << 8) > 0)
        {
            if (!colliderArray[0].gameObject.GetComponent<Move>().destroyed)
            {
                gravityField.Play();

                GameObject ballObject = colliderArray[0].gameObject;

                Sprite ballSprite = ballObject.GetComponent<SpriteRenderer>().sprite;

                // Only if x and y extents and scales are the same (squares or circles)
                float scale = ballObject.transform.localScale.x;
                float ballRadius = ballSprite.bounds.extents.x * scale;

                // BALL ENTERED
                // If the ball's left, top, right and bottom edges are withing the hole's
                if (ballObject.transform.position.x - ballRadius > transform.position.x - holeRadius
                    && ballObject.transform.position.y + ballRadius < transform.position.y + holeRadius
                    && ballObject.transform.position.x + ballRadius < transform.position.x + holeRadius
                    && ballObject.transform.position.y - ballRadius > transform.position.y - holeRadius)
                {
                    entered = true;

                    // Enable collider and reduce speed further
                    insideCollider.enabled = true;
                    ballObject.GetComponent<Rigidbody2D>().velocity *= 0.4f;

                    // Play particles
                    winningParticles.Play();

                    // Play sound
                    winningSoundSource.PlayOneShot(winningSound);

                    // Show menu
                    Manager.ManagerInst.clearedMenu.SetActive(true);
                    UnityEngine.UI.Image[] stars = Manager.ManagerInst.clearedMenu.GetComponentsInChildren<UnityEngine.UI.Image>();
                    stars[2].color = Color.yellow;
                    for (int i = 0; i < Manager.ManagerInst.gemsCollected; i++)
                    {
                        stars[i + 3].color = Color.yellow;
                    }

                    string thisScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString();

                    // Set key of this level to be true, meaning its beaten
                    PlayerPrefs.SetInt(thisScene, true ? 1 : 0);

                    // Update total gems collected, adding difference between how many collected this time and collected before
                    // and update new stars collected in playerprefs
                    // IF more collected this time or previous value isnt recorded
                    if (!PlayerPrefs.HasKey("Stars" + thisScene)
                        || Manager.ManagerInst.gemsCollected > PlayerPrefs.GetInt("Stars" + thisScene))
                    {

                        // Set total
                        PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + Manager.ManagerInst.gemsCollected - PlayerPrefs.GetInt("Stars" + thisScene));

                        // Set number of gems collected from this level alone, key is "Stars" + buildindex
                        PlayerPrefs.SetInt("Stars" + thisScene, Manager.ManagerInst.gemsCollected);
                    }
                }
                else //Make ball curve towards center
                {
                    ballObject.GetComponent<Rigidbody2D>().AddForce(-(ballObject.transform.position - transform.position).normalized * 2f);
                }
            }
            else
            {
                gravityField.Stop();
            }
        }
        else
        {
            gravityField.Stop();
        }
    }
}
