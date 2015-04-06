using UnityEngine;
using System.Collections;

public abstract class MenuOption 
{
	//protected Menu parentMenu;
	
	public void Draw(Vector2 position,GUIStyle style)
	{
		Vector2 textSize = style.CalcSize(new GUIContent(GetText()));
		Rect buttonPos = new Rect(position.x,position.y,textSize.x,textSize.y*1.2f);
		
		if (!IsAvailable())
		{
			GUI.color = Color.red;
		}
		
		if (GUI.Button(buttonPos,GetText(),style) && IsAvailable())
		{
			Activate();
			
			if (IsTransition())
			{
				SwapMenu();
			}
		}
	}
	
	protected void SwapMenu()
	{
		Menu newMenu = GetNewMenu();
		MenuManager menuManager = GameObject.FindGameObjectWithTag("Main").GetComponent<MenuManager>();
		
		menuManager.SwapMenu(newMenu);
	}
	
	public abstract void Activate();
	public abstract string GetText();
	public abstract bool IsAvailable();
	public abstract bool IsTransition();
	protected abstract Menu GetNewMenu();
}
