using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
	public GameObject followObject;
	public float time = 1;
	public bool keepOriginalOffset = true;

	Vector3 originalOffset;
	// Use this for initialization
	void Start () {
//		yPos = gameObject.transform.position.y;
		originalOffset = followObject.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(followObject!=null)
		{
			Vector3 postition = Vector3.Lerp(gameObject.transform.position,followObject.transform.position - originalOffset,time);
//			postition.y = yPos;
			gameObject.transform.position = postition;
		}
	}
}
