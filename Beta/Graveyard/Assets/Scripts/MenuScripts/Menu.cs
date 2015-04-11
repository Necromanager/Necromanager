using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Menu 
{
	protected List<MenuOption> options;
	protected GUIStyle myStyle;
	
	protected void Init()
	{
		options = new List<MenuOption>();
		AddOptions();
	}
	
	public void SetupFont(int fontSize,Color fontColor)
	{
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
		GUI.color = fontColor;
	}
	
	public void Draw(float buffer)
	{
		Vector2 drawPosition = new Vector2(Screen.width/2,Screen.height/3);
		Vector2 textSize;
	
		foreach (MenuOption option in options)
		{
			textSize = myStyle.CalcSize(new GUIContent(option.GetText()));
			drawPosition = new Vector2(drawPosition.x-textSize.x/2,drawPosition.y);
			
			option.Draw(drawPosition,myStyle);
			
			drawPosition = new Vector2(Screen.width/2,drawPosition.y+buffer+textSize.y*1.2f);
		}
		
		DrawExtras();
	}
	
	protected abstract void DrawExtras();
	protected abstract void AddOptions();
	
}
