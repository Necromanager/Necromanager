using UnityEngine;
using System.Collections;

public class MainMenuFunctions : MonoBehaviour {

	public void loadLevel(int level)
	{
		Application.LoadLevel(level);
	}

	public void loadFirstLevel()
	{
		int level = 1;
		if (GlobalValues.tutorials)
			level = 2;

		Application.LoadLevel(level);
	}
	
	public void exitGame()
	{
		Application.Quit ();
	}
	
	public void toggleTutorials()
	{
		GlobalValues.ToggleTutorials();
	}
	
	public void toggleDifficulty()
	{
		GlobalValues.IncrementMenuDifficulty(1);
		
		if (GlobalValues.difficulty > 1.0f)
		{
			GlobalValues.difficulty = 0.1f;
		}
		if (GlobalValues.difficulty < 0.1f)
		{
			GlobalValues.difficulty = 1.0f;
		}
	}
	
	public void toggleSize()
	{
		GlobalValues.IncrementSize(1);
	}

	public void toggleSound()
	{
		GlobalValues.soundOff = !GlobalValues.soundOff;
	}
}
