using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour {

	Text text;

	void Awake()
	{
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (GlobalValues.tutorials)
		{
			text.text = "On";
		}
		else
		{
			text.text = "Off";
		}


	}
}
