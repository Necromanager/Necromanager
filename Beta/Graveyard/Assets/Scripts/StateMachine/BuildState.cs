using UnityEngine;
using System.Collections;

public class BuildState : GameState
{
	protected const int FONT_SIZE = 45;
	
	protected float Y_DRAW_POS = Screen.height/2;

	NewMenu buildMenu;
	NewMenu gameMenu;

	public BuildState()
	{
		Init ();
	}
	
	public override void Init()
	{
		LoadUIManager();
		/*
		uiManager.ToggleGameUI(false);
		uiManager.ToggleBuildUI(true);
		uiManager.ToggleStoreUI(false);
		*/

		if(firstTime)
		{
			firstTime = false;
		}
		else
		{
			buildMenu = GameObject.FindGameObjectWithTag ("BuildUI").GetComponent<NewMenu> ();
			gameMenu = GameObject.FindGameObjectWithTag ("GameUI").GetComponent<NewMenu> ();
			buildMenu.setNext (gameMenu);
			buildMenu.isOpen = true;
		}

		//uiManager.showMenu (GameObject.FindGameObjectWithTag("BuildUI").GetComponent<NewMenu>());
	}
	
	public override void UpdateState()
	{
	}
	
	public override bool ShouldSwitchState()
	{
		if(InputMethod.getButtonDown("Continue"))
			buildMenu.isOpen = false;
		return InputMethod.getButtonDown("Continue");
	}
	
	public override GameMode GetGameMode()
	{
		return GameMode.BUILD;
	}
	
	public override void Draw()
	{
		//ShowStartPrompt(FONT_SIZE,Y_DRAW_POS);
		//ShowPrompt(FONT_SIZE,"Press [ENTER] to finish");
	}
	
	public override string GetControls()
	{
		string text = "[F1] - Hide Controls\n"+
			          "[WASD] - Move\n"+
				      "[LMouse] - Place barricade\n"+
				      "[RMouse] - Remove barricade\n"+
				      "[Q][E] - Zoom in/out";
		return text;
	}
}
