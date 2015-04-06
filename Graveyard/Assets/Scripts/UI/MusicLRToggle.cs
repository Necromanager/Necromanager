using UnityEngine;
using System.Collections;

public class MusicLRToggle : LRToggle {

	NewMainMenuManager mainMenu;
	
	protected override void Start ()
	{
		base.Start ();
		mainMenu = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<NewMainMenuManager>();
	}
	
	protected override void handleLeft ()
	{
		mainMenu.toggleMusic();
	}
	protected override void handleRight ()
	{
		mainMenu.toggleMusic();
	}
}
