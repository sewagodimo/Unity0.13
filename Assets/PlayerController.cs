using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public GameObject collectableParticlePrefab;

	private Rigidbody rigidBody;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidBody.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Bashed into " + other.gameObject.tag);
		if (other.gameObject.tag == "Collectable") 
		{
			Instantiate (collectableParticlePrefab, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		}
	}
}