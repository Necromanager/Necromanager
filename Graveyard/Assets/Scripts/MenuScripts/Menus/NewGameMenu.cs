using UnityEngine;
using System.Collections;

public class NewGameMenu : Menu
{
	public NewGameMenu()
	{
		Init();
	}
	
	protected override void DrawExtras()
	{
	}
	
	protected override void AddOptions()
	{
		options.Add(new DifficultyOption());
		options.Add(new SizeOption());
		options.Add(new TutorialOption());
		options.Add(new StartOption());
		options.Add(new BackOption());
	}
}
