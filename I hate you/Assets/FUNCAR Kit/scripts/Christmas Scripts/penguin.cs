using UnityEngine;
using System.Collections;

public class penguin : MonoBehaviour {

	public Transform targetAvoid;
	public float seeDistance = 30f;
	[HideInInspector]
	public GameObject centerObj;

	public GameObject renderObject;

	public SkinnedMeshRenderer skin;

	private Rigidbody rbody;
	private Animator anim;
	private string behaviour = "idle";
	private float n;
	private float animSpeedGoal = 1f;
	private Vector3 homePos;
	private bool ragdollMode = false;
	private float waitN = 0f;

	// Use this for initialization
	void Start () 
	{
		rbody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		homePos = transform.position;
		animSpeedGoal = Random.Range(1f,1.5f);

		targetAvoid = GameObject.FindGameObjectWithTag ("Player").transform;
		if (renderObject == null)
			renderObject = transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (skin.isVisible);
		if(ragdollMode)
		{
			if(waitN > 0f)
			{
				waitN -= Time.deltaTime;
			}
			else
			{
				stopBeingRagdoll();
			}
		}
		else if (skin.isVisible)
		{
			//get distance from target//
			n = Vector3.Distance(transform.position,targetAvoid.position);

			if (n < 30) {
				//see monster//
				if(n<=seeDistance)
				{
					if(behaviour != "recover")
					{
						behaviour = "run";
						anim.SetBool("run",true);
						animSpeedGoal = 2f;
					}
				}

				//recover behaviour//
				if(behaviour == "recover")
				{
					faceTargetSlowly();

					if(waitN > 0f)
					{
						waitN -= Time.deltaTime;
					}
					else
					{
						behaviour = "idle";
						animSpeedGoal = 1f;
					}
				}

				//idle behaviour//
				if(behaviour == "idle")
				{
					faceTarget();
				}
				//run behaviour//
				else if(behaviour == "run")
				{
					//escape//
					if(n > seeDistance*1.5f)
					{
						behaviour = "calmDown";
						animSpeedGoal = 1f;
					}
					//run away from target//
					else
					{
						runAway();
					}
				}
				//calm down//
				else if(behaviour == "calmDown")
				{
					//get far away//
					if(n > seeDistance*3f)
					{
						//decide to either walk home, or stand around here//
						if(Random.Range(0,2)==0)
						{
							behaviour = "goHome";
						}
						else
						{
							behaviour = "idle";
							anim.SetBool("run",false);
						}
					}
					//run away at the slower pace//
					else
					{
						runAway();
					}
				}
				//goHome behaviour//
				else if(behaviour == "goHome")
				{
					//get distance from original starting position//
					n = Vector3.Distance(transform.position,homePos);
					if(n > 10f)
					{
						walkHome();
					}
					else
					{
						behaviour = "idle";
						anim.SetBool("run",false);
					}
				}

				//adjust animation speed//
				anim.speed = Mathf.Lerp(anim.speed,animSpeedGoal,0.5f*Time.deltaTime);
			}
		}
	}
	
	void runAway()
	{
		//rotate away//
		Quaternion initRot = transform.rotation;
		transform.LookAt(2 * transform.position - targetAvoid.position);
		transform.rotation = Quaternion.Lerp(initRot,transform.rotation,anim.speed*Time.deltaTime);
		transform.rotation = Quaternion.Euler(0f,transform.eulerAngles.y,0f);
		
		//move forwards//
		moveForward();
	}

	void walkHome()
	{
		//rotate towards home//
		Quaternion initRot = transform.rotation;
		transform.LookAt(homePos);
		transform.rotation = Quaternion.Lerp(initRot,transform.rotation,anim.speed*Time.deltaTime);
		transform.rotation = Quaternion.Euler(0f,transform.eulerAngles.y,0f);

		//move forwards//
		moveForward();
	}
	
	void moveForward()
	{
		Vector3 moveVel = transform.forward*(7f*anim.speed);
		rbody.AddForce(0f,-20f*Time.deltaTime,0f);
		rbody.velocity = new Vector3(moveVel.x,rbody.velocity.y,moveVel.z);
	}

	void faceTarget()
	{
		//rotate towards target//
		Quaternion initRot = transform.rotation;
		transform.LookAt(targetAvoid.position);
		transform.rotation = Quaternion.Lerp(initRot,transform.rotation,anim.speed*Time.deltaTime);
		transform.rotation = Quaternion.Euler(0f,transform.eulerAngles.y,0f);
	}

	void faceTargetSlowly()
	{
		//rotate towards target//
		Quaternion initRot = transform.rotation;
		transform.LookAt(targetAvoid.position);
		Quaternion goalRot = Quaternion.Euler(0f,transform.eulerAngles.y,0f);
		transform.rotation = Quaternion.Lerp(initRot,goalRot,anim.speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if(!ragdollMode)
		{
			Vector3 _colPos = (col.transform.position + transform.position)/2;

			//collide with car body//
			if(col.name == "colSuspension" || col.name == "colBody")
			{
				becomeRagdoll(_colPos);
			}
			//collide with car tire//
			else if(col.name == "colLB" || col.name == "colLF" || col.name == "colRB" || col.name == "colRF")
			{
				becomeRagdoll(_colPos);
			}
		}
	}

	void becomeRagdoll(Vector3 colPos)
	{
		ragdollMode = true;
		anim.enabled = false;

		//enable all colliders//
		foreach (Collider col in GetComponentsInChildren<Collider>()) 
		{
			col.enabled = true;
		}
		//enable rigidbodies//
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) 
		{
			rb.isKinematic = false;
			rb.AddExplosionForce(5f,colPos,7f);
		}
		//freeze rigidbody that is not part of the skeleton//
		rbody.isKinematic = true;
		//disable colliders that are not part of the skeleton//
		foreach (Collider col in GetComponents<Collider>()) 
		{
			col.enabled = false;
		}

		GameManager.instanse.addAngleByKill ();
		renderObject.SetActive (false);
		EffectsAI.instanse.playBloodEffect (transform.position);

		waitN = 10f;
	}

	void stopBeingRagdoll()
	{
		//disable all colliders//
		foreach (Collider col in GetComponentsInChildren<Collider>()) 
		{
			col.enabled = false;
		}
		//disable rigidbodies//
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) 
		{
			rb.isKinematic = true;
		}
		//unfreeze rigidbody that is not part of the skeleton//
		rbody.isKinematic = false;
		//enable colliders that are not part of the skeleton//
		foreach (Collider col in GetComponents<Collider>()) 
		{
			col.enabled = true;
		}

		//update parent position since it does not move with the ragdoll//
		transform.position = centerObj.transform.position;
		transform.rotation = centerObj.transform.rotation;

		anim.enabled = true;
		ragdollMode = false;

		behaviour = "recover";
		anim.SetBool("run",false);
		anim.speed = 0.5f;
		animSpeedGoal = 3f;

		renderObject.SetActive (true);

		waitN = 1.8f;
	}
}

















