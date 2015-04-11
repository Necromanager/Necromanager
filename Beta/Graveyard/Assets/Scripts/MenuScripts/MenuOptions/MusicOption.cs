using UnityEngine;
using System.Collections;

public class MusicOption : MenuOption
{
	public MusicOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.musicOff = !GlobalValues.musicOff;
	}
	
	public override string GetText()
	{
		string musicString = "On";
		
		if (GlobalValues.musicOff)
		{
			musicString = "Off";
		}
		
		return "Music: "+musicString;
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
