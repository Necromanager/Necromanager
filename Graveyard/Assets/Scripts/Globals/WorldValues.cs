using UnityEngine;
using System.Collections;

public class WorldValues : MonoBehaviour 
{
	[SerializeField] private float minuteLength;
	[SerializeField] private DifficultyValues minWaveTime;
	[SerializeField] private DifficultyValues maxWaveTime;
	[SerializeField] private DifficultyValues minZombiesPerWave;
	[SerializeField] private DifficultyValues maxZombiesPerWave;
	
	[SerializeField] private bool usePlayerSelectedValues = true;
	
	[SerializeField] private float startingDifficulty;
	[SerializeField] private int startingSize;
	[SerializeField] private float startingMoney;
	[SerializeField] private int startingDay;

	void Awake() 
	{
		GlobalValues.MINUTE_TIME = minuteLength;
		//GlobalValues.minZombieSpawn = minZombieSpawnTime;
		//GlobalValues.maxZombieSpawn = maxZombieSpawnTime;
		GlobalValues.minWaveTime = minWaveTime;
		GlobalValues.maxWaveTime = maxWaveTime;
		GlobalValues.minWaveZombies = minZombiesPerWave;
		GlobalValues.maxWaveZombies = maxZombiesPerWave;
		
		if (!usePlayerSelectedValues)
		{
			GlobalValues.difficulty = startingDifficulty;
			GlobalValues.size = startingSize;
			GlobalValues.money = startingMoney;
			GlobalValues.day = startingDay;
		}
	}
	
	/*public float GetMinSpawn()
	{
		return minZombieSpawnTime;
	}
	
	public float GetMaxSpawn()
	{
		return maxZombieSpawnTime;
	}*/
}
