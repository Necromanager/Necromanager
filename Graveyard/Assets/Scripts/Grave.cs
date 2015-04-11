using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grave : MonoBehaviour 
{
	private Shader textShader;

	private const int MIN_ZOMBIES = 1;
	private const int MAX_ZOMBIES = 3;
	private const int MAX_DEPTH = 5;
	private const int TEXT_SIZE = 15;
	private const float GRAVE_SPEED = 0.5f;
	private const float DUST_SPAWN_PERCENT = 0.9f;
	private const float MIN_CANDLE_DISTANCE = 0.75f;
	
	/*private const float MIN_SPAWN_TIME = 30;
	private const float MAX_SPAWN_TIME = 80;*/
	
	//private List<Vector3> gravePositions;
	private List<Vector3> candlePositions;
	private List<GameObject> candles;
	private List<Quaternion> graveRotations;
	private Object[] burySounds;

//	private Vector3 goalGravePos;
//	private Quaternion goalGraveRot;
	private Vector3 pos;
	private Tile myTile;
	//private bool activated;
	//private bool createdDust;
	private GameObject gravestoneOb;
	private GameObject textBox;
	private TextMesh textMeshPrefab;
	//private float spawnTime;
	//private float currentSpawnTime;
	private float spawnDelay;
	private int curZombies;
	private int maxZombies = 0;
	private int originalNum;
	private int depth;

	void Start () 
	{	
		//activated = false;
		Init();
		//burySounds = Resources.LoadAll("Sounds/Effects/Bury/");
		CreateGravePositions();
		//CreateCandlePositions();
		//candles = new List<GameObject> ();
		//CreateGravestoneText();
	}
	
	public void PossiblyDestroy(float percent)
	{
		if (Random.Range(0.0f,1.0f) <= percent)
		{
			DestroyImmediate(gameObject);
		}
	}
	
	private void Init()
	{
		pos = transform.position;
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z);
		Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
		foreach (Collider ob in below)
		{
			if ((ob.tag == "Floor"))
			{
				myTile = ob.GetComponent<Tile>();
				break;
			}
		}
		CreateGravestone();
		//maxZombies = Random.Range(MIN_ZOMBIES,MAX_ZOMBIES+1);
		/*maxZombies = 0;
		originalNum = maxZombies;
		depth = maxZombies;
		curZombies = maxZombies;*/
		//createdDust = false;
		
		//currentSpawnTime = 0;
		spawnDelay = 0;
		//spawnTime = Random.Range(MIN_SPAWN_TIME,MAX_SPAWN_TIME);
		//spawnTime = GetSpawnTime();
	}
	
	public Tile GetTile()
	{
		return myTile;
	}
	
	public void AddZombies(int numZombies)
	{
		maxZombies += numZombies;
		depth = maxZombies;
		curZombies = maxZombies;
		originalNum = maxZombies;
		//UpdateGravestoneText();
		UpdateCandles ();
	}
	
	public bool HasZombies()
	{
		return (curZombies > 0);
	}
	
	/*public void FillGrave(int capacity, int numZombies)
	{
		maxZombies = capacity;
		depth = capacity;
		curZombies = numZombies;
		
		UpdateGravestoneText();
	}*/
	
	private void CreateGravePositions()
	{
		//PLACEHOLDER
		//gravePositions = new List<Vector3>();
		graveRotations = new List<Quaternion>();
		
		//Vector3 gPos = gravestoneOb.transform.position;
		Quaternion rot = gravestoneOb.transform.rotation;
		
		//gravePositions.Add(new Vector3(gPos.x,-1.0f,gPos.z));
		//gravePositions.Add(new Vector3(gPos.x,-0.4f,gPos.z));
		//gravePositions.Add(new Vector3(gPos.x,0.15f,gPos.z));
		//gravePositions.Add(gPos);
		
		graveRotations.Add(rot);
		graveRotations.Add(rot);
		graveRotations.Add(rot);
		graveRotations.Add(rot);
		
//		goalGravePos = gravePositions[curZombies];
//		goalGraveRot = graveRotations[curZombies];
		
		/*Vector3 pos3 = new Vector3(pos.x,pos.y*.75f,pos.z);
		Vector3 pos2 = new Vector3(pos.x,pos.y*.5f,pos.z);
		Vector3 pos1 = new Vector3(pos.x,pos.y*.25f,pos.z);*/
		
	}

	private void CreateCandlePositions()
	{
		candlePositions = new List<Vector3> ();
		//float randX, randY, randZ;
		float yOffset = 0.2f;
		Vector3 pos = transform.position;
		//Vector3 candlePos = new Vector3(0,0,0);
		
		candlePositions.Add(new Vector3(pos.x-0.25f, pos.y+yOffset, pos.z+0.45f));
		candlePositions.Add(new Vector3(pos.x-0.25f, pos.y+yOffset, pos.z-0.4f));
		candlePositions.Add(new Vector3(pos.x+0.25f, pos.y+yOffset, pos.z+0.45f));
		candlePositions.Add(new Vector3(pos.x+0.25f, pos.y+yOffset, pos.z-0.4f));
		candlePositions.Add(new Vector3(pos.x, pos.y+yOffset, pos.z));
		
		for (int i=0; i<candlePositions.Count; i++)
		{
			Vector3 temp = candlePositions[i];
			int randomIndex = Random.Range(i, candlePositions.Count);
			candlePositions[i] = candlePositions[randomIndex];
			candlePositions[randomIndex] = temp;
		}
		/*bool foundSpace = false;

		for (int i=0; i< MAX_DEPTH; i++)
		{
			foundSpace = false;
			while (!foundSpace)
			{
				foundSpace = true;
				randX = Random.Range(-.25f, .25f);
				randY = 0.2f;
				randZ = Random.Range(-.45f, .4f);
				candlePos = new Vector3(pos.x+randX, pos.y+randY, pos.z+randZ);

				for (int j=0; j<candles.Count; j++)
				{
					if (Vector3.Distance(candlePos, candles[j].transform.position) < MIN_CANDLE_DISTANCE)
					{
						foundSpace = false;
						break;
					}
				}
			}

			candlePositions.Add(candlePos);
		}*/
	}

	private void UpdateCandles()
	{
		if (candles == null) 
		{
			candles = new List<GameObject>();
		}
		if (candlePositions == null) 
		{
			CreateCandlePositions();
		}

		if (depth < candles.Count) 
		{
			while (candles.Count > depth)
			{
				GameObject.Destroy(candles[candles.Count-1]);
				candles.RemoveAt(candles.Count-1);
			}
		}
		else if (candles.Count < depth)
		{
			GameObject candle;
			for (int i=candles.Count; i<depth; i++) 
			{
				candle = GameObject.Instantiate (Resources.Load ("Prefabs/Candle_Placeholder")) as GameObject;
				candle.transform.position = candlePositions[i];
				candles.Add(candle);
			}
		}
		

		for (int i=0; i<candles.Count; i++)
		{
			Candle candle = candles[i].GetComponent<Candle>();
			candle.Activate(i < curZombies);
		}
	}
	
	public void Reset()
	{
		//curZombies = maxZombies;
		curZombies = originalNum;
		//currentSpawnTime = 0;
		//createdDust = false;
		//spawnTime = Random.Range(MIN_SPAWN_TIME,MAX_SPAWN_TIME);
		//spawnTime = GetSpawnTime();
		
		//UpdateGravestoneText();
		UpdateCandles ();
	}
	
	void Update () 
	{
		if (spawnDelay > 0)
		{
			spawnDelay -= Time.deltaTime;
			if (spawnDelay <= 0)
			{
				SpawnZombie();
			}
		}
	}

	void FixedUpdate()
	{
		UpdateGraveStone();
		//UpdateGravestoneText();
	}
	
	private void UpdateGraveStone()
	{
		/*int index = curZombies-1;
		if (index < 0)
		{
			index = 0;
		}*/
		/*
		int index = curZombies;
		if(Mathf.Approximately(Vector3.Distance(gravestoneOb.transform.position, gravePositions[index]), 0))
		{

		gravestoneOb.transform.position = Vector3.Lerp(gravestoneOb.transform.position,
		                                               gravePositions[index],GRAVE_SPEED);
		gravestoneOb.transform.rotation = Quaternion.Lerp(gravestoneOb.transform.rotation,
		                                               graveRotations[index],GRAVE_SPEED);
		}
		*/
	}
	
	private float GetSpawnTime()
	{
		//float diffScale = 1.1f-GlobalValues.difficulty;
		
		//float minTime = GlobalValues.minZombieSpawn * diffScale;
		//float maxTime = GlobalValues.maxZombieSpawn * diffScale;
		
		return GlobalValues.GetWaveTime();
	}
	
	/*public void SetSpawnTime(float min, float max)
	{
		spawnTime = Random.Range(min,max);
		currentSpawnTime = 0;
	}*/
	
	private void CreateGravestone()
	{
		Object[] gravestones = Resources.LoadAll("FinalAssets/Gravestones");
		string path = "FinalAssets/Gravestones/Gravestone";
		path += Random.Range(1,gravestones.Length+1);
		
		gravestoneOb = GameObject.Instantiate(Resources.Load(path)) as GameObject;
		Vector3 gravestonePos = new Vector3(pos.x,0.5f,pos.z+1f);
		
		gravestoneOb.transform.position = gravestonePos;
	}
	
	public void CreateDust()
	{
		GameObject dust = GameObject.Instantiate(Resources.Load("Prefabs/ParticleEffects/GraveDust")) as GameObject;
		Vector3 dustSpawnPos = new Vector3(pos.x,1.5f,pos.z-0.5f);
		//Debug.Log ("Spawned dust");
		
		dust.transform.position = dustSpawnPos;
	}

	public void SpawnZombie(float delay)
	{
		if (delay <= 0)
		{
			SpawnZombie();
			return;
		}

		spawnDelay = delay;
	}

	public void SpawnZombie()
	{
		Results.zombiesSpawned++;
		GameObject newZombie = GameObject.Instantiate(Resources.Load("Zombies/BasicZombie")) as GameObject;
		//GameObject newZombie = GameObject.Instantiate(Resources.Load("Zombies/Necromancer")) as GameObject;
		Vector3 zombieSpawnPos = new Vector3(pos.x,1.5f,pos.z-0.5f);
		
		newZombie.transform.position = zombieSpawnPos;
		newZombie.GetComponent<BasicZombie>().SetHomeGrave(this);
		ChangeNumZombies(-1);

		UpdateCandles ();
		CreateDust();
	}
	
	public void ReturnZombie()
	{
		ChangeNumZombies(1);
		PlayBurySound();

		Results.moneyEarned += GlobalValues.returnMoney;
		Results.zombiesReturned++;

		GlobalValues.AddMoney(GlobalValues.returnMoney);
		MoneyUI.spawnMoneyText (transform.position, GlobalValues.returnMoney);

		
		UpdateCandles ();
		CreateDust();
	}
	
	public bool CanGetDeeper(int change)
	{
		return (depth+change) <= MAX_DEPTH;
	}
	
	public void ChangeDepth(int change)
	{	
		depth += change;
		
		if (depth < MIN_ZOMBIES)
		{
			depth = MIN_ZOMBIES;
		}

		UpdateCandles ();
		CreateDust ();
		//UpdateGravestoneText();
	}
	
	/*public bool CanBeDeeper()
	{
		return depth < MAX_DEPTH;
	}*/
	
	/*public void SetActive(bool isActive)
	{
		activated = isActive;
	}*/
	
	private void ChangeNumZombies(int change)
	{
		curZombies += change;

		UpdateCandles ();
		//UpdateGravestoneText();
	}
	
	public int GetNumZombies()
	{
		return curZombies;
	}
	
	public bool HasRoom()
	{
		return (curZombies < depth);
	}
	
	private void CreateGravestoneText()
	{
		Vector3 gravePos = gravestoneOb.transform.position;
		textBox = new GameObject();
		textBox.transform.position = new Vector3(gravePos.x,gravePos.y+0.2f,gravePos.z-1);
		textBox.transform.localScale = new Vector3(0.5f,0.5f,1);
		textBox.transform.eulerAngles = new Vector3(90,0,0);
		TextMesh myText = (TextMesh)textBox.AddComponent(typeof(TextMesh));
		
		//myText.text = curZombies.ToString()+"\n"+depth.ToString();
		//UpdateGravestoneText();
		MeshRenderer meshRend = textBox.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
		meshRend.material = Resources.Load("Fonts/arial",typeof(Material)) as Material;
		meshRend.material.shader = Resources.Load ("Shaders/3DText",typeof(Shader)) as Shader;
		
		myText.font = Resources.Load("Fonts/arial",typeof(Font)) as Font;
		myText.fontSize = TEXT_SIZE;
		myText.anchor = TextAnchor.MiddleCenter;
		myText.alignment = TextAlignment.Center;
		myText.GetComponent<Renderer>().material.color = Color.black;
	}
	
	private void UpdateGravestoneText()
	{
		TextMesh myText = (TextMesh)textBox.GetComponent<TextMesh>();
		myText.text = curZombies.ToString()+"/"+depth.ToString();
	}
	
	private void PlayBurySound()
	{
		int index = Random.Range(0, SoundEffectLibrary.bury.Count);
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.bury[index]);
	}
}
