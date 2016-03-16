using UnityEngine;
using System.Collections;

public class MenuAI : MonoBehaviour {

	// Use this for initialization
	public GameObject [] mainUI;
	public GameObject [] shopUI;
	public GameObject [] fobiesUI;



	public static MenuAI instance;

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		showMainUI ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void onPlay()
	{
		Application.LoadLevel (" map2");
	}

	public void fromShopToMain()
	{
		showMainUI ();
		MenuCameraMotion.instance.moveToMainMenu ();
	}

	public void onFobies()
	{
		showFobiesUI ();
		MenuCameraMotion.instance.moveToFobies ();
	}

	public void onCarShop()
	{
		MenuCameraMotion.instance.moveToCarShop ();
		showShopUI ();
	}

	public void showMainUI()
	{
		foreach (GameObject go in mainUI)
		{
			go.SetActive (true);
		}
		foreach (GameObject go2 in shopUI)
		{
			go2.SetActive (false);
		}
		foreach (GameObject go3 in fobiesUI)
		{
			go3.SetActive (false);
		}
	}

	public void showShopUI()
	{
		foreach (GameObject go in mainUI)
		{
			go.SetActive (false);
		}
		foreach (GameObject go2 in shopUI)
		{
			go2.SetActive (true);
		}
		foreach (GameObject go3 in fobiesUI)
		{
			go3.SetActive (false);
		}
	}

	public void showFobiesUI()
	{
		foreach (GameObject go in mainUI)
		{
			go.SetActive (false);
		}
		foreach (GameObject go2 in shopUI)
		{
			go2.SetActive (false);
		}
		foreach (GameObject go3 in fobiesUI)
		{
			go3.SetActive (true);
		}
	}
}
