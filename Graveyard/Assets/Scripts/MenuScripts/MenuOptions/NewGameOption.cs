using UnityEngine;
using System.Collections;

public class NewGameOption : MenuOption
{
	public NewGameOption()
	{
	}
	
	public override void Activate()
	{
	}
	
	public override string GetText()
	{
		return "New Game";
	}
	
	public override bool IsAvailable()
	{
		return true;
	}
	
	public override bool IsTransition()
	{
		return true;
	}
	
	protected override Menu GetNewMenu()
	{
		return new NewGameMenu();
	}
}
