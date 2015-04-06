using UnityEngine;
using System.Collections;

public class InputModeMAK : InputMode {

	public InputModeMAK()
	{
		onEngage ();
	}

	public override void onMenuLoad()
	{
		showMouse ();
	}
	
	public override void onGameLoad()
	{
		hideMouse ();
	}
	
	public override void onBuildLoad()
	{
		showMouse ();
	}
	
	public override void onEngage()
	{
		InputMethod.setInputCode(InputModeCode.KEYBOARD_AND_MOUSE);
		showMouse ();
		//Debug.Log("Mouse and Keyboard Mode Engaged");
	}

	public override void onPause()
	{
		showMouse ();
	}
}