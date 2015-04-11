using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NewMainMenuManager : NewMenuManager
{
	[SerializeField] NewMenu mainMenuElements;

	public void showElements()
	{
		mainMenuElements.isOpen = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		MenuEvents.showMainBGEvent += showElements;
	}
	
	protected override void OnDisable()
	{
		base.OnDisable();
		MenuEvents.showMainBGEvent -= showElements;
	}
}