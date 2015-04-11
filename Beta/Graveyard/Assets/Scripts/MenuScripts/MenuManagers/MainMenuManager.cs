using UnityEngine;
using System.Collections;

public class MainMenuManager : MenuManager
{
	[SerializeField] private AudioClip mainMenuMusic;

	private const float START_MONEY = 200;
	private const float START_DIFFICULTY = 0.1f;
	private const int START_SIZE = 2;

	protected override void Init()
	{
		GlobalFunctions.PlayBkgMusic(mainMenuMusic);
		GlobalValues.money = START_MONEY;
		GlobalValues.difficulty = START_DIFFICULTY;
		GlobalValues.size = START_SIZE;
	}
	
	protected override Menu GetInitialMenu()
	{
		return new MainMenu();
	}
	
	protected override void UpdateManager()
	{
	}
}
