using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SizePanel : MonoBehaviour {

	Text text;
	
	void Awake()
	{
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = GlobalValues.GetSizeString ();
	}
}
