using UnityEngine;
using System.Collections;

public class DemoVideo : MonoBehaviour 
{
	[SerializeField] private float timeToWait;
	[SerializeField] private MovieTexture demoVid;

	private float passedTime;
	private bool startedVideo;

	void Start () 
	{
		passedTime = 0;
		startedVideo = false;
	}

	void OnGUI()
	{
		//Debug.Log ("Is playing: " + demoVid.isPlaying);
		if (demoVid.isPlaying)
		{
			Rect fullScreen = new Rect(0,0, Screen.width, Screen.height);
			GUI.DrawTexture(fullScreen, demoVid, ScaleMode.StretchToFill);
		}
	}

	void Update () 
	{
		if (startedVideo)
		{
			Debug.Log ("Vidisplaying: " + demoVid.isPlaying);

			if (!demoVid.isPlaying)
			{
				Debug.Log("Finished!!!");
				demoVid.Stop();
				passedTime = 0;
				startedVideo = false;
			}
		}
		else
		{
			passedTime += Time.deltaTime;
			if (passedTime >= timeToWait)
			{
				if (!demoVid.isPlaying)
				{
					StartCoroutine (ReadyMovie ());
					startedVideo = true;
				}
			}
		}
		if(Input.anyKeyDown)
		{
			demoVid.Stop();
			passedTime = 0;
			startedVideo = false;
		}
	}

	IEnumerator ReadyMovie()
	{
		while(!demoVid.isReadyToPlay)
		{
			yield return new WaitForSeconds(0.5f);
		}
		demoVid.Play ();
	}
}
