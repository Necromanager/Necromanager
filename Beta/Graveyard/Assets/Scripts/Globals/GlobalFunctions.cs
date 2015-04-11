using UnityEngine;
using System.Collections;

public class GlobalFunctions 
{
	private static SoundManager soundManager = null;

	private static void LoadSoundManager()
	{
		if (soundManager == null)
		{
			soundManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>();
		}
	}

	public static void StopBkgMusic()
	{
		//SoundManager soundManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>();
		LoadSoundManager();
		soundManager.StopBackgroundMusic();
	}
	
	public static void PlayBkgMusic(AudioClip bkgMusic)
	{
		LoadSoundManager();
		Debug.Log(soundManager == null);
		soundManager.PlayBackgroundMusic(bkgMusic);
	}

	public static void PlaySoundEffect(AudioClip sound)
	{
		//SoundManager soundManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>();
		LoadSoundManager();
		soundManager.PlaySound(sound);
	}
	
	public static void PlaySoundEffect(string soundPath)
	{
		//SoundManager soundManager = GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>();
		LoadSoundManager();
		soundManager.PlaySound(soundPath);
	}

	public static Vector3 NormalizeVector3(Vector3 vec)
	{
		return new Vector3 (NormalizeFloat(vec.x), NormalizeFloat(vec.y), NormalizeFloat(vec.z));
	}

	public static float NormalizeFloat(float val)
	{
		if(val > 0)
		{
			val = 1;
		}
		else if(val < 0)
		{
			val = -1;
		}

		return val;
	}
}
