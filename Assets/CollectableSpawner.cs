using UnityEngine;
using System.Collections;

public class CollectableSpawner : MonoBehaviour 
{
	public GameObject CollectablePrefab;
	public float SpawnRadius;
	public int NumberOfCollectables;
	// Use this for initialization
	void OnEnable () 
	{		
		for(int i = 0; i<NumberOfCollectables; i++)
		{
			Vector3 spawnPosition = transform.position;
			spawnPosition += new Vector3 (Mathf.Sin((Mathf.PI*2 /NumberOfCollectables)*i ),0, Mathf.Cos((Mathf.PI*2 /NumberOfCollectables)*i)) * SpawnRadius;
			Instantiate(CollectablePrefab,spawnPosition,transform.rotation);
		}
	}
}
