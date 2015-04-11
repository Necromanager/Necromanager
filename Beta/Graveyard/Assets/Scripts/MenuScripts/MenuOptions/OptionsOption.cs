using UnityEngine;
using System.Collections;

public class OptionsOption : MenuOption 
{
	public OptionsOption()
	{
	}
	
	public override void Activate()
	{
	}
	
	public override string GetText()
	{
		return "Options";
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
		return new OptionsMenu();
	}
}
