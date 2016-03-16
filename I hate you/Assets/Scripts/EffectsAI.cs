using UnityEngine;
using System.Collections;

public class EffectsAI : MonoBehaviour {

	public static EffectsAI instanse;

	public GameObject bloodPrefab;
	private ArrayList bloodArray = new ArrayList();
	// Use this for initialization

	void Awake()
	{
		instanse = this;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playBloodEffect(Vector3 pos)
	{
		GameObject freeBlood = null;
		foreach (GameObject bloods in bloodArray)
		{
			if (!bloods.activeInHierarchy) 
			{
				freeBlood = bloods;
			}
		}

		if (freeBlood != null)
		{
			freeBlood.transform.position = pos;
			freeBlood.SetActive (true);
		}
		else 
		{
			GameObject blood = (GameObject)Instantiate (bloodPrefab, pos, Quaternion.identity);
			bloodArray.Add (blood);
		}
	}

}
