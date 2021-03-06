﻿using UnityEngine;
using System.Collections;

public class GameOverUI : NewMenu {

//	ResultsScreen results;

	public override void Awake()
	{
		base.Awake ();
		//results = GetComponentInChildren<ResultsScreen> ();
	}
	public void loadScene(int scene)
	{
		Application.LoadLevel (scene);
	}

	public override void onShow()
	{
		base.onShow ();
		GlobalFunctions.StopBkgMusic ();
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.deathScreen);
	}
	
	public void ResetGame()
	{
		GlobalValues.money = 0;
		GlobalValues.day = 1;
		GlobalValues.curDiffIncrease = 0;
		GlobalValues.wonGame = false;
		GlobalValues.difficulty = GlobalValues.startDifficulty;
	}
}
