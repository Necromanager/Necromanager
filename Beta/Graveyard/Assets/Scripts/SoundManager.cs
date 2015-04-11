using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	//[SerializeField] bool replaceMusic = true;
	[SerializeField] List<AudioClip> backgroundTracks;
	[SerializeField] AudioClip storeMusic;
	[SerializeField] AudioClip buildMusic;

	AudioSource soundEffects;
	AudioSource messageEffects;
	AudioSource bkgMusic;
	bool created = false;
	int prevBackgroundIndex;

	void Awake () 
	{
		Init ();
	}
	
	void Update () 
	{
	
	}
	
	private void Init()
	{
		if (!created)
		{
			soundEffects = gameObject.AddComponent<AudioSource>();
			bkgMusic = gameObject.AddComponent<AudioSource>();
			messageEffects = gameObject.AddComponent<AudioSource>();
			created = true;
			prevBackgroundIndex = Random.Range(0, backgroundTracks.Count);
		}
	}
	
	public void PlayGameModeMusic(GameMode newMode)
	{
		Init ();
		switch(newMode)
		{
		case GameMode.GAME:
			PlayBackgroundMusic();
			break;
		case GameMode.BUILD:
			PlayBackgroundMusic(buildMusic);
			break;
		case GameMode.STORE:
			PlayBackgroundMusic(storeMusic);
			break;
		}
	}
	
	public void PlaySound(AudioClip sound)
	{
		if (GlobalValues.soundOff)
		{
			return;
		}
		
		soundEffects.PlayOneShot(sound);
	}
	
	public void PlaySound(string soundPath)
	{
		AudioClip sound = Resources.Load (soundPath) as AudioClip;
		PlaySound(sound);
	}
	
	public void PlayMessageSound(AudioClip sound)
	{
		if (GlobalValues.soundOff)
		{
			return;
		}
	
		if (messageEffects.isPlaying)
		{
			if (sound == messageEffects.clip)
			{
				return;
			}
			else
			{
				messageEffects.Stop();
			}
		}
		
		messageEffects.loop = false;
		messageEffects.clip = sound;
		messageEffects.Play();
	}
	
	public void PlayBackgroundMusic()
	{
		if ((backgroundTracks.Count < 1) || GlobalValues.musicOff)
		{
			return;
		}
		
		Init();
		
		int backgroundIndex = Random.Range(0,backgroundTracks.Count);
		while (backgroundIndex == prevBackgroundIndex)
		{
			backgroundIndex = Random.Range(0,backgroundTracks.Count);
		}
		prevBackgroundIndex = backgroundIndex;
		
		bkgMusic.loop = true;
		bkgMusic.clip = backgroundTracks[backgroundIndex];
		bkgMusic.Play();
	}
	
	public void PlayBackgroundMusic(AudioClip newBkgMusic)
	{
		Init ();
	
		if (GlobalValues.musicOff)
		{
			return;
		}
	
		bkgMusic.loop = true;
		bkgMusic.clip = newBkgMusic;
		bkgMusic.Play();
	}

	public void StopBackgroundMusic()
	{
		bkgMusic.Stop();	
	}
}
