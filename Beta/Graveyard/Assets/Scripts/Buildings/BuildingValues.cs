using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingValues : MonoBehaviour 
{
	[SerializeField] private List<BuildingData> buildings;

	public BuildingData GetData(string name)
	{
		foreach (BuildingData building in buildings) 
		{
			if (building.name == name)
			{
				return building;
			}
		}
		
		Debug.LogError ("Building "+name+" not found");
		return null;
	}
}
