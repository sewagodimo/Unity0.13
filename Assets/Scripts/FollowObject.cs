using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
	public GameObject followObject;
	public float Time = 1;
	public bool KeepOriginalOffset = true;

	Vector3 _originalOffset;
	// Use this for initialization
	void Start () {

		_originalOffset = followObject.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(followObject!=null)
		{
			Vector3 postition = Vector3.Lerp(gameObject.transform.position,followObject.transform.position - _originalOffset,Time);
			gameObject.transform.position = postition;
		}
	}
}
