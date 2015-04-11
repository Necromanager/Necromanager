using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{
	[SerializeField] List<UIElement> gameUI;
	[SerializeField] List<UIElement> storeUI;
	[SerializeField] List<UIElement> buildUI;


	public void ToggleGameUI(bool activate)
	{
		ToggleUIElements(gameUI,activate);
	}
	
	public void ToggleStoreUI(bool activate)
	{
		ToggleUIElements(storeUI,activate);
	}
	
	public void ToggleBuildUI(bool activate)
	{
		ToggleUIElements(buildUI,activate);
	}
	
	private void ToggleUIElements(List<UIElement> elements, bool activate)
	{
		foreach (UIElement temp in elements)
		{
			temp.LoadElements();
			temp.SetDraw(activate);
		}
	}
}
