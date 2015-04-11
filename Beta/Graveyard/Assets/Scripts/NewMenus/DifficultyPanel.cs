using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultyPanel : MonoBehaviour {


	Text text;
	
	void Awake()
	{
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = GlobalValues.GetDifficultyString ();
	}
}
