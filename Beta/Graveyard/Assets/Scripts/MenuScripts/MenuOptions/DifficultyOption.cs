using UnityEngine;
using System.Collections;

public class DifficultyOption : MenuOption  
{
	public DifficultyOption()
	{
	}

	public override void Activate()
	{
		GlobalValues.IncrementDifficulty();
		
		if (GlobalValues.difficulty > 1.0f)
		{
			GlobalValues.difficulty = 0.1f;
		}
	}
	
	public override string GetText()
	{
		//string diffString = (GlobalValues.difficulty*100.0f).ToString("F0")+"%";
		string diffString = GlobalValues.GetDifficultyString();
		return "Start difficulty: "+diffString;
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
