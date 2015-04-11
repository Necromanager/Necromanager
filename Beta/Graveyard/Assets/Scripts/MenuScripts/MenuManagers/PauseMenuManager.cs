using UnityEngine;
using System.Collections;

public class PauseMenuManager : MenuManager
{
	protected override void Init()
	{
		canDraw = false;
	}
	
	protected override Menu GetInitialMenu()
	{
		return new PauseMenu();
	}
	
	protected override void UpdateManager()
	{
		canDraw = GlobalValues.paused;
	}
}
