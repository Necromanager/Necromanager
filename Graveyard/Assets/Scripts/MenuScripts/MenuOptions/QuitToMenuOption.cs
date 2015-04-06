using UnityEngine;
using System.Collections;

public class QuitToMenuOption : MenuOption
{
	public QuitToMenuOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.TogglePause();
		Application.LoadLevel(0);
	}
	
	public override string GetText()
	{
		return "Quit";
	}
	
	public override bool IsAvailable()
	{
		return true;
	}
	
	public override bool IsTransition()
	{
		return false;
	}
	
	protected override Menu GetNewMenu()
	{
		return null;
	}
}
