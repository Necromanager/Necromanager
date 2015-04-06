using UnityEngine;
using System.Collections;

public class PopUpFactory : MonoBehaviour 
{
	public static void CreateMessage(string message, int size, Color color, float duration, AudioClip sound)
	{
		RemoveCurrentMessage();
		
		GameObject temp = new GameObject();
		temp.tag = "PopUp";
		temp.AddComponent<PopUpMessage>();
		temp.GetComponent<PopUpMessage>().Init(message,size,color,duration,sound);
	}
	
	public static void CreateMessage(Texture image, Vector2 imageSize, float duration, AudioClip sound)
	{
		RemoveCurrentMessage();
		
		GameObject temp = new GameObject();
		temp.tag = "PopUp";
		temp.AddComponent<PopUpMessage>();
		temp.GetComponent<PopUpMessage>().Init(image,imageSize,duration,sound);
	}
	
	private static void RemoveCurrentMessage()
	{
		GameObject[] obs = GameObject.FindGameObjectsWithTag("PopUp");
		
		foreach (GameObject ob in obs)
		{
			DestroyImmediate(ob);
		}
	}
	
	public static void ZombieEscapeMessage()
	{
		CreateMessage("Zombie Escaped!", 50, Color.red, 5.0f, SoundEffectLibrary.zombieEscaped);
	}
	
	public static void NoTargetMessage()
	{
		CreateMessage("No Target", 50, Color.red, 0.5f, SoundEffectLibrary.error);
	}
}
