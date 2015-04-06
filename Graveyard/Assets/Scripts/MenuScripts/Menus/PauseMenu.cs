using UnityEngine;
using System.Collections;

public class PauseMenu : Menu
{
	public PauseMenu()
	{
		Init();
	}
	
	protected override void DrawExtras()
	{
	}
	
	protected override void AddOptions()
	{
		options.Add(new ResumeOption());
		options.Add(new QuitToMenuOption());
	}
}
