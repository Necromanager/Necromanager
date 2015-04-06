using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NewMenuManager : MonoBehaviour {

	public NewMenu current;
	public NewMenu next;

	[SerializeField]
	private AudioClip menuMusic;

	[SerializeField]
	EventSystem es;

	virtual protected void Start()
	{
		next = current;
		showNextMenu ();
		if(menuMusic != null)
		{
			GlobalFunctions.PlayBkgMusic(menuMusic);
		}
	}

	virtual protected void OnEnable()
	{
		MenuEvents.nextMenuEvent += showNextMenu;
		MenuEvents.nextMenuEvent2 += showNextMenu;
		MenuEvents.menuTransEvent += startMenuTransition;
	}

	virtual protected void OnDisable()
	{
		MenuEvents.nextMenuEvent -= showNextMenu;
		MenuEvents.nextMenuEvent2 -= showNextMenu;
		MenuEvents.menuTransEvent -= startMenuTransition;
	}

	virtual protected void Update()
	{

	}

	public void startMenuTransition(NewMenu menu)
	{
		Debug.Log ("Starting Menu Transition (given)");
		next = menu;
		// you are going from one menu to another
		if(current != null)
		{
			current.onHide();
		}
		else // there is no previously active menu
		{
			showNextMenu();
		}
	}

	//called when old menu is finished with out transition
	public void showNextMenu()
	{
		current = next;
		next = null;

		if(current != null)
		{
			current.onShow();
			current.isOpen = true;
		}

		es.SetSelectedGameObject (null);

		if(InputMethod.getInputCode() == InputModeCode.CONTROLLER && current != null)
		{
			current.setSelected (es);
		}
	}

	public void showNextMenu(NewMenu menu)
	{
		current = menu;
		next = null;
		
		if(current != null)
		{
			current.onShow();
			current.isOpen = true;
		}
		
		es.SetSelectedGameObject (null);
		
		if(InputMethod.getInputCode() == InputModeCode.CONTROLLER && current != null)
		{
			current.setSelected (es);
		}
	}
	
	public void toggleMusic()
	{
		GlobalValues.musicOff = !GlobalValues.musicOff;
		if(GlobalValues.musicOff)
			GlobalFunctions.StopBkgMusic ();
		else
			GlobalFunctions.PlayBkgMusic (menuMusic);
	}
	
	public void setSelected(Selectable target)
	{
		if(target == null || InputMethod.getInputCode() != InputModeCode.CONTROLLER)
		{
			es.SetSelectedGameObject (null);
		}
		else
		{
			es.SetSelectedGameObject (target.gameObject);
		}
	}

}