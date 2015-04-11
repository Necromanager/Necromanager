using UnityEngine;
using System.Collections;

public class BackOption : MenuOption 
{
	public BackOption()
	{
	}
	
	public override void Activate()
	{
		//Application.LoadLevel("RandomTest");
	}
	
	public override string GetText()
	{
		return "Back";
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
		return new MainMenu();
	}
}
