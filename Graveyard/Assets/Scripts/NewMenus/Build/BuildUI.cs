using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour {

	[SerializeField]
	Color selectedColor;
	[SerializeField]
	Color idleColor;
	[SerializeField]
	Text descText;

	int selected = 0;

	List<BuildUIElement> elements;

	// Use this for initialization
	void Start ()
	{
		elements = new List<BuildUIElement> ( GetComponentsInChildren<BuildUIElement>() );
		for(int i = 0; i < elements.Count; i++)
		{
			elements[i].setColor(idleColor);
		}

		if(elements[0] != null)
		{
			elements[0].setColor(selectedColor);
			descText.text = elements [0].updateDescription ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( InputMethod.getButtonDown("Prev Item") )
		{
			move(-1);
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.buildSwitchItem);
		}
		if( InputMethod.getButtonDown("Next Item") )
		{
			move(1);
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.buildSwitchItem);
		}
	}

	void move(int direction)
	{
		elements [selected].setColor (idleColor);

		selected += direction;
		if(selected >= elements.Count )
			selected = 0;
		if(selected < 0)
			selected = elements.Count - 1;

		elements [selected].setColor (selectedColor);
		elements [selected].updateSelector();
		descText.text = elements [selected].updateDescription ();

	}

}
