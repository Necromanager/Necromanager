using UnityEngine;
using System.Collections;

public class TutorialLRToggle : LRToggle {

	protected override void handleLeft ()
	{
		GlobalValues.ToggleTutorials();
	}
	protected override void handleRight ()
	{
		GlobalValues.ToggleTutorials();
	}
}
