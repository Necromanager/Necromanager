using UnityEngine;
using System.Collections;

public class SizeOption : MenuOption  
{
	public SizeOption()
	{
	}
	
	public override void Activate()
	{
		GlobalValues.IncrementSize(1);
	}
	
	public override string GetText()
	{
		string sizeString = GlobalValues.GetSizeString();
		return "Start size: "+sizeString;
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
