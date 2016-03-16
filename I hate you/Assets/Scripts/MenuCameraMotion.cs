using UnityEngine;
using System.Collections;

public class MenuCameraMotion : MonoBehaviour {

	public static MenuCameraMotion instance;

	public Vector3 mainMenuPos;
	public Vector3 mainMenuRot;

	public Vector3 carShopPos;
	public Vector3 carShopRot;

	public Vector3 fobiesPos;
	public Vector3 fobiesRot;

	public float [] fogDensity;

	// Use this for initialization
	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		moveToMainMenu ();
	}

	public void moveToMainMenu()
	{
		LeanTween.move (gameObject, mainMenuPos, 1);
		LeanTween.rotate (gameObject, mainMenuRot, 1);

		LeanTween.value (gameObject, RenderSettings.fogDensity, fogDensity[0], 1).setOnUpdate ((float val) => 
		{
				RenderSettings.fogDensity = val;
		});
	}

	public void moveToFobies()
	{
		LeanTween.move (gameObject, fobiesPos, 1);
		LeanTween.rotate (gameObject, fobiesRot, 1);

		LeanTween.value (gameObject, RenderSettings.fogDensity, fogDensity[2], 1).setOnUpdate ((float val) => 
			{
				RenderSettings.fogDensity = val;
			});
	}

	public void moveToCarShop()
	{
		LeanTween.move (gameObject, carShopPos, 1);
		LeanTween.rotate (gameObject, carShopRot, 1);

		LeanTween.value (gameObject, RenderSettings.fogDensity, fogDensity[1], 1).setOnUpdate ((float val) => 
			{
				RenderSettings.fogDensity = val;
			});
	}

}
