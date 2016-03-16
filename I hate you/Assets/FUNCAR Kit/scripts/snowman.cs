using UnityEngine;
using System.Collections;

public class snowman : MonoBehaviour {

	public Transform targetObj;
	public float seeDistance = 60f;
	public GameObject exploderObj;

	private Animator anim;
	private string behaviour = "hidden";
	private float n;
	private bool isVisible;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//detect if visible by any camera (including editor view)//
		if(gameObject.GetComponentInChildren<Renderer>().isVisible)
		{
			isVisible = true;
		}
		else
		{
			isVisible = false;
		}

		//get distance from target//
		n = Vector3.Distance(transform.position,targetObj.position);

		faceTarget();

		//hidden behaviour//
		if(behaviour == "hidden")
		{
			//can be seen//
			if(isVisible)
			{
				//see target//
				if(n<=seeDistance)
				{
					behaviour = "idle";
					anim.SetBool("hidden",false);
				}
			}
		}
		//idle behaviour//
		else if(behaviour == "idle")
		{
			//hide when out of view so it can pop up again//
			if(!isVisible)
			{
				if(n>seeDistance)
				{
					behaviour = "hidden";
					anim.SetBool("hidden",true);
				}
			}
		}

	}

	void faceTarget()
	{
		//rotate towards target//
		Quaternion initRot = transform.rotation;
		transform.LookAt(targetObj.position);
		transform.rotation = Quaternion.Lerp(initRot,transform.rotation,5f*Time.deltaTime);
		transform.rotation = Quaternion.Euler(0f,transform.eulerAngles.y,0f);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(behaviour != "hidden")
		{
			//collide with car body//
			if(col.name == "colSuspension" || col.name == "colBody")
			{
				
				Vector3 _colPos = (col.transform.position + transform.position)/2;
				explode(_colPos);
			}
			//collide with car tire//
			if(col.name == "colLB" || col.name == "colLF" || col.name == "colRB" || col.name == "colRF")
			{
				Vector3 _colPos = (col.transform.position + transform.position)/2;
				explode(_colPos);
			}
		}
	}

	void explode(Vector3 colPos)
	{
		gameObject.SetActive(false);
		GameObject exploder = Instantiate(exploderObj,transform.position,transform.rotation) as GameObject;
		exploder.GetComponent<exploder>().explode(colPos);
		
		Invoke("reset",5f);
	}

	void reset()
	{
		gameObject.SetActive(true);
		behaviour = "hidden";
		anim.SetBool("hidden",true);
		anim.Play("snowman_hide");
	}
}







