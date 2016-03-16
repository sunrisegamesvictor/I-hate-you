using UnityEngine;
using System.Collections;

public class ObjectLifeTime : MonoBehaviour {

	// Use this for initialization
	public float lifeTime = 2;

	void Start () 
	{
		
	}

	void OnEnable()
	{
		Invoke ("onSetInactive",lifeTime);
	}

	void onSetInactive()
	{
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
