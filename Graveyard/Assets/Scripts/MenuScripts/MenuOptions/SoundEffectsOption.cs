using UnityEngine;
using System.Collections;

public class SoundEffectsOption : MenuOption
{
	public SoundEffectsOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.soundOff = !GlobalValues.soundOff;
	}
	
	public override string GetText()
	{
		string soundString = "On";
		
		if (GlobalValues.soundOff)
		{
			soundString = "Off";
		}
		
		return "Sound Effects: "+soundString;
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
