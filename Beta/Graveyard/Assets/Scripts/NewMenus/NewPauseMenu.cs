using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems.EventSystem;

public class NewPauseMenu : MonoBehaviour
{

	bool shouldDraw = false;
	Canvas canvas;

	[SerializeField]
	List<GameObject> buttons;

	[SerializeField]
	EventSystem eventSystem;

	[SerializeField]
	StandaloneInputModule inputModule;

	void Awake()
	{
		eventSystem.SetSelectedGameObject(buttons[0]);
		canvas = GetComponent<Canvas>();
		shouldDraw = GlobalValues.paused;
		foreach(GameObject b in buttons)
		{
			b.SetActive(shouldDraw);
		}
		canvas.enabled = shouldDraw;
	}

	void Update()
	{
		if(shouldDraw != GlobalValues.paused)
		{
			shouldDraw = GlobalValues.paused;

			canvas.enabled = shouldDraw;
			foreach(GameObject b in buttons)
			{
				b.SetActive(shouldDraw);
			}

			eventSystem.SetSelectedGameObject(null);
			if(shouldDraw && InputMethod.getInputCode() == InputModeCode.CONTROLLER)
			{
				eventSystem.SetSelectedGameObject(buttons[0]);
			}
		}
	}

	public void handleResume()
	{
		Debug.Log ("Game Resumed");
		GlobalValues.TogglePause();
	}

	public void handleQuit()
	{
		Debug.Log ("Quit Game");
		GlobalValues.TogglePause();
		Application.LoadLevel(0);
	}
}
