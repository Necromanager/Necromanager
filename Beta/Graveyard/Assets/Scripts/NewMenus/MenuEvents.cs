using UnityEngine;
using System.Collections;

public static class MenuEvents
{
	public delegate void NextMenuEvent();
	public static event NextMenuEvent nextMenuEvent;

	public static void nextMenu()
	{
		if(!Object.ReferenceEquals(null, nextMenuEvent))
		{
			nextMenuEvent();
		}
	}

	public delegate void NextMenuEvent2(NewMenu menu);
	public static event NextMenuEvent2 nextMenuEvent2;
	
	public static void nextMenu(NewMenu menu)
	{
		if(!Object.ReferenceEquals(null, nextMenuEvent2))
		{
			nextMenuEvent2(menu);
		}
	}

	public delegate void MenuTransEvent(NewMenu nextMenu);
	public static event MenuTransEvent menuTransEvent;
	
	public static void menuTransition(NewMenu nextMenu)
	{
		if(!Object.ReferenceEquals(null, nextMenuEvent))
		{
			menuTransEvent(nextMenu);
		}
	}


	public delegate void ShowMainBGEvent();
	public static event ShowMainBGEvent showMainBGEvent;
	
	public static void showMainBG()
	{
		if(!Object.ReferenceEquals(null, showMainBGEvent))
		{
			showMainBGEvent();
		}
	}
}
