using UnityEngine;
using System.Collections;

public class DifficultyLRToggle : LRToggle {

	protected override void handleLeft()
	{
		GlobalValues.IncrementMenuDifficulty(-1);
		if (GlobalValues.difficulty > 1.0f)
		{
			GlobalValues.difficulty = 0.1f;
		}
		if (GlobalValues.difficulty < 0.1f)
		{
			GlobalValues.difficulty = 1.0f;
		}
	}

	protected override void handleRight()
	{
		GlobalValues.IncrementMenuDifficulty(1);
		if (GlobalValues.difficulty > 1.0f)
		{
			GlobalValues.difficulty = 0.1f;
		}
		if (GlobalValues.difficulty < 0.1f)
		{
			GlobalValues.difficulty = 1.0f;
		}
	}
}
