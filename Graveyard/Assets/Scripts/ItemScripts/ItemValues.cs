using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemValues : MonoBehaviour 
{
	[SerializeField] private List<ItemData> items;

	public ItemData GetData(string name)
	{
		foreach (ItemData item in items) 
		{
			if (item.name == name)
			{
				return item;
			}
		}

		Debug.LogError ("Item "+name+" not found");
		return null;
	}
}
