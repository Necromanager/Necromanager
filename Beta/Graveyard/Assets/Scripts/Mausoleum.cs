using UnityEngine;
using System.Collections;

public class Mausoleum : MonoBehaviour 
{
	private const float MIN_SPAWN_TIME = 10;
	private const float MAX_SPAWN_TIME = 60;
	
	[SerializeField] private float visibleRange;
	[SerializeField] private float minVisibility;
	
	//private bool bossWave;
	private bool bossSpawned;
	
	private float curSpawnTime;
	private float spawnTime;
	
	private GameObject playerCamera;

	void Start () 
	{
		Init();
	}
	
	void Update () 
	{
		UpdateSpawnTime();
		
		/*if (Input.GetKeyDown(KeyCode.B))
		{
			curSpawnTime = spawnTime-0.1f;
			bossWave = true;
		}*/
	}
	
	void OnGUI()
	{
		if (bossSpawned)
		{
		}
	}
	
	private void UpdateSpawnTime()
	{
		if (curSpawnTime < spawnTime)
		{
			curSpawnTime += Time.deltaTime;
			if (curSpawnTime >= spawnTime)
			{
				curSpawnTime = spawnTime;
				SpawnBoss();
			}
		}
	}
	
	private void Init()
	{
		//bossWave = false;
		bossSpawned = false;
		spawnTime = Random.Range(MIN_SPAWN_TIME,MAX_SPAWN_TIME);
		curSpawnTime = spawnTime;
		playerCamera = Camera.main.gameObject;
	}
	
	public void Reset()
	{
		Init ();
		
		/*bossWave = Random.value >= 0.75f;
		if (bossWave)
		{
			curSpawnTime = 0;
		}*/
	}
	
	private GameObject GetBossToSpawn()
	{
		return GameObject.Instantiate(Resources.Load("Zombies/Necromancer")) as GameObject;
	}
	
	private void SpawnBoss()
	{
		Vector3 pos = transform.position;
		Vector3 spawnPosition = new Vector3(pos.x,1.5f,pos.z-2);
		
		GameObject bossZombie = GetBossToSpawn();
		bossZombie.transform.position = spawnPosition;
		
		bossSpawned = true;
	}
	
	private Color GetColor()
	{
		float curAlpha = 0;
		Vector3 camPos = new Vector3(playerCamera.transform.position.x,0,playerCamera.transform.position.z);
		Vector3 myPos = new Vector3(transform.position.x,0,transform.position.z);
		float distance = Vector3.Distance(camPos, myPos);
		
		if (distance <= visibleRange)
		{
			curAlpha = distance/visibleRange;
			
			if (curAlpha < minVisibility)
			{
				curAlpha = minVisibility;
			}
			
			return new Color(1,1,1,curAlpha);
		}
		
		return new Color(1,1,1);
	}
}
