using UnityEngine;
using System.Collections;

public class InputModeController : InputMode
{
	public InputModeController()
	{
		onEngage ();
	}

	public override void onMenuLoad()
	{
		hideMouse ();
	}

	public override void onGameLoad()
	{
		hideMouse ();
	}

	public override void onBuildLoad()
	{
		hideMouse ();
	}
	
	public override void onEngage()
	{
		InputMethod.setInputCode(InputModeCode.CONTROLLER);
		hideMouse ();
		Debug.Log("Controller Mode Engaged");
	}

	public override void onPause()
	{
		hideMouse ();
	}
}

