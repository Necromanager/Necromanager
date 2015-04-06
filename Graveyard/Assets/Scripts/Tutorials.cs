using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorials : MonoBehaviour 
{
	[SerializeField] private int fontSize = 15;
	//[SerializeField] private Color fontColor = Color.white;
	//[SerializeField] private Color backgroundColor = Color.black;
	[SerializeField] private bool activated = false;

	[SerializeField] private GUIStyle myStyle;
	private GameMode gameMode;
	private Vector2 drawPos;
	private bool initialized = false;
	
	private List<GameMode> shownModes = new List<GameMode>();
	

	void Start () 
	{
		Init();
		//shownModes.Add(GameMode.STORE);
	}
	
	void Update ()
	{
		if(InputMethod.getButtonDown("Interact"))
		{
			activated = false;
		}
	}
	
	private void Init()
	{
		if (!initialized)
		{
			gameMode = GameMode.GAME;
			shownModes = new List<GameMode>();
			drawPos = new Vector2(Screen.width/2,Screen.height/2);
			initialized = true;
		}
	}
	
	void OnGUI()
	{
		if (!activated)
		{
			return;
		}
		
		SetupFont();
		
		/*GUISkin buttonSkin = new GUISkin();
		buttonSkin.button.normal.background = Resources.Load("Textures/Blank") as Texture2D;
		GUI.skin = buttonSkin;*/
		GUI.depth = -99;
		
		string text = GetText();
		Vector2 textSize = myStyle.CalcSize(new GUIContent(text));
		Rect displayRect = new Rect(drawPos.x-(textSize.x/2),drawPos.y-(textSize.y/2),
		                            textSize.x,textSize.y);
		
		if (GUI.Button(displayRect,text,myStyle))
		{
			activated = false;
		}
	}
	
	public void ShowTutorial(GameMode newGameMode)
	{
		ShowTutorial(newGameMode,0,0);
	}
	
	public void ShowTutorial(GameMode newGameMode,float xOffset, float yOffset)
	{

		if (!GlobalValues.tutorials)
		{
			return;
		}
		
		if (shownModes.Contains(newGameMode))
		{
			return;
		}
		
		Init();
		drawPos = new Vector2(Screen.width/2+xOffset,Screen.height/2+yOffset);
		shownModes.Add(newGameMode);
		gameMode = newGameMode;
		activated = true;
	}
	
	private void SetupFont()
	{
		//myStyle = new GUIStyle(GUI.skin.button);
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
	}
	
	private string GetText()
	{
		switch(gameMode)
		{
		case GameMode.BUILD:
			return "Place buildings and traps to slow\n"+
				   "down the zombie horde. Make sure\n"+
			       " to save some money for tomorrow!\n"+
			       "<Click to close>";
		case GameMode.GAME:
			return "Prevent the zombies from escaping until\n"+
				   "day-break! Stun 'em with your shovel and\n"+
			       "put 'em back in their graves! Every zombie\n"+
			       "escaped costs you money. Go bankrupt and\n"+
			       "it's game over!\n"+
			       "<Click to close>";
		case GameMode.STORE:
			return "Buy items to help you battle the\n"+
			       "horde. Don't spend it all in one\n"+
			       "place or you're done for!\n"+
			       "<Click to close>";
		default:
			return "";
		}
	}
}
