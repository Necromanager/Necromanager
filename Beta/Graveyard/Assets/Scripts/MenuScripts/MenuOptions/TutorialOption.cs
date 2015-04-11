using UnityEngine;
using System.Collections;

public class TutorialOption : MenuOption
{
	public TutorialOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.ToggleTutorials();
	}
	
	public override string GetText()
	{
		string tutString = "";
		if (!GlobalValues.tutorials)
		{
			tutString += "Off";
		}
		else
		{
			tutString += "On";
		}
		
		return "Tutorials: "+tutString;
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
