using UnityEngine;
using System.Collections;

public class Pinkslip : MonoBehaviour {

	public AudioClip stinger;
	public bool hasTriggered = false;

	public void trigger()
	{
		GlobalFunctions.PlaySoundEffect (stinger);
		GetComponent<Animator>().SetTrigger("GoodNight");
		hasTriggered = true;
	}

	public void playThud()
	{
		GlobalFunctions.PlaySoundEffect ("Sounds/Effects/Thud");
	}
	
	public void playWhoosh()
	{
		GlobalFunctions.PlaySoundEffect ("Sounds/Effects/Miss");
	}
}
