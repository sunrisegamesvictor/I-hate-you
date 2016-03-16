using UnityEngine;
using System.Collections;

public class buttons : MonoBehaviour {

	public static int levelN = 0;

	public void pressNextLevel()
	{
		levelN++;
		if(levelN > 2)
		{
			levelN = 0;
		}
		Application.LoadLevel(levelN);
	}

	public void pressExit()
	{
		Application.Quit();
	}
}
