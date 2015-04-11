using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour {

	Text text;
	
	void Awake()
	{
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		string myText;
		if (GlobalValues.soundOff)
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
