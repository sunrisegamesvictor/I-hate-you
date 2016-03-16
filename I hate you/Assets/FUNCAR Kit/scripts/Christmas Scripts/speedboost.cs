using UnityEngine;
using System.Collections;

public class speedboost : MonoBehaviour 
{
	public GameObject fireTrailL;
	public GameObject fireTrailR;

	void OnTriggerEnter(Collider col)
	{
		//collide with car tire//
		if(col.name == "colLB" || col.name == "colLF" || col.name == "colRB" || col.name == "colRF")
		{
			//tell the wheel it hit a speedboost//
			col.GetComponent<wheel>().enterSpeedBoost(fireTrailL,fireTrailR);
		}
	}
}
