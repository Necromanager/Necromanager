using UnityEngine;
using System.Collections;

public class StartOption : MenuOption
{
	public StartOption()
	{
	}

	public override void Activate()
	{
		Application.LoadLevel(1);
	}
	
	public override string GetText()
	{
		return "Start";
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
