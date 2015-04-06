using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour 
{
	void Start () 
	{
		PopUpFactory.CreateMessage ("You win!", 40, Color.green, 99999, SoundEffectLibrary.winNight);
		GlobalValues.wonGame = true;
	}

	void Update () 
	{
		GlobalValues.wonGame = true;
	}
}
