using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public static GameManager instanse;

	public float angleAddByKill = 0;
	public float angleReduce = 0;
	public float angleAddReduce = 0;
	public float maxAngleRange = 1000;
	private float currentAngleRange = 0;
	private int currentGameAngleValue = 0;

	void Awake()
	{
		instanse = this;
	}

	void Start () 
	{
		onSetup ();
		StartCoroutine (onGameTick());
	}

	void onSetup()
	{
		currentAngleRange = maxAngleRange;
	}

	public IEnumerator onGameTick()
	{
		yield return new WaitForSeconds (1);
		angleReduce += angleAddReduce;
		StartCoroutine (onGameTick());
	}

	public void addAngleByKill()
	{
		currentGameAngleValue += (int)angleAddByKill;
		UIControl.instance.updateAngleLabel (currentGameAngleValue);

		currentAngleRange += angleAddByKill;
		if (currentAngleRange > maxAngleRange)
			currentAngleRange = maxAngleRange;
	}

	public void updateAngleProgress()
	{
		UIControl.instance.onUpdateAngleProgress (currentAngleRange/maxAngleRange);
	}

	// Update is called once per frame
	void Update () 
	{
		currentAngleRange -= angleReduce*Time.deltaTime;
		updateAngleProgress ();
	}
}
