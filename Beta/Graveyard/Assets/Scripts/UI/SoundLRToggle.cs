using UnityEngine;
using System.Collections;

public class SoundLRToggle : LRToggle {

	protected override void handleLeft ()
	{
		GlobalValues.soundOff = !GlobalValues.soundOff;
	}
	protected override void handleRight ()
	{
		GlobalValues.soundOff = !GlobalValues.soundOff;
	}

}
