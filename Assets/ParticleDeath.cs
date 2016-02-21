using UnityEngine;
using System.Collections;

public class ParticleDeath : MonoBehaviour
{
    private ParticleSystem particleSystem;
	// Use this for initialization
	void Start ()
    {
        particleSystem = GetComponent<ParticleSystem>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (particleSystem.isStopped)
	    {
	        Destroy(gameObject);
	    }
	}
}
