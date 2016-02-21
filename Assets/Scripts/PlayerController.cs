using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{

	public float Speed;
	public GameObject CollectableParticlePrefab;
    public Text ScoreText;

    private Rigidbody _rigidBody;

	void Start ()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		_rigidBody.AddForce (movement * Speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectable") 
		{

            Debug.Log("Bashed into " + other.gameObject.tag);

            Instantiate (CollectableParticlePrefab, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		    CollectableSpawner.NumCollectablesFound++;


		    if (CollectableSpawner.NumCollectablesFound >= CollectableSpawner.NumCollectables)
            {
                if (ScoreText != null)
                {
                    ScoreText.text = "You Win!";
                }
                else
                {
                    Debug.LogError("ScoreText is null!");
                }
                Invoke("ReloadScene", 5);
		    }
		    else
            {
                if (ScoreText != null)
                {
                    ScoreText.text = "Collectables: " + CollectableSpawner.NumCollectablesFound + "/" +
                                     CollectableSpawner.NumCollectables;

                }
                else
                {
                    Debug.LogError("ScoreText is null!");
                }
            }
        }
	}

    void ReloadScene()
    {
        SceneManager.LoadScene("RollABall");
    }
}