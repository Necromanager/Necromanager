using UnityEngine;
using System.Collections;

public class StoreState : GameState
{
	protected const int FONT_SIZE = 45;
	protected const float Y_DRAW_POS = 500;

	StoreMenu storeMenu;
	NewMenu buildMenu;

	public StoreState()
	{
		Init ();
	}

	public override void Init()
	{
		LoadUIManager();
		if(firstTime)
		{
			firstTime = false;
		}
		else
		{
			buildMenu = GameObject.FindGameObjectWithTag ("BuildUI").GetComponent<NewMenu> ();
			storeMenu = GameObject.FindGameObjectWithTag ("StoreUI").GetComponent<StoreMenu> ();
			storeMenu.setNext (buildMenu);
			storeMenu.isOpen = true;
		}
	}
	
	public override void UpdateState()
	{
	}
	
	public override bool ShouldSwitchState()
	{
		if(InputMethod.getButtonDown("Continue"))
			storeMenu.isOpen = false;
		return InputMethod.getButtonDown("Continue");
	}
	
	public override GameMode GetGameMode()
	{
		return GameMode.STORE;
	}
	
	public override void Draw()
	{
		GameObject.FindGameObjectWithTag("Main").GetComponent<Store>().Draw(Screen.height/4);
		//ShowStartPrompt(FONT_SIZE,Y_DRAW_POS);
		//ShowPrompt(FONT_SIZE,"Press [ENTER] to finish");
	}
	
	public override string GetControls()
	{
		string text = "[F1] - Hide Controls\n"+
			          "[LMouse] - Buy\n"+
				      "[Enter] - Continue";
		return text;
	}
}
