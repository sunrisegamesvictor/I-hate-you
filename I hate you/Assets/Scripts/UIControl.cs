using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour {

	public static UIControl instance;
	public UISlider angleProgressBar;
	public UILabel gameAngleLabel;
	// Use this for initialization
	public float goAxis = 0;

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateAngleLabel(int num)
	{
		gameAngleLabel.text = num.ToString ();
	}

	public void onUpdateAngleProgress(float value)
	{
		angleProgressBar.value = value;
	}

	public void onGoDown()
	{
		goAxis = 1;
	}

	public void onGoUp()
	{
		goAxis = 0;
	}

	public void onBackDown()
	{
		goAxis = -1;
	}

	public void onBackUp()
	{
		goAxis = 0;
	}
}
