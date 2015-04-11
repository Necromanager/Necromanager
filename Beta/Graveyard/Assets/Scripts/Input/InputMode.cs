using UnityEngine;
using System.Collections;

public abstract class InputMode
{
	public virtual void onMenuLoad(){}
	public virtual void onGameLoad(){}
	public virtual void onBuildLoad(){}
	public virtual void onPause(){}

	public virtual void onEngage(){}

	protected void showMouse()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	protected void hideMouse()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}
}
