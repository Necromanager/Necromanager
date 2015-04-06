using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour {
	
	Text text;
	
	void Awake()
	{
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		string myText;

		if (GlobalValues.musicOff)
		{
			myText = "Off";
		}
		else
		{
			myText = "On";
		}

		text.text = myText;
	}
}
