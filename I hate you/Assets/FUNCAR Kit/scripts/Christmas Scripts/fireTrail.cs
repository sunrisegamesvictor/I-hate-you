using UnityEngine;
using System.Collections;

public class fireTrail : MonoBehaviour {

	private ParticleSystem particles;
	
	// Use this for initialization
	void Start () 
	{
		particles = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		//make them update smoothly with the camera that updates on FixedUpdate//
		particles.Simulate(Time.deltaTime,true,false);
	}
}
