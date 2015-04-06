using UnityEngine;
using System.Collections;

public class QuitOption : MenuOption
{
	public QuitOption()
	{
	}
	
	public override void Activate()
	{
		Application.Quit();
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
