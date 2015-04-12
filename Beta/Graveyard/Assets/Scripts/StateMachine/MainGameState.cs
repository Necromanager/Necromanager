using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGameState : GameState
{
	//protected const float RETURN_MONEY = 3.50f;
	//protected const float ESCAPE_PENALTY = 5.00f;
	//private const float KILL_PENALTY = 3.00f;
	protected const float SPAWN_DELAY = 0.75f;
	protected const int FONT_SIZE = 45;
	protected const int RESULTS_SIZE = 35;
	protected const int CLOCK_SIZE = 50;
	protected const float Y_DRAW_POS = 250;
	//private const float minuteTime = 0.25f;

	//private bool timeStarted;
	protected bool changedMusic;
	//private bool activatedZombies;
	protected float waveTime;
	protected int zombiesToSpawn;
	bool lostGame = false;
	//private float curMinuteTime;
	//private Color startColor;
	//private Color endColor;
	
	//private Texture clockTex;
	//private Texture bigHandTex;
	//private Texture smallHandTex;
	
	protected ClockUI clock;
	protected Newspaper newspaper;

	protected List<string> paymentNames;
	protected List<float> paymentValues;
	protected List<int> paymentNums;
	protected List<string> penaltyNames;
	protected List<float> penaltyValues;
	protected List<int> penaltyNums;



	protected NewMenu gameMenu;
	protected StoreMenu storeMenu;


	public MainGameState()
	{
		GameObject uiCanvas = GameObject.FindGameObjectWithTag("UIManager");

		clock = uiCanvas.GetComponentInChildren<ClockUI>();
		newspaper = GameObject.FindGameObjectWithTag ("NewspaperUI").GetComponent<Newspaper> ();
		Init ();
	}
	
	public override void Init()
	{
		//Debug.Log ("Main game init");
		GlobalValues.ResetTime();
		GlobalValues.morning = false;
		GlobalValues.inGame = false;
		GlobalValues.ResetZombieNums();
		
		changedMusic = false;
		//activatedZombies = false;
		
		LoadUIManager();
		/*
		uiManager.ToggleGameUI(true);
		uiManager.ToggleBuildUI(false);
		uiManager.ToggleStoreUI(false);
		*/

		//uiManager.startMenuTransition (GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameMenu>());

		if(firstTime)
		{
			firstTime = false;
		}
		else
		{
			gameMenu = GameObject.FindGameObjectWithTag ("GameUI").GetComponent<NewMenu> ();
			storeMenu = GameObject.FindGameObjectWithTag ("StoreUI").GetComponent<StoreMenu> ();
			gameMenu.setNext (storeMenu);
			gameMenu.isOpen = true;
		}

		SetupResultLists();
		clock.StartTime();
		StartWave();
		
		GlobalValues.inGame = true;
		
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
		foreach(GameObject floor in floors)
		{
			floor.GetComponent<Tile>().DevalueBuilding();
		}
		//Debug.Log("Done with init");
	}
	
	private void StartWave()
	{
		float diffScale = 1.1f - GlobalValues.difficulty;
		waveTime = GlobalValues.GetWaveTime() * diffScale;
		zombiesToSpawn = GlobalValues.GetNumZombies();
		
		//Debug.Log("Wave time: "+waveTime+" Number: "+zombiesToSpawn);
	}
	
	private void SpawnZombies()
	{
		List<int> usedGraves = new List<int>();
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");
		
		if ((graves.Length < 1) || (graves.Length < zombiesToSpawn))
		{
			return;
		}
		
		int index = 0;
		bool newIndex;
		Grave grave;
		for (int i=0; i<zombiesToSpawn; i++)
		{	
			newIndex = false;
			while (!newIndex)
			{
				index = Random.Range(0, graves.Length);
				newIndex = !usedGraves.Contains(index);
				if (newIndex)
				{
					usedGraves.Add(index);
				}
			}

			grave = graves[index].GetComponent<Grave>();
			if (grave.HasZombies())
			{
				grave.SpawnZombie(SPAWN_DELAY*i);
			}
		}
	}
	
	/*private void ActivateZombies()
	{
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");

		if (graves.Length < 1)
		{
			return;
		}
		
		foreach(GameObject grave in graves)
		{
			grave.GetComponent<Grave>().SetActive(true);
		}
		
		int index;
		for (int i=0; i<3; i++)
		{
			index = Random.Range(0,graves.Length);
			//graves[index].GetComponent<Grave>().SetSpawnTime(8,15);
		}
		
		GameObject[] mausoleums = GameObject.FindGameObjectsWithTag("Mausoleum");
		foreach (GameObject mausoleum in mausoleums)
		{
			mausoleum.GetComponent<Mausoleum>().Reset();
		}
		
		activatedZombies = true;
	}*/
	
	private void SetupResultLists()
	{
		paymentNames = new List<string>();
		paymentValues = new List<float>();
		paymentNums = new List<int>();
		penaltyNames = new List<string>();
		penaltyValues = new List<float>();
		penaltyNums = new List<int>();
	
		paymentNames.Add("Zombies returned");
		paymentValues.Add(GlobalValues.returnMoney);
		
		penaltyNames.Add("Zombies escaped");
		penaltyValues.Add(GlobalValues.escapeMoney);
		//penaltyNames.Add("Zombies killed");
		//penaltyValues.Add(KILL_PENALTY);
		
		for (int i=0; i<paymentNames.Count; i++)
		{
			paymentNums.Add(0);
		}
		for (int i=0; i<penaltyNames.Count; i++)
		{
			penaltyNums.Add(0);
		}
	}

	public override void UpdateState()
	{
		/*if (!activatedZombies)
		{
			ActivateZombies();
		}*/
		
		if (Input.GetKeyDown(KeyCode.F10))
		{
			GlobalValues.EndTime();
		}
		
		if (GlobalValues.IsTimeUp())
		{
			DestroyZombies();
			if (!changedMusic)
			{
				GlobalFunctions.StopBkgMusic();
				GlobalFunctions.PlaySoundEffect("Sounds/Effects/Events/endOfNight");
				//GlobalValues.AddMoney(CalculatePay());
				ToggleNewspaper(true);
				changedMusic = true;
			}
			GlobalValues.inGame = false;
			clock.StopTime();
		}
		else
		{
			waveTime -= Time.deltaTime;
			if (waveTime <= 0)
			{
				SpawnZombies();
				StartWave();
			}
		}
	}
	
	/*private void UpdateTime()
	{
		curMinuteTime += Time.deltaTime;
		
		if (curMinuteTime >= minuteTime)
		{
			curMinuteTime = 0;
			GlobalValues.PassTime(0,1);
		}
		
		RenderSettings.ambientLight = Color.Lerp(startColor,endColor,GlobalValues.GetTimePercentage());
	}*/
	
	private void DestroyZombies()
	{
		Grave graveScript;
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");
		foreach(GameObject grave in graves)
		{
			graveScript = grave.GetComponent<Grave>();
			//graveScript.SetActive(false);
			graveScript.Reset();
		}
		
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		foreach(GameObject zombie in zombies)
		{
			GameObject.DestroyImmediate(zombie);
		}
	}
	
	public override bool ShouldSwitchState()
	{
		if (GlobalValues.IsTimeUp() && InputMethod.getButtonDown("Continue") && !lostGame)
		{
			ToggleNewspaper(false);
			//GlobalValues.AddMoney(CalculatePay());
			
			//clock.SetDraw(false);

			if (GlobalValues.wonGame)
			{
				Debug.Log("Won game");
				Application.LoadLevel(0);
			}

			if (!GlobalValues.IsBankrupt())
			{
				Debug.Log ("IIIIIIIIIIIIIIIVE!");
				GlobalValues.day++;
				//GlobalValues.difficulty += 0.1f;
				GlobalValues.IncrementDifficulty();
				if (GlobalValues.difficulty > 1.0f)
				{
					GlobalValues.difficulty = 1.0f;
				}
				
				Reset();
			}
			else
			{
				Debug.Log("AAAARRRRRRRRGGHHHH!");
				//GAME OVER - Placeholder
				//uiManager.startMenuTransition ();
				NewMenu dead = GameObject.FindGameObjectWithTag("DeadUI").GetComponent<GameOverUI>();
				gameMenu.setNext(dead);

				lostGame = true;
				gameMenu.isOpen = false;
				dead.isOpen = true;
				return false;
			}
			gameMenu.isOpen = false;
			return true;
		}
		
		return false;
	}
	
	private float CalculatePay()
	{
		float totalPay = 0;
		
		paymentNums[0] = GlobalValues.zombiesReturned;
		penaltyNums[0] = GlobalValues.zombiesEscaped;
		//penaltyNums[1] = GlobalValues.zombiesKilled;
		
		for (int i=0; i<paymentNames.Count; i++)
		{
			totalPay+=(paymentValues[i]*paymentNums[i]);
		}
		for (int i=0; i<penaltyNames.Count; i++)
		{
			totalPay-=(penaltyValues[i]*penaltyNums[i]);
		}
		
		return totalPay;
	}
	
	public void Reset()
	{
		PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		player.GetInventory().Reset();
		
		GameObject[] gums = GameObject.FindGameObjectsWithTag("Gum");
		foreach(GameObject gum in gums)
		{
			GameObject.Destroy(gum);
		}
		
		GameObject[] gels = GameObject.FindGameObjectsWithTag("Gel");
		foreach(GameObject gel in gels)
		{
			GameObject.Destroy(gel);
		}

		GameObject[] boxes = GameObject.FindGameObjectsWithTag("ScreamBox");
		foreach(GameObject box in boxes)
		{
			GameObject.Destroy(box);
		}
		
		changedMusic = false;
	}
	
	public override GameMode GetGameMode()
	{
		return GameMode.GAME;
	}
	
	public override void Draw()
	{
		if (GlobalValues.IsTimeUp() && !lostGame)
		{
			//ShowResults();
		}
	}

	private void ToggleNewspaper(bool activate)
	{
		CanvasGroup cg = newspaper.GetComponent<CanvasGroup> ();

		if (activate)
		{
			cg.interactable = true;
			cg.blocksRaycasts = true;

			float pay = CalculatePay();
			if (pay < 0)
			{
				newspaper.headline.text = "Zombies Escape! Town Doomed!";
				newspaper.picture.sprite = Resources.Load<Sprite>("Sprites/UI/NewspaperCharacterSad");
				newspaper.storyPay.text = "Necromanager fined $"+Mathf.Abs(pay).ToString("F2");
				newspaper.endNight(false);
			}
			else
			{
				newspaper.headline.text = "Zombies Contained! Town Rejoices!";
				newspaper.picture.sprite = Resources.Load<Sprite>("Sprites/UI/NewspaperCharacterHappy");
				newspaper.storyPay.text = "Necromanager earns $"+pay.ToString("F2");
				newspaper.endNight(true);
			}
		}
		else
		{
			newspaper.toStore();
			cg.interactable = false;
			cg.blocksRaycasts = false;
		}
	}
	
	/*private void DrawClock()
	{
		SetupFont(CLOCK_SIZE);
		string timeString = GlobalValues.GetTimeString()+"\n"+GlobalValues.GetDayString();
		Vector2 textSize = myStyle.CalcSize(new GUIContent(timeString));
		GUI.skin.label.fontSize = CLOCK_SIZE;
		GUI.color = Color.white;
		GUI.Label(new Rect(clockTex.width,0,textSize.x,textSize.y*1.5f),timeString);
		
		float rotateAngle;
		GUI.DrawTexture(new Rect(0,0,clockTex.width,clockTex.height),clockTex);
		
		//Rotates GUI textures
		
		//Big hand
		GUI.color = Color.gray;
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		rotateAngle = (GlobalValues.minute/60f)*360f;
		GUIUtility.RotateAroundPivot(rotateAngle,new Vector2(clockTex.width/2,clockTex.height/2));
		GUI.DrawTexture(new Rect(0,0,bigHandTex.width,bigHandTex.height),bigHandTex);
		GUI.EndGroup();
		
		//Return rotation to 0
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUIUtility.RotateAroundPivot(-rotateAngle,new Vector2(clockTex.width/2,clockTex.height/2));
		GUI.EndGroup();
		
		//Small hand
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		rotateAngle = (GlobalValues.hour/12f)*360f;
		GUIUtility.RotateAroundPivot(rotateAngle,new Vector2(clockTex.width/2,clockTex.height/2));
		GUI.DrawTexture(new Rect(0,0,smallHandTex.width,smallHandTex.height),smallHandTex);
		GUI.EndGroup();
		
		//Return rotation to 0
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUIUtility.RotateAroundPivot(-rotateAngle,new Vector2(clockTex.width/2,clockTex.height/2));
		GUI.EndGroup();
	}*/
	
	/*private void ShowResults()
	{
		SetupFont(RESULTS_SIZE);
		float middleX = Screen.width/2;
		float spacing = RESULTS_SIZE*2;
		float startY = Screen.height/4;
		string text;
		Vector2 textSize;
		
		Texture backTex = Resources.Load("Textures/Blank") as Texture;
		GUI.color = new Color(0,0,0,0.75f);
		GUI.DrawTexture(GetResultsBoxSize(RESULTS_SIZE,spacing,middleX,startY),backTex);
		
		int numRows = paymentValues.Count + penaltyValues.Count;
		
		GUI.skin.label.fontSize = RESULTS_SIZE;
		
		for (int i=0; i<numRows; i++)
		{
			if (paymentValues.Count > i)
			{
				GUI.color = Color.green;
				text = paymentNames[i]+": "+GlobalValues.GetMoneyString(paymentValues[i])+" x "+paymentNums[i].ToString();
				textSize = myStyle.CalcSize(new GUIContent(text));
				GUI.Label(new Rect(middleX-(textSize.x/2),startY+(spacing*i),textSize.x,textSize.y),text);
			}
			else
			{
				int index = i-paymentValues.Count;
				GUI.color = Color.red;
				text = penaltyNames[index]+": -"+GlobalValues.GetMoneyString(penaltyValues[index])+" x "+penaltyNums[index].ToString();
				textSize = myStyle.CalcSize(new GUIContent(text));
				GUI.Label(new Rect(middleX-(textSize.x/2),startY+(spacing*i),textSize.x,textSize.y),text);
			}
		}
		
		GUI.color = Color.white;
		text = "Total pay: "+GlobalValues.GetMoneyString(CalculatePay());
		/*if ((GlobalValues.money + CalculatePay()) <= 0.0f)
		{
			text+="\nGAME OVER";
		}
		textSize = myStyle.CalcSize(new GUIContent(text));
		GUI.Label(new Rect(middleX-(textSize.x/2),startY+(spacing*(numRows+1)),textSize.x,textSize.y*1.5f),text);
		
		text = "Press [ENTER] to continue";
		textSize = myStyle.CalcSize(new GUIContent(text));
		GUI.Label (new Rect(middleX-(textSize.x/2),startY+(spacing*(numRows+2)),textSize.x,textSize.y*1.5f),text);
	
		
	}*/
	
	/*private Rect GetResultsBoxSize(int fontSize,float spacing,float middleX,float startY)
	{
		float boxWidth = 0;
		float boxHeight = 0;
		int numRows = paymentValues.Count + penaltyValues.Count;
		string text;
		Vector2 textSize;
		
		for (int i=0; i<numRows; i++)
		{
			if (paymentValues.Count > i)
			{
				text = paymentNames[i]+": "+GlobalValues.GetMoneyString(paymentValues[i])+" x "+paymentNums[i].ToString();
				textSize = myStyle.CalcSize(new GUIContent(text));
			}
			else
			{
				int index = i-paymentValues.Count;
				text = penaltyNames[index]+": -"+GlobalValues.GetMoneyString(penaltyValues[index])+" x "+penaltyNums[index].ToString();
				textSize = myStyle.CalcSize(new GUIContent(text));
			}
			
			if (textSize.x > boxWidth)
			{
				boxWidth = textSize.x;
			}
			
			boxHeight += textSize.y+spacing;
		}
		
		text = "Total pay: "+GlobalValues.GetMoneyString(CalculatePay());
		textSize = myStyle.CalcSize(new GUIContent(text));
		boxHeight += textSize.y+spacing;
		
		text = "Press [ENTER] to continue";
		textSize = myStyle.CalcSize(new GUIContent(text));
		boxHeight += textSize.y;
		
		return new Rect(middleX-(boxWidth/2),startY,boxWidth,boxHeight);
	}*/
	
	public override string GetControls()
	{
		string text = "Someone dun goofed";

		switch(InputMethod.getInputCode())
		{
		case InputModeCode.KEYBOARD_AND_MOUSE:
				text = "[F1] - Hide Controls\n"+
			          "[WASD] - Move\n"+
				      "[LMouse] - Pickup/Putdown\n"+
				      "[RMouse] - Use Item\n"+
				      "[Q][E] - Change Item";
			break;

		case InputModeCode.CONTROLLER:
			text = "[Back] - Hide Controls\n"+
					"[Left Stick] - Move\n"+
					"[A] - Pickup/Putdown\n"+
					"[X] - Use Item\n"+
					"[LB][RB] - Change Item";
			break;
		}
		return text;
	}
}
