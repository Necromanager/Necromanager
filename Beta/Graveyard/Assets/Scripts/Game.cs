using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{	
	private const string DATA_PATH = "savedata.txt";

	[SerializeField] public Color nightColor;
	[SerializeField] public Color dayColor;
	[SerializeField] public Color storeColor;
	[SerializeField] private Vector3 playerStartPosition;
	[SerializeField] private PlayerScript player;
	[SerializeField] private Selector selector;
	[SerializeField] private GameMode startMode;
	//[SerializeField] private int clockSize;
	[SerializeField] private int moneySize;
	[SerializeField] private int resultsSize;
	[SerializeField] private int controlsSize;
	[SerializeField] private int startPromptSize;
	
	private GUIStyle myStyle;
	private GameStateMachine stateMachine;
	
	private bool showControls;


	public bool isTutorial = false;

	void Awake ()
	{

	}

	void Start ()
	{
		Init(true);

	}
	
	private void Init(bool newGame)
	{
		if (newGame)
		{
			//GlobalValues.money = START_MONEY;
			//GlobalValues.difficulty = START_DIFFICULTY;
			Results.reset();
			if(GlobalValues.debugMode)
				GlobalValues.money = 400000;
			else
				GlobalValues.money = 0;

			GlobalValues.day = 1;
			GlobalValues.curDiffIncrease = 0;
			GlobalValues.wonGame = false;
			GlobalValues.startDifficulty = GlobalValues.difficulty;
		}
		else
		{
			LoadData();
		}
		
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		}
		if (selector == null)
		{
			selector = GameObject.FindGameObjectWithTag("Selector").GetComponent<Selector>();
		}
		
		stateMachine = new GameStateMachine(this);
		stateMachine.LoadState(startMode);
		
		GlobalValues.morning = false;
		showControls = true;
		GetComponent<LevelGeneration>().GenerateLevel();

		player.setGSM (stateMachine);
	}
	
	private void LoadData()
	{
		try
		{
			StreamReader sr = new StreamReader(DATA_PATH);
			GlobalValues.money = float.Parse(sr.ReadLine());
			GlobalValues.difficulty = float.Parse(sr.ReadLine());
			sr.Close();
		}
		catch
		{
		}
	}
	
	private void SaveData()
	{
		StreamWriter sw = new StreamWriter(DATA_PATH);
		sw.WriteLine(GlobalValues.money);
		sw.WriteLine(GlobalValues.difficulty);
		sw.Close();
	}
	
	private void SaveData(float newMoney,float newDifficulty)
	{
		StreamWriter sw = new StreamWriter(DATA_PATH);
		sw.WriteLine(newMoney);
		sw.WriteLine(newDifficulty);
		sw.Close();
	}








	void Update ()
	{
		if (InputMethod.getButtonDown("Quit")
		    && stateMachine.GetGameMode() == GameMode.GAME
		    && !GlobalValues.IsTimeUp())
		{
			//SaveData(START_MONEY,START_DIFFICULTY);
			//Application.LoadLevel("MainMenu");
			GlobalValues.TogglePause();
		}
		
		if (InputMethod.getButtonDown("Controls"))
		{
			showControls = !showControls;
		}
	
		stateMachine.UpdateState();

        var time = Time.time;
        Shader.SetGlobalFloat("_Time", time);


	}












	void OnGUI()
	{
		stateMachine.Draw();
		//DrawMoney();
		//ShowControls();
	}
	
	public void SwitchGameMode(GameMode newMode)
	{
		switch (newMode)
		{
		case GameMode.BUILD:
			EnablePlayerObjects(false,true,false);
			RenderSettings.ambientLight = storeColor;
			break;
		case GameMode.GAME:
			EnablePlayerObjects(true,false,false);
			RenderSettings.ambientLight = nightColor;
			break;
		case GameMode.STORE:
			EnablePlayerObjects(false,false,true);
			RenderSettings.ambientLight = storeColor;
			break;
		}
		
		//GetComponent<Tutorials>().ShowTutorial(newMode,0,0);
		GetComponent<SoundManager>().PlayGameModeMusic(newMode);
	}
	
	private void EnablePlayerObjects(bool enablePlayer, bool enableSelector, bool enableStore)
	{
		PlayerCamera pCam = Camera.main.GetComponent<PlayerCamera>();
		SelectorCamera sCam = Camera.main.GetComponent<SelectorCamera>();
		StoreCamera storeCam = Camera.main.GetComponent<StoreCamera>();
	
		if (enablePlayer)
		{
			player.enabled = true;
			player.Reset();
			player.SetVisible(true);
			pCam.Activate();
		}
		else
		{
			player.enabled = false;
			player.SetVisible(false);
			pCam.Deactivate();
		}
		
		if (enableSelector)
		{
			selector.enabled = true;
			selector.GetComponent<Renderer>().enabled = true;
			sCam.Activate();
		}
		else
		{
			selector.enabled = false;
			selector.GetComponent<Renderer>().enabled = false;
			sCam.Deactivate();
		}
		
		if (enableStore)
		{
			storeCam.Activate();
		}
		else
		{
			storeCam.Deactivate();
		}
	}
	
	/*private void ShowControls()
	{
		SetupFont(controlsSize);
		string text = "";
		float startY = 75;
		GUI.color = Color.white;
		GUI.skin.label.fontSize = controlsSize;
		
		if (showControls)
		{
			text = stateMachine.GetControls();
		}
		else
		{
			switch(InputMethod.getInputCode())
			{
			case InputModeCode.KEYBOARD_AND_MOUSE:
				text = "[F1] - Controls";
				break;

			case InputModeCode.CONTROLLER:
				text = "[Back] - Controls";
				break;
			}

		}
		
		Vector2 textSize = myStyle.CalcSize(new GUIContent(text));

		GUI.Label(new Rect(Screen.width-textSize.x,
		                   startY,
		                   textSize.x,
		                   textSize.y*1.5f),
		          text);
	}*/
	
	private void SetupFont(int fontSize)
	{
		myStyle = new GUIStyle();
		myStyle.fontSize = fontSize;
	}
	
	private void DrawMoney()
	{
		SetupFont(moneySize);
		string moneyText = GlobalValues.GetMoneyString();
		Vector2 textSize = myStyle.CalcSize(new GUIContent(moneyText));
		GUI.skin.label.fontSize = myStyle.fontSize;
		GUI.color = Color.white;
		
		GUI.Label(new Rect(Screen.width-textSize.x,0,textSize.x,textSize.y*1.5f),moneyText);
	}

	public GameStateMachine getGSM()
	{
		return stateMachine;
	}
}
