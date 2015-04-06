using UnityEngine;
using System.Collections;

public abstract class GameState 
{
	protected int nextState;
	protected int prevState;
	protected GUIStyle myStyle;
	protected NewMenuManager uiManager = null;

	protected bool firstTime = true;

	public void SetLinkedStates(int nextStateIndex, int prevStateIndex)
	{
		nextState = nextStateIndex;
		prevState = prevStateIndex;
	}
	
	public int GetNextStateIndex()
	{
		return nextState;
	}
	
	public int GetPrevStateIndex()
	{
		return prevState;
	}
	
	protected void LoadUIManager()
	{
		if (uiManager == null)
		{
			GameObject temp = GameObject.FindGameObjectWithTag("UIManager");
			uiManager = temp.GetComponent<NewMenuManager>();
		}
	}
	
	protected void SetupFont(int fontSize)
	{
		myStyle = new GUIStyle();
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
	}
	
	protected void ShowPrompt(int fontSize, string prompt)
	{
		SetupFont(fontSize);
		
		Texture backTex = Resources.Load("Textures/Blank") as Texture;
		
		string text = prompt;
		Vector2 textSize = myStyle.CalcSize(new GUIContent(text));
		
		float xPos = (Screen.width/2)-(textSize.x/2);
		float yPos = textSize.y*1.15f;
		
		GUI.color = new Color(0,0,0,0.8f);
		GUI.DrawTexture(new Rect(xPos,yPos,textSize.x,textSize.y*1.15f),backTex);
		
		GUI.color = Color.white;
		GUI.Label(new Rect(xPos,yPos,textSize.x,textSize.y*1.15f),text);
	}

	public abstract void Init();
	public abstract void UpdateState();
	public abstract bool ShouldSwitchState();
	public abstract GameMode GetGameMode();
	public abstract void Draw();
	public abstract string GetControls();
}
