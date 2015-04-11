using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewStoreMenu : MonoBehaviour {

	int numItems = 10;
	private List<Item> itemList = new List<Item>();

	void fillList()
	{
		//itemList.Add(new Shotgun());
		itemList.Add(new Coffee());
		itemList.Add(new PickAxe());

		for(int i = 2; i < numItems; i++)
		{
			itemList.Add(new NoItem());
		}


		/*
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		*/

		foreach (Item item in itemList)
		{
			item.Init();
		}
	}
}
