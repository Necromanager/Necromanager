using UnityEngine;
using System.Collections;

public class ResumeOption : MenuOption
{
	public ResumeOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.TogglePause();
	}
	
	public override string GetText()
	{
		return "Resume";
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
