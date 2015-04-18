using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour 
{
	[SerializeField] private float spinSpeed;

	void Start () 
	{
		//PopUpFactory.CreateMessage ("You win!", 40, Color.green, 99999, SoundEffectLibrary.winNight);
		GlobalValues.wonGame = true;
	}

	void Update () 
	{
		transform.Rotate(new Vector3(0,spinSpeed * Time.deltaTime,0));
	
		GlobalValues.wonGame = true;
	}
}
