using UnityEngine;
using System.Collections;

public class MainMenu : Menu
{
	public MainMenu()
	{
		Init();
	}

	protected override void DrawExtras()
	{
		Texture titleTex = Resources.Load("Textures/Title") as Texture;
		Rect drawRect = new Rect(Screen.width/2-titleTex.width/2,
		                         Screen.height/5-titleTex.height/2,
								 titleTex.width,
		                         titleTex.height);
							
		GUI.DrawTexture(drawRect,titleTex);
	}
	
	protected override void AddOptions()
	{
		options.Add(new NewGameOption());
		options.Add(new OptionsOption());
		options.Add(new QuitOption());
	}
}
