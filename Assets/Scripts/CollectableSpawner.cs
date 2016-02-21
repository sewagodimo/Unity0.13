using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CollectableSpawner : MonoBehaviour 
{
	public GameObject CollectablePrefab;
    public Text ScoreText;
    public float SpawnRadius = 13;
	public int NumberOfCollectables = 12;
    public static int NumCollectables = 0;
    public static int NumCollectablesFound = 0;
    // Use this for initialization
    void OnEnable ()
    {

        for (int i = 0; i<NumberOfCollectables; i++)
		{
			Vector3 spawnPosition = transform.position;
			spawnPosition += new Vector3 (Mathf.Sin((Mathf.PI*2 /NumberOfCollectables)*i ),0, Mathf.Cos((Mathf.PI*2 /NumberOfCollectables)*i)) * SpawnRadius;
            Instantiate(CollectablePrefab, spawnPosition, transform.rotation);
            NumCollectables++;

		}

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
